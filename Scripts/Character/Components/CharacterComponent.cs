using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    // Character Components add basic "character" functionality.
    protected Character _Character;
    protected CharacterController _CharacterController;
    protected CharacterMovement _CharacterMovement;
    protected Rigidbody2D _CharacterRigidBody2D;

    protected bool _CharacterLock;
    protected bool _CharacterAbillityLock;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        _Character = GetComponent<Character>();
        _CharacterController = GetComponent<CharacterController>();
        _CharacterMovement = GetComponent<CharacterMovement>();
        _CharacterRigidBody2D = GetComponent<Rigidbody2D>();

        _CharacterAbillityLock = false;
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
        // Standard Use Functions
        HandleInput();
        InternalInput();
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
