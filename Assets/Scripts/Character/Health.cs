using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    protected float _MaxHealth;
    protected float _CurrentHealth;
    protected float _DefaultMaxHealth { get; set;}
    protected Collider2D _Collider2D;

    protected bool _IsAlive;
    protected bool _Hitable = true;

    public bool IsAlive => _IsAlive;
    public float CurrentHealth => _CurrentHealth;

    protected virtual void Start() 
    {
        // Set Default values
        SetToDefault();
        _Collider2D = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        HandleAbility();
    }
    
    protected virtual void SetToDefault()
    {
        // Base Values you want can be set here with an override
        _MaxHealth = _DefaultMaxHealth;
        _CurrentHealth = _DefaultMaxHealth;
        _IsAlive = true;
    }

    protected virtual void HandleAbility()
    {
        HandleInput();
    }

    protected virtual void HandleInput()
    {
        // For testing purposes.
        // You would want to override this puppy with the child script and add something like; 
        // if(Input.GetKeyDown(KeyCode.J))
        // DamageCharacter(10);
    }

    protected virtual void CheckCharacterStatus()
    {
        // not needed
        if(_CurrentHealth <= 0){
            _IsAlive = false;
        }
    }

    public virtual bool Heal(float amount)
    {
        float newHealth = _CurrentHealth + amount;

        if (newHealth > _MaxHealth)
        {
            return false;
        }
        else
        {
            _CurrentHealth += amount;
        }
        return true;
    }

    public virtual void Damage(float amount)
    {
        if(!_Hitable){ return; }

        float newHealth = _CurrentHealth - amount;

        if (newHealth <= 0)
        {
            _CurrentHealth = 0;
            Die();
        }
        else
        {
            _CurrentHealth -= amount;
        }
    }

    protected virtual void Die()
    {
        _IsAlive = false;
    }

    public virtual void TimedDecraseHealth(float amount, float time)
    {
        // TODO
    }

    public virtual void TimedIncreaseHealth(float amount, float time)
    {

    }

    public virtual void MakeInvun(){
        _Hitable = false;
    }

    public virtual void TurnOffMakeInvun(){
        _Hitable = true;
    }
}
