using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDodge : CharacterComponent
{   
    [Range (0,1000)]
    public float _DodgeDistance = 1000f;
    private float _DodgeDuration = 3f;
    private float _DodgeTimer;
    private bool _CanDodge;
    private bool _isDodging;

    private Vector2 _DodgeOrigin;
    private Vector2 _DodgeDestination;
    private Vector2 _DodgeNewPosition;

    protected override void Start()
    {
        base.Start();
    }

    private void Awake() {
        _CanDodge = true;
        _isDodging = false;
    }

    protected override void HandleInput()
    {
        if(_Character.CharacterType == Character.CharacterTypes.Player && Input.GetKeyDown(KeyCode.LeftShift) && _CanDodge){
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
        _DodgeTimer = 0;

        _CharacterRigidBody2D.AddForce(new Vector2(_DodgeDistance * 1, 0), ForceMode2D.Force);
    }
    
    private void StopDodge(){
        _isDodging = false;
        _CanDodge = true;
        _CharacterMovement._CanMove = true;

    }

}
