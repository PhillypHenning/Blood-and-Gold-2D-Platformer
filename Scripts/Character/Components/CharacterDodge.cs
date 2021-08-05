using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDodge : CharacterComponent
{   
    public float _DodgeDistance = 250;
    private float _DodgeDuration = .5f;
    private float _DodgeTimer;
    private bool _CanDodge;
    private bool _isDodging;

    // Ensure this component is the second in the character gameobject hierarchy (Scripts; 1. Character, 2. CharacterComponents) 

    protected override void Start()
    {
        base.Start();
        _CanDodge = true;
        _isDodging = false;
    }

    protected override void HandleInput()
    {
        if(_Character.CharacterType == Character.CharacterTypes.Player && Input.GetKeyDown(KeyCode.LeftShift) && _CanDodge && _CharacterMovement._CharacterDirection  !=0 && _CharacterStats._CanUseAbility){
            // Dodge here  
            Dodge();
        }
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();

        if(_isDodging){
            if(_DodgeTimer < _DodgeDuration){
                //Dodging
                _DodgeTimer += Time.deltaTime;
                _CharacterRigidBody2D.AddForce(new Vector2(_DodgeDistance * _CharacterMovement._CharacterDirection, 0), ForceMode2D.Force);
            }
            else{
                StopDodge();
            }
        }
    }

    private void Dodge(){
        _isDodging = true;
        _CanDodge = false;
        _CharacterMovement._CanMove = false;
        //_Test._CanUseAbility = false; <-This works as long as CharacterComponent is the top most Component in the Character Gameobject. 
        _CharacterStats._CanUseAbility = false;
        _DodgeTimer = 0;
    }
    
    private void StopDodge(){
        _isDodging = false;
        _CanDodge = true;
        _CharacterMovement._CanMove = true;
        _CharacterStats._CanUseAbility = true;
    }

}
