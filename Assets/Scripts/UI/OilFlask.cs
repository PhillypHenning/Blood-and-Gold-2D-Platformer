using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OilFlask: MonoBehaviour
{
    // adjust the slider value to match massed value
    private Slider _Slider;
    private int _OilQuantity;

    [SerializeField] private InventoryManager _PlayerInventory;

    private void Start()
    {
        _Slider = GetComponent<Slider>();
        if (_Slider == null) Debug.LogError("OilFlask UI slider could not find a slider component");
    }

    private void Update()
    {
        var oilQuantity = _PlayerInventory.GetQuantity(ItemType.Oil);
        if (_Slider.value == oilQuantity) return;
        SetSlider(oilQuantity);
    }

    public void SetSlider(float value)
    {
        _Slider.value = value / 100;
    }
}