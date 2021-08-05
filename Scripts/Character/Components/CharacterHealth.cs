using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : CharacterComponent
{
    private float _MaxHealth;
    private float _CurrentHealth;
    private float _DefaultMaxHealth = 100f;

    private bool _IsAlive;

    protected override void Start()
    {
        base.Start();
        SetToDefault();
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
    }

    protected override void SetToDefault()
    {
        _MaxHealth = _DefaultMaxHealth;
        _CurrentHealth = _DefaultMaxHealth;

        _IsAlive = true;
    }

    // A Player can gain health
    public void HealCharacter(float amount)
    {
        float checkMaxHeal = _CurrentHealth + amount;

        if(checkMaxHeal >= _MaxHealth)
        {
            _CurrentHealth = _MaxHealth;
        }
        else
        {
            _CurrentHealth += amount;
        }
    }

    // A player can lose health
    public void DamageCharacter(float amount)
    {
        _CurrentHealth -= amount;
        // check if player is dead
        CheckCharacterStatus();
    }

    // Increase Max Health
    public void IncreaseMaxHealth(float amount)
    {
        // TODO: Should we add a cap?
        _MaxHealth += amount;
    }

    public void DecreaseMaxHealth(float amount)
    {
        // Incase of status effect, this could come in handy
        _MaxHealth -= amount;
    }

    // A player can die
    public void CheckCharacterStatus(){
        if(_CurrentHealth <= 0)
        {
            _IsAlive = false;
        }
    }

}
