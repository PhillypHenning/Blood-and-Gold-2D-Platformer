using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    // Character Components add basic "character" functionality.
    protected Character _Character;
    protected CharacterController _CharacterController;
    protected CharacterMovement _CharacterMovement;
    protected CharacterJump _CharacterJump;
    protected CharacterStats _CharacterStats;
    protected CharacterHealth _CharacterHealth;
    protected CharacterAnimation _CharacterAnimation;

    // Gameobject Unity components
    protected Rigidbody2D _CharacterRigidBody2D;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        // You may feel tempted to remove the base. series of template functions, but be warned; if you do it, you will loose your references to these components. 
        _Character = GetComponent<Character>();
        _CharacterController = GetComponent<CharacterController>();
        _CharacterMovement = GetComponent<CharacterMovement>();
        _CharacterJump = GetComponent<CharacterJump>();
        _CharacterRigidBody2D = GetComponent<Rigidbody2D>();
        _CharacterStats = GetComponent<CharacterStats>();
        _CharacterHealth = GetComponent<CharacterHealth>();
        // does every character component need an animation component?
        _CharacterAnimation = GetComponent<CharacterAnimation>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleAbility();
        // will every child component have an animation to handle?
        HandleAnimation();
    }

    protected virtual void HandleAnimation()
    {
        // use this for components that use animation
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
        // Handles Player Inputs and Actions on them
        // For an easy example please review the Dash component.
    }

    protected virtual void InternalInput()
    {
        // Handles "Game Engine" Inputs (AI)
        
    }

    protected virtual void SetToDefault(){
        
    }
}
