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

    [SerializeField] private LanternDial _LanternDial;
    private bool _IsLanternOn;
    private bool _AdjustmentMode;
    private float _DrainRate;
    private float _IsEnabled;
    private Light2D _Light;

    private float _OilDrainPool = 0f;

    // MIN / MAX / OFF threshholds for light radius (inner/outer)
    // logic assumes that inner min/max is 1/4 outer min/max
    private const float MIN_INNER_RADIUS = 0.5f;
    private const float MAX_INNER_RADIUS = 3f;

    private const float MIN_OUTER_RADIUS = 4f;
    private const float MAX_OUTER_RADIUS = 12f;

    private const float OUTER_RADIUS_OFF = 2f;
    private const float INNER_RADIUS_OFF = 0.3f;

    private const float MAX_DRAIN_RATE = .1f;

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

        SetLanternDial();

        if (_Light == null) Debug.LogWarning("CharacterLantern was unable to locate a Light2D component.");
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        if(!_HandleInput){return;}
        if (_Character.IsLocked) return;
        if (Input.GetKeyDown(KeyCode.X))
        {
            SwitchLanternOnOff();
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            SwitchAdjustmentMode();
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

            SetLanternDial();
        }
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
        DrainOil();
        AdjustLantern();
    }

    protected override void SetToDefault()
    {
        _IsLanternOn = true;
        _InnerRadiusThreshold = MAX_INNER_RADIUS;
        _OuterRadiusThreshold = MAX_OUTER_RADIUS;
        _FlickerMax = MAX_INTENSITY;
        _FlickerMin = MIN_INTENSITY;
    }

    private void DrainOil()
    {
        if (_InventoryManager.GetQuantity(ItemType.Oil) == 0)
        {
            _IsLanternOn = false;
            SetLanternDial();
            return;
        }

        _OilDrainPool += DrainRate();
        if (_OilDrainPool < 100f) return;

        _OilDrainPool = 0f;
        _InventoryManager.RemoveFromInventory(ItemType.Oil, 1);
        Debug.Log("Oil remaining: " + _InventoryManager.GetQuantity(ItemType.Oil) + "%");
    }

    private float DrainRate()
    {
        if (_Light.pointLightInnerRadius <= INNER_RADIUS_OFF)
        {
            return 0;
        }

        return (MAX_DRAIN_RATE * (_Light.pointLightInnerRadius / MAX_INNER_RADIUS));
    }

    private void SwitchAdjustmentMode()
    {
        _AdjustmentMode = !_AdjustmentMode;
        if (_AdjustmentMode)
        {
            _LanternDial.AdjustmentModeOn();
        }
        else
        {
            _LanternDial.AdjustmentModeOff();
        }
    }

    private void SwitchLanternOnOff()
    {
        // white B7B7B7
        // orange F3C08B
        _IsLanternOn = !_IsLanternOn;
        if (_IsLanternOn)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/Lantern/Lantern_Light");
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/Lantern/Lantern_Extinguish");
        }

        SetLanternDial();
    }


    private void Flicker()
    {
        // TODO: Flicker needs to be based on something either than intensity (doesn't look good)


        // Lerp from  Min/Max Radius -5, to Min/Max Radius + 5
        // -5, Min/Max Radius, +5
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

    private void AdjustLantern()
    {
        var timeFactor = Time.deltaTime * 10;

        if (_IsLanternOn)
        {
            if (IsAtThreshold(_InnerRadiusThreshold, _OuterRadiusThreshold))
            {
                // flicker 
                Flicker();
                return;
            }

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
            if (IsAtThreshold(MIN_INNER_RADIUS, OUTER_RADIUS_OFF)) return;

            if (_Light.pointLightInnerRadius > INNER_RADIUS_OFF)
            {
                DecreaseInnerRadius(timeFactor, INNER_RADIUS_OFF);
            }
            if (_Light.pointLightOuterRadius > OUTER_RADIUS_OFF)
            {
                DecreaseOuterRadius(timeFactor, OUTER_RADIUS_OFF);
            }
        }
    }

    private float ThresholdPercentage()
    {
        return ((_InnerRadiusThreshold - MIN_INNER_RADIUS) / (MAX_INNER_RADIUS - MIN_INNER_RADIUS));
    }

    private void SetLanternDial()
    {
        if (_IsLanternOn)
        {
            _LanternDial.SetDialPosition(ThresholdPercentage());
        }
        else
        {
            _LanternDial.SetDialPositionOff();
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
