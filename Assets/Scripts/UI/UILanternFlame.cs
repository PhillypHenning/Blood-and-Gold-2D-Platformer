using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILanternFlame : MonoBehaviour
{
    private Animator _Animator;

    void Start()
    {
        _Animator = GetComponent<Animator>();

        if (_Animator == null) Debug.LogWarning("UI Lantern Flame could not find an animator component.");
    }

    public void SetLevel(int level)
    {
        switch (level)
        {
            case 0:
                _Animator.Play("LowFlame");
                break;
            case 1:
                _Animator.Play("MidFlame");
                break;
            case 2:
                _Animator.Play("LargeFlame");
                break;
            default:
                return;
        }
    }
}
