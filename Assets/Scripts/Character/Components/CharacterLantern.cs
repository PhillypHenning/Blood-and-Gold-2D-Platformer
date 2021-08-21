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
    private float _DrainRate;
    private float _IsEnabled;
    private Light2D _Light;

    public float DrainRate => _DrainRate;

    protected override void Start()
    {
        base.Start();
        _Light = GetComponentInChildren<Light2D>();
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(KeyCode.X) && !_Character.IsLocked)
        {
            SwitchLantern();
        }
    }

    private void SwitchLantern()
    {
        // TODO: add animation to smooth transition
        _IsLanternOn = !_IsLanternOn;

        if (_IsLanternOn)
        {
            SetRadius(0f, 4f);
        }
        else
        {
            SetRadius(3f, 12f);
        }
    }

    private void SetRadius(float inner, float outer)
    {
        StartCoroutine(SetOuterRadius(inner, outer));
    }

    // TODO: refactor into re-usable function called twice 
    private IEnumerator SetOuterRadius(float newInnerRadius, float newOuterRadius)
    {
        // don't look at me, it's 2am and it's the first draft, bed now refactor later
        var currentInnerRadius = _Light.pointLightInnerRadius;
        var currentOuterRadius = _Light.pointLightOuterRadius;
        var radiusDiff = currentOuterRadius - newOuterRadius;

        bool isIncreasing = radiusDiff < 0;

        // while the difference between old / new value != 0, alter value
        while (_Light.pointLightInnerRadius != newInnerRadius || _Light.pointLightOuterRadius != newOuterRadius)
        {
            var timeFactor = Time.deltaTime * 6;

            if (isIncreasing)
            {
                if (_Light.pointLightInnerRadius + timeFactor > newInnerRadius)
                {
                    _Light.pointLightInnerRadius = newInnerRadius;
                }
                else
                {
                    _Light.pointLightInnerRadius += timeFactor;
                }

                if (_Light.pointLightOuterRadius + timeFactor > newOuterRadius)
                {
                    _Light.pointLightOuterRadius = newOuterRadius;
                }
                else
                {
                    _Light.pointLightOuterRadius += timeFactor;
                }
            }
            else
            {
                if (_Light.pointLightInnerRadius - timeFactor < newInnerRadius)
                {
                    _Light.pointLightInnerRadius = newInnerRadius;
                }
                else
                {
                    _Light.pointLightInnerRadius -= timeFactor;
                }

                if (_Light.pointLightOuterRadius - timeFactor < newOuterRadius)
                {
                    _Light.pointLightOuterRadius = newOuterRadius;
                }
                else
                {
                    _Light.pointLightOuterRadius -= timeFactor;
                }
            }

            yield return null;
        }
    }
}
