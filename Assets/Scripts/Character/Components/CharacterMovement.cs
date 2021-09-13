using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponent
{
    public float _MovementSpeed;
    public float _DefaultMovementSpeed = 200f;

    private float _HorizontalMovement;
    private float _VerticalMovement;

    public float HorizontalMovement {get; set;}
    public float VerticalMovement {get; set;}

    public bool _MovementSurpressed;
    public bool _SlidingMovement = false;


    // TODO: This component will need to know which way the sprite is facing

    protected override void Start()
    {
        base.Start();
        SetToDefault();
        HorizontalMovement = _HorizontalMovement;
        VerticalMovement = _VerticalMovement;

        if(_Character.CharacterType == Character.CharacterTypes.Player){
            Physics2D.IgnoreLayerCollision(7, 13); // <--  ignore collision with "Enemy Wall"
            Physics2D.IgnoreLayerCollision(7, 8); // <--  ignore collision with "Enemies"
            Physics2D.IgnoreLayerCollision(7, 12);
        }

        else if(_Character.CharacterType == Character.CharacterTypes.AI){ // TODO ADD OTHER AI ENEMIES
            Physics2D.IgnoreLayerCollision(8, 8);
            Physics2D.IgnoreLayerCollision(8, 10); // <--  ignore collision with "Enemy Shield"
            Physics2D.IgnoreLayerCollision(8, 12);
            Physics2D.IgnoreLayerCollision(8, 14);
        }
    }

    protected override void HandlePhysicsAbility()
    {
        // Technique #1: Most Basic Movement
        // _CharacterRigidBody2D.velocity = new Vector2(_MovementSpeed * _HorizontalInput, 0);

        // Techinique #2: Adding Force to the Rigidbody to move in a more gradual way
        // It can feel like sliding on ice. Be cognitive that the Rigidbody mass is weighed in to the function.
        //_CharacterRigidBody2D.AddForce(new Vector2(_MovementSpeed * _HorizontalInput, 0));
        //_CharacterRigidBody2D.AddForce(new Vector2(_MovementSpeed * _HorizontalInput, 0), ForceMode2D.Force); // <-- Default, gradual force build up applied
        if(_Character.AIType == Character.AITypes.Boss){
            _Character.RigidBody2D.AddForce(new Vector2(_MovementSpeed * _HorizontalMovement, _MovementSpeed * _VerticalMovement), ForceMode2D.Force); // <-- Specified, Immediate force applied        
        }
        
        if (CanMove())
        {
            if(!_SlidingMovement){
                _Character.RigidBody2D.AddForce(new Vector2(_MovementSpeed * _HorizontalMovement, 0), ForceMode2D.Impulse); // <-- Specified, Immediate force applied        
            }else{
                _Character.RigidBody2D.AddForce(new Vector2(_MovementSpeed * _HorizontalMovement, 0), ForceMode2D.Force); // <-- Specified, Immediate force applied        
            }
        
        }

        if(_Character.AIType == Character.AITypes.Boss){
            _Character.RigidBody2D.AddForce(new Vector2(_MovementSpeed * _HorizontalMovement, _MovementSpeed * _VerticalMovement), ForceMode2D.Force); // <-- Specified, Immediate force applied        
        }
    }

    protected override void HandleInput()
    {
        base.HandleInput();

        if (_Character.CharacterType == Character.CharacterTypes.Player)
        {
            _HorizontalMovement = Input.GetAxisRaw("Horizontal");
        }

        if (_Character.CharacterType == Character.CharacterTypes.AI)
        {
            //Debug.Log(_HorizontalMovement);
        }


        if (CanMove())
        {
            _Character.IsMoving = _HorizontalMovement != 0;
            if (_Character.IsMoving)
            {
                if ((_Character.FacingRight && _HorizontalMovement < 0) || !_Character.FacingRight && _HorizontalMovement > 0)
                {
                    FlipCharacter();
                }
            }

            _CharacterAnimation.Movement();
        }
    }

    protected override void InternalInput()
    {
        base.InternalInput();
        // Horizontal and Vertical control is handled by the StateController
    }

    protected override void SetToDefault()
    {
        // NOTE: Be aware that the AI has had its value changed in the Unity editor.
        _MovementSpeed = _DefaultMovementSpeed;
    }

    private void FlipCharacter()
    {
        _CharacterFlip.FlipCharacter();
    }

    public void IncreaseMovementSpeed(float amount, float abilitylength, bool lockout = true)
    {
        // TODO: Might not need. 
    }

    public void MovePosition(Vector2 newPosition)
    {
        // TODO: (Potentially) add option to disable animations while position is being moved 
        //Debug.Log(newPosition);
        _Character.RigidBody2D.MovePosition(newPosition);
    }

    public bool CanMove()
    {
        return !_Character.IsLocked;
    }

    public void Stop()
    {

    }

    public void LockMovement()
    {

    }

    public void UnlockMovement()
    {

    }

    public void SetHorizontal(float value)
    {
        _HorizontalMovement = value;
    }

    public void SetVertical(float value)
    {
        _VerticalMovement = value;
    }
}
