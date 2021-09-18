using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SconceCounter : MonoBehaviour
{
    public int LitSconces = 0;
    private TextMeshProUGUI _TextUI;
    private Animator _Animator;
    private bool _IsUISconceLit = false;
    
    void Start()
    {
        _TextUI = GameObject.Find("LitSconces").GetComponent<TextMeshProUGUI>();
        _Animator = GameObject.Find("SconceUI").GetComponent<Animator>();
    }

    public void AddSconce()
    {
        LitSconces++;
        _TextUI.text = LitSconces.ToString();
        if (!_IsUISconceLit)
        {
            _Animator.SetTrigger("IsLit");
        }
    }
}
