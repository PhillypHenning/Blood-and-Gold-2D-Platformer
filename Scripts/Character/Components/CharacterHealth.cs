using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : Health
{
    private float _CharacterMaxHealth = 100f;

    protected override void SetToDefault()
    {
        _DefaultMaxHealth = _CharacterMaxHealth;
        base.SetToDefault();
    }
    
    protected override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(KeyCode.J))
        {
            Damage(10);
        }
    }
}
