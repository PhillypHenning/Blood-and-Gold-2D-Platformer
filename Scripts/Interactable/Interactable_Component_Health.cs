using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Component_Health : Health
{
    private float _CharacterMaxHealth = 50f;

    protected override void SetToDefault(){
        _DefaultMaxHealth = _CharacterMaxHealth;
        base.SetToDefault();
    }
    
    protected override void HandleInput(){
        base.HandleInput();
        if(Input.GetKeyDown(KeyCode.K)){
            Damage(10);
        }
    }
}
