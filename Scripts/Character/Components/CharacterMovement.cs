using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponent
{
    public float _MovementSpeed { get; set; }
    
    private float _WalkSpeed = 100f; 
    private float _HorizontalInput;

    public bool _CanMove {get; set;}

    protected override void Start()
    {
        base.Start();
        SetToDefault();
        _CanMove = true;
    }

    protected override void HandlePhysicsAbility()
    {   
        // Technique #1: Most Basic Movement
        // _CharacterRigidBody2D.velocity = new Vector2(_MovementSpeed * _HorizontalInput, 0);

        // Techinique #2: Adding Force to the Rigidbody to move in a more gradual way
        // It can feel like sliding on ice. Be cognitive that the Rigidbody mass is weighed in to the function.
        //_CharacterRigidBody2D.AddForce(new Vector2(_MovementSpeed * _HorizontalInput, 0));
        //_CharacterRigidBody2D.AddForce(new Vector2(_MovementSpeed * _HorizontalInput, 0), ForceMode2D.Force); // <-- Default, gradual force build up applied
        if(_CanMove){
            _CharacterRigidBody2D.AddForce(new Vector2(_MovementSpeed * _HorizontalInput, 0), ForceMode2D.Impulse); // <-- Specified, Immediate force applied        
        }
    }
    
    protected override void HandleInput()
    {
        if (_Character.CharacterType == Character.CharacterTypes.Player)
        {
            _HorizontalInput = Input.GetAxisRaw("Horizontal");
        }
    }

    protected override void SetToDefault()
    {
        _MovementSpeed = _WalkSpeed;
    }

    public void IncreaseMovementSpeed(float amount, float abilitylength,  bool lockout = true){
       // TODO: Might not need. 
    }

    public void MovePosition(Vector2 newPosition){
        _CharacterRigidBody2D.MovePosition(newPosition);
    }
}
