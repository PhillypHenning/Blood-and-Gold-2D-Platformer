using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : CharacterComponent
{
    private bool _CharacterCanJump;

    private float _VerticalTakeoff = 15f;
    private float _Fallmultiplier = 2.5f;
    private float _LowJumpModifier = 2.5f;
    private float _GravityScaled = 1f;

    public float _HeightestJumpReached; // TODO: Stretch goal, but may be interesting?

    // Jumping should disable all other CharacterAbilities.. unfortunately I'm not sure if there is a reliable way of knowing when a jump is complete..
    // If we make the variable based on simply standing on the ground that will effect every other ability since the character will likely be standing on the ground. 
    // TODO: Find a reliable way to know when the character is moving? I was thinking by having a floating y value, if it's moving then count the character in a jump motion?

    protected override void Start()
    {
        base.Start();
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
        DecideCharacterCanJump();
        ApplyGameGravity();

        _HeightestJumpReached = 0;
    }

    protected override void HandleInput()
    {
        // Check if the player has pressed the Space button and the player can jump
        if(Input.GetKeyDown(KeyCode.Space) && _CharacterCanJump ){
            Jump();
        }
    }

    private void DecideCharacterCanJump(){
        // Any decision logic should be put in here.
        // Basic Decisions;
        //  1. Is the player on the ground?
        //  2. Does the player have an extra jump?
        bool canJump = false;
        if(_CharacterRigidBody2D.IsTouchingLayers(LayerMask.GetMask("Platforms"))){
            canJump = true;
        }
        _CharacterCanJump = canJump;
    }

    private void Jump(){
        //_CharacterRigidBody2D.velocity = new Vector2(0, _JumpHeight);
        //_CharacterRigidBody2D.AddForce(new Vector2(0, _JumpHeight), ForceMode2D.Impulse);
        
        
        //_CharacterStats._CanUseAbility = false;
        _CharacterRigidBody2D.velocity = Vector2.up * _VerticalTakeoff;
    }

    private void ApplyGameGravity(){
        // This function applies a more game like physics to the jumping. The jump takes longer to reach it's peak, then has a snapper fall to the ground. 
        
        if(_CharacterRigidBody2D.velocity.y < 0){
            _CharacterRigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (_Fallmultiplier - _GravityScaled) * Time.deltaTime;
        }
        else if(_CharacterRigidBody2D.velocity.y > 0 && !Input.GetButton("Jump")){
            // Enables us to have a "sensitive" jump height
            _CharacterRigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (_LowJumpModifier - _GravityScaled) * Time.deltaTime;
        }
    }
}
