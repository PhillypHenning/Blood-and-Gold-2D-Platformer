using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextPopupUI : MonoBehaviour
{
    private float _TextFadeTime;
    public float _TimeToShow = 2f;
    private TextMeshProUGUI _TextUI;

    private void Start()
    {
        _TimeToShow = 2f;
        _TextUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_TextUI.text != "" && (Time.time >= _TextFadeTime))
        {
            _TextUI.text = "";
        }
    }

    public void UpdateText(string message)
    {
        _TextUI.text = message;
        _TextFadeTime = Time.time + _TimeToShow;
    }

}