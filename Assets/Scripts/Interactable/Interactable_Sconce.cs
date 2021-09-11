using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Interactable_Sconce : Interactable 
{
    private Light2D _Light;
    private bool _IsLit;

    private const float MAX_INTENSITY = 0.85f;
    private const float MIN_INTENSITY = 0.65f;
    private static float _FlickerFactor = 0.0f;
    private float _FlickerMax;
    private float _FlickerMin;

    protected override void Update()
    {
        base.Update();
        Flicker();
    }

    protected override void SetToDefault()
    {
        base.SetToDefault();
        _DefaultMessage = "Press 'F' to light.";
        _IsLit = false;
        _FlickerMin = MIN_INTENSITY;
        _FlickerMax = MAX_INTENSITY;
    }

    protected override bool InputEnabled()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

    protected override void Reward()
    {
        base.Reward();
        if (!CheckPlayerFuel())
        {
            UpdateMessage("Not enough fuel to light sconce.");
            return;
        }
        RemoveVisualQue();
        LightSconce();
    }

    private void LightSconce()
    {
        _Light = GetComponentInChildren<Light2D>();

        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Interactables/Torch/Torch_Light");
        if (_Light == null) Debug.LogError("Sconce could not find Light2D component");
        _Light.enabled = true;
        _IsLit = true;
    }

    private void Flicker()
    {
        if (!_IsLit) return;
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

    private bool CheckPlayerFuel()
    {
        var playerInventory = _Character.GetComponent<InventoryManager>();
        if (playerInventory == null) Debug.LogError("Player inventory not found.");
        if (playerInventory.GetQuantity(ItemType.Oil) >= 10)
        {
            playerInventory.RemoveFromInventory(ItemType.Oil, 10);
            return true;
        }

        return false;
    }

    public void TurnOffLight(){
        _IsLit = false;
    }
}