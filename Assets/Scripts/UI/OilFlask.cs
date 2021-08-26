using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OilFlask: MonoBehaviour
{
    // adjust the slider value to match massed value
    private Slider _Slider;

    private void Start()
    {
        _Slider = GetComponent<Slider>();
        if (_Slider == null) Debug.LogError("OilFlask UI slider could not find a slider component");
    }

    public void SetSlider(float value)
    {
        _Slider.value = value / 100;
    }
}