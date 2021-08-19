using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponent
{
    public float _MovementSpeed { get; set; }
    
    private float _WalkSpeed = 200f; 
    private float _HorizontalInput;

    public bool _CanMove {get; set;} // marked for deletion
    public float _CharacterDirection => _HorizontalInput;

    public bool _MovementSurpressed;

    // TODO: This component will need to know which way the sprite is facing

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
        if(!_Character.IsLocked){
            _CharacterRigidBody2D.AddForce(new Vector2(_MovementSpeed * _HorizontalInput, 0), ForceMode2D.Impulse); // <-- Specified, Immediate force applied        
        }
    }
    
    protected override void HandleInput()
    {
        print("character locked? : " + _Character.IsLocked);
        if (_Character.CharacterType == Character.CharacterTypes.Player && !_Character.IsLocked)
        {
            _HorizontalInput = Input.GetAxisRaw("Horizontal");
            _CharacterAnimation.Movement(_HorizontalInput);
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
        // TODO: (Potentially) add option to disable animations while position is being moved 
        Debug.Log(newPosition);
        _CharacterRigidBody2D.MovePosition(newPosition);
    }

    public void Stop(){

    }

    public void LockMovement(){

    }

    public void UnlockMovement(){
        
    }
}
