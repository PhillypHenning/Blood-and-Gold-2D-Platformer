using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    // Character Components add basic "character" functionality.
    protected Character _Character;
    protected CharacterMovement _CharacterMovement;
    protected CharacterJump _CharacterJump;
    protected CharacterHealth _CharacterHealth;
    protected CharacterAnimation _CharacterAnimation;
    protected InventoryManager _InventoryManager;

    // Gameobject Unity components
    protected bool _HandleInput = true;
    protected bool _HandleInternalInput = true;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        // You may feel tempted to remove the base. series of template functions, but be warned; if you do it, you will loose your references to these components. 
        _Character = GetComponent<Character>();
        _CharacterMovement = GetComponent<CharacterMovement>();
        _CharacterJump = GetComponent<CharacterJump>();
        _CharacterHealth = GetComponent<CharacterHealth>();
        _CharacterAnimation = GetComponent<CharacterAnimation>();
        _InventoryManager = GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleAbility();
    }

    protected virtual void FixedUpdate()
    {
        HandlePhysicsAbility();
    }

    protected virtual void HandleAbility()
    {
        // actions below this line should not be possible after death
        if (!_CharacterHealth.IsAlive) return;

        // Standard Use Functions
        HandleInput();
        InternalInput();

        // If you remove the base.HandleAbility in a child function, it will stop running that components above functions ^^ *Input. 
    }

    protected virtual void HandlePhysicsAbility(){
        // Use if you need access to the FixedUpdate
        // This needs to be used for Dynamic Rigidbodies.
    }

    protected virtual void HandleInput()
    {   
        if (_Character.CharacterType == Character.CharacterTypes.Player && _Character.Actionable){
            _HandleInternalInput = false;
        }
        // Handles Player Inputs and Actions on them
        // For an easy example please review the Dash component.
    }

    protected virtual void InternalInput()
    {
        if (_Character.CharacterType == Character.CharacterTypes.AI && _Character.Actionable){ 
            _HandleInput = false;
         };
        // Handles "Game Engine" Inputs (AI)
        
    }

    protected virtual void SetToDefault()
    {
        
    }
}
