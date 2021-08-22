using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CharacterLantern : CharacterComponent
{
    // lantern is usable by player only.

    // Functionality TODO:
    //  - Enable / Disable (hotkey "x")
    //    - Disable should not reduce the light to 0, rather, it should emit very little light
    //  - Reference to Player oil resource (TODO)
    //    - Drains oil while Player is "holding" lantern
    //  - Adjust threshold (hotkey "LEFT ALT")
    //    - This will allow the player to use the scroll wheel to add/reduce oil usage
    //      - more fuel = more light + faster drain rate

    private bool _IsLanternOn;
    private bool _AdjustmentMode;
    private float _DrainRate;
    private float _IsEnabled;
    private Light2D _Light;

    public float DrainRate => _DrainRate;

    // MIN / MAX threshholds for light radius (inner/outer)
    private const float MIN_INNER_RADIUS = 0.5f;
    private const float MAX_INNER_RADIUS = 3f;

    private const float MIN_OUTER_RADIUS = 4f;
    private const float MAX_OUTER_RADIUS = 12f;

    // tracks player-set threshold for lantern brightness
    private float _InnerRadiusThreshold;
    private float _OuterRadiusThreshold;

    // tracks flicker effect animation
    private const float MAX_INTENSITY = 0.8f;
    private const float MIN_INTENSITY = 0.7f;
    private static float _FlickerFactor = 0.0f;
    private float _FlickerMax;
    private float _FlickerMin;

    protected override void Start()
    {
        base.Start();
        SetToDefault();
        _Light = GetComponentInChildren<Light2D>();

        if (_Light == null) Debug.LogWarning("CharacterLantern was unable to locate a Light2D component.");
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        if (_Character.IsLocked) return;
        if (Input.GetKeyDown(KeyCode.X))
        {
            SwitchLanternOnOff();
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            SwitchAdjustmentMode();
            _IsLanternOn = true;
        }
        if (_IsLanternOn && _AdjustmentMode)
        {
            var scrollWheelY = Input.mouseScrollDelta.y;
            if (scrollWheelY == 0) return;
            if (scrollWheelY > 0)
            {
                if (_InnerRadiusThreshold + .25f <= MAX_INNER_RADIUS)
                {
                    _InnerRadiusThreshold += .25f;
                }
                else
                {
                    _InnerRadiusThreshold = MAX_INNER_RADIUS;
                }

                if (_OuterRadiusThreshold + 1f <= MAX_OUTER_RADIUS)
                {
                    _OuterRadiusThreshold += 1f;
                }
                else
                {
                    _OuterRadiusThreshold = MAX_OUTER_RADIUS;
                }
            }
            else
            {
                if (_InnerRadiusThreshold - .25f >= MIN_INNER_RADIUS)
                {
                    _InnerRadiusThreshold -= .25f;
                }
                else
                {
                    _InnerRadiusThreshold = MIN_INNER_RADIUS;
                }

                if (_OuterRadiusThreshold - 1f >= MIN_OUTER_RADIUS)
                {
                    _OuterRadiusThreshold -= 1f;
                }
                else
                {
                    _OuterRadiusThreshold = MIN_OUTER_RADIUS;
                }
            }

            // TODO: figure out best use case for player interacting with lantern 
            // should it automatically turn off when at min threshold? 
            // should the min threshold be higher when lantern is off?
            // what feels the most natural?
            // check to see if lantern should be turned on or off
            if (IsAtThreshold(MIN_INNER_RADIUS, MIN_OUTER_RADIUS))
            {
                _IsLanternOn = false;
                _AdjustmentMode = false;
            }
            else
            {
                _IsLanternOn = true;
            }
        }
    }

    private void SwitchAdjustmentMode()
    {
        _AdjustmentMode = !_AdjustmentMode;
    }

    private void SwitchLanternOnOff()
    {
        _IsLanternOn = !_IsLanternOn;

        if (_IsLanternOn)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/Lantern/Lantern_Light");
        }

        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/Lantern/Lantern_Extinguish");
        }
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
        AdjustLantern();
        //Flicker();
    }

    private void Flicker()
    {
        // TODO: Flicker needs to be based on something either than intensity (doesn't look good)
        _Light.intensity = Mathf.Lerp(_FlickerMin, _FlickerMax, _FlickerFactor);
        _FlickerFactor += Time.deltaTime;

        if (_FlickerFactor > 1.0f)
        {
            float tempMax = _FlickerMax;
            _FlickerMax = _FlickerMin;
            _FlickerMin = tempMax;
            _FlickerFactor = 0.0f;
        }
    }

    protected override void SetToDefault()
    {
        _IsLanternOn = true;
        _InnerRadiusThreshold = MAX_INNER_RADIUS;
        _OuterRadiusThreshold = MAX_OUTER_RADIUS;
        _FlickerMax = MAX_INTENSITY;
        _FlickerMin = MIN_INTENSITY;
    }

    private void AdjustLantern()
    {
        var timeFactor = Time.deltaTime * 10;

        if (_IsLanternOn)
        {
            if (IsAtThreshold(_InnerRadiusThreshold, _OuterRadiusThreshold)) return;

            if (_Light.pointLightInnerRadius < _InnerRadiusThreshold)
            {
                IncreaseInnerRadius(timeFactor);
            }
            else if (_Light.pointLightInnerRadius > _InnerRadiusThreshold)
            {
                DecreaseInnerRadius(timeFactor, _InnerRadiusThreshold);
            }

            if (_Light.pointLightOuterRadius < _OuterRadiusThreshold)
            {
                IncreaseOuterRadius(timeFactor);
            }
            else if (_Light.pointLightOuterRadius > _OuterRadiusThreshold)
            {
                DecreaseOuterRadius(timeFactor, _InnerRadiusThreshold);
            }
        }
        else
        {
            if (IsAtThreshold(MIN_INNER_RADIUS, MIN_OUTER_RADIUS)) return;

            if (_Light.pointLightInnerRadius > MIN_INNER_RADIUS)
            {
                DecreaseInnerRadius(timeFactor, MIN_INNER_RADIUS);
            }
            if (_Light.pointLightOuterRadius > MIN_OUTER_RADIUS)
            {
                DecreaseOuterRadius(timeFactor, MIN_OUTER_RADIUS);
            }
        }
    }

    private bool IsAtThreshold(float innerThreshold, float outerThreshold)
    {
        return _Light.pointLightInnerRadius == innerThreshold && _Light.pointLightOuterRadius == outerThreshold; 
    }

    private void IncreaseInnerRadius(float timeFactor)
    {
        if (_Light.pointLightInnerRadius + timeFactor > _InnerRadiusThreshold)
        {
            _Light.pointLightInnerRadius = _InnerRadiusThreshold;
        }
        else
        {
            _Light.pointLightInnerRadius += timeFactor;
        }
    }

    private void IncreaseOuterRadius(float timeFactor)
    {
        if (_Light.pointLightOuterRadius + timeFactor > _OuterRadiusThreshold)
        {
            _Light.pointLightOuterRadius = _OuterRadiusThreshold;
        }
        else
        {
            _Light.pointLightOuterRadius += timeFactor;
        }
    }

    private void DecreaseInnerRadius(float timeFactor, float threshold)
    {
        if (_Light.pointLightInnerRadius - timeFactor < threshold)
        {
            _Light.pointLightInnerRadius = threshold;
        }
        else
        {
            _Light.pointLightInnerRadius -= timeFactor;
        }
    }

    private void DecreaseOuterRadius(float timeFactor, float threshold)
    {
        if (_Light.pointLightOuterRadius - timeFactor < threshold)
        {
            _Light.pointLightOuterRadius = threshold;
        }
        else
        {
            _Light.pointLightOuterRadius -= timeFactor;
        }
    }
}
