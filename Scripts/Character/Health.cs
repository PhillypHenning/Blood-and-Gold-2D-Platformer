using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    protected float _MaxHealth;
    protected float _CurrentHealth;
    protected float _DefaultMaxHealth { get; set;}

    protected bool _IsAlive;

    protected virtual void Start() {
        // Set Default values
        SetToDefault();
    }

    protected virtual void Update(){
        HandleAbility();
    }
    
    protected virtual void SetToDefault()
    {
        // Base Values you want can be set here with an override
        _MaxHealth = _DefaultMaxHealth;
        _CurrentHealth = _DefaultMaxHealth;
        _IsAlive = true;
    }

    protected virtual void HandleAbility(){
        CheckCharacterStatus();
        HandleInput();
    }

    protected virtual void HandleInput(){
        // For testing purposes.
        // You would want to override this puppy with the child script and add something like; 
        // if(Input.GetKeyDown(KeyCode.J))
        // DamageCharacter(10);
        
    }

    public virtual void Heal(float amount){
        float newHealth = _CurrentHealth + amount;

        if(newHealth > _MaxHealth){
            return;
        }else{
            _CurrentHealth += amount;
        }
    }

    protected virtual void Damage(float amount){
        float newHealth = _CurrentHealth - amount;

        if(newHealth < 0){
            return;
        }else{
            _CurrentHealth -= amount;
        }
    }

    protected virtual void TimedDecraseHealth(float amount, float time){
        // TODO
    }

    protected virtual void TimedIncreaseHealth(float amount, float time){

    }

    public virtual void CheckCharacterStatus(){
        if(_CurrentHealth <= 0){
            _IsAlive = false;
        }
    }

    
    
}
