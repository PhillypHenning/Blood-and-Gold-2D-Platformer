using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : CharacterComponent
{
    private bool _CharacterCanJump;
    private bool _IsGrounded;
    public bool IsGrounded => _IsGrounded;
    public bool CharacterCanJump => _CharacterCanJump;

    private float _Fallmultiplier = 2.5f;
    private float _LowJumpModifier = 2.5f;
    private float _GravityScaled = .5f; // Lower = More gravity applied.

    public float _HeightestJumpReached; // TODO: Stretch goal, but may be interesting?

    public float VerticalTakeoff = 20f;

    public bool JumpIfAble = false;
    public float JumpCD = 1f;
    public float JumpCDTimer;

    // Jumping should disable all other CharacterAbilities.. unfortunately I'm not sure if there is a reliable way of knowing when a jump is complete..
    // If we make the variable based on simply standing on the ground that will effect every other ability since the character will likely be standing on the ground. 
    // TODO: Find a reliable way to know when the character is moving? I was thinking by having a floating y value, if it's moving then count the character in a jump motion?

    private CharacterAnimation _Animation;

    protected override void Start()
    {
        base.Start();
        JumpCDTimer = JumpCD;
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
        DecideCharacterCanJump();
        ApplyGameGravity();
        HandleJumpCD();

        _HeightestJumpReached = 0;
    }

    private void HandleJumpCD()
    {
        if (JumpCDTimer < 0) return;
        JumpCDTimer -= Time.deltaTime;
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        if (!_HandleInput) { return; }
        if (Input.GetKeyDown(KeyCode.Space) && _CharacterCanJump && !_Character.IsLocked && JumpCDTimer <= 0)
        {
            JumpCDTimer = JumpCD;
            Jump();
        }
    }

    protected override void InternalInput()
    {
        base.InternalInput();
        if((JumpIfAble && JumpCDTimer <= 0) && _CharacterCanJump && !_Character.IsLocked){
            JumpCDTimer = JumpCD;
            Jump();
        }
    }

    private void DecideCharacterCanJump()
    {
        // Any decision logic should be put in here.
        // Basic Decisions;
        //  1. Is the player on the ground?
        //  2. Does the player have an extra jump?
        bool touchingPlatform = false;

        if (_Character.RigidBody2D.IsTouchingLayers(LayerMask.GetMask("Platforms")))
        {
            touchingPlatform = true;
        }

        // if we add a double jump this logic will need to change since "canjump" will be determined by other factors
        _CharacterCanJump = touchingPlatform;

        // TODO: add coyote timer?
        _Character.IsGrounded = touchingPlatform;
    }

    private void Jump()
    {
        //_CharacterRigidBody2D.velocity = new Vector2(0, _JumpHeight);
        //_CharacterRigidBody2D.AddForce(new Vector2(0, _JumpHeight), ForceMode2D.Impulse);


        _Character.RigidBody2D.velocity = Vector2.up * VerticalTakeoff;

        // animate the jump
        _CharacterAnimation.Jump();
    }

    private void ApplyGameGravity()
    {
        // This function applies a more game like physics to the jumping. The jump takes longer to reach it's peak, then has a snapper fall to the ground. 
        if (_Character.RigidBody2D.velocity.y < 0)
        {
            _Character.RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (_Fallmultiplier - _GravityScaled) * Time.deltaTime;
        }
        else if (_Character.RigidBody2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            // Enables us to have a "sensitive" jump height
            _Character.RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (_LowJumpModifier - _GravityScaled) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Platform-Slide")
        {
            // Start sticky slide

            // Start pushing down
            _Character.RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime;

            // Maybe push a little to the left?

        }

        if (other.tag == "Platform-Jump")
        {
            _CharacterCanJump = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Character/Player_Land");
        }

        if (other.tag == "Platform")
        {
            _CharacterCanJump = true;
            //_CharacterAnimation.Landing();
        }
    }

    public void TriggerJump()
    {
        if (_CharacterCanJump && !_Character.IsLocked)
        {
            Jump();
        }
    }
}
