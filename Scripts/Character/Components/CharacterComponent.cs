using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    // Character Components add basic "character" functionality.
    protected Character _Character;
    protected CharacterController _CharacterController;
    protected CharacterMovement _CharacterMovement;
    protected CharacterStats _CharacterStats;

    // Gameobject Unity components
    protected Rigidbody2D _CharacterRigidBody2D;
    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // You may feel tempted to remove the base. series of template functions, but be warned; if you do it, you will loose your references to these components. 
        _Character = GetComponent<Character>();
        _CharacterController = GetComponent<CharacterController>();
        _CharacterMovement = GetComponent<CharacterMovement>();
        _CharacterRigidBody2D = GetComponent<Rigidbody2D>();
        _CharacterStats = GetComponent<CharacterStats>();
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
