using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDodge : CharacterComponent
{
    public float _DodgeDistance;
    private float _DodgeDuration = 10f;
    private float _DodgeTimer;
    private bool _IsDodging;
    private bool _DodgeDone = false;

    private float _TimeUntilInvunIsDone = 0f;
    private float _TimeOfInvun = .60f;
    private bool _InvunActivate = false;
    private int _OriginalLayer;

    // Ensure this component is the second in the character gameobject hierarchy (Scripts; 1. Character, 2. CharacterComponents) 

    protected override void Start()
    {
        base.Start();
        SetToDefault();
        _OriginalLayer = gameObject.layer;
        Physics2D.IgnoreLayerCollision(9, 10);
    }

    protected override void Update()
    {
        base.Update();
        if (Time.time > _TimeUntilInvunIsDone){
            if(_InvunActivate){
                _InvunActivate = false;
                //_CharacterHealth.TurnOffMakeInvun();
                ChangeBackToOriginalLayer();
            }
        }
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        if (!_HandleInput) { return; }
        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDodge())
        {
            Dodge();
        }
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();

        if (_IsDodging && _DodgeTimer <= _DodgeDuration)
        {
            _DodgeTimer += Time.deltaTime;
        }
        else if (_IsDodging)
        {
            StopDodge();
        }
    }

    protected override void HandlePhysicsAbility()
    {
        if (_IsDodging)
        {
            // TODO: Lerp it up?
            // total % how far along the dodge is in % multiplied by the distance of dodge
            // this will apply more force at the start and less near its completion
            float dodgeForce = (1 - (_DodgeTimer / _DodgeDuration)) * _DodgeDistance;
            _Character.RigidBody2D.AddForce(new Vector2(dodgeForce * (_Character.FacingRight ? 1 : -1), 0), ForceMode2D.Impulse);
        }
    }

    private void Dodge()
    {
        // TODO: adjust collider to fit sprite animation
        _IsDodging = true;
        _Character.IsLocked = true;
        _DodgeTimer = 0f;
        _CharacterAnimation.Dodge();
        _TimeUntilInvunIsDone = Time.time + _TimeOfInvun;
        _InvunActivate = true;
        //_CharacterHealth.MakeInvun();
        ChangeToRollLayer();
    }

    private void StopDodge()
    {
        _Character.IsLocked = false;
        _IsDodging = false;
    }

    private bool CanDodge()
    {
        return !_Character.IsLocked && _Character.IsGrounded && _Character.IsMoving;
    }

    protected override void SetToDefault()
    {
        //_DodgeDistance = 500f;
        _IsDodging = false;

        // matches dodge duration to length of dodge animation
        if (_CharacterAnimation.AnimationTimes.ContainsKey(CharacterAnimation.AnimationState.Dodge))
        {
            _DodgeDuration = _CharacterAnimation.AnimationTimes[CharacterAnimation.AnimationState.Dodge];
        }
        else
        {
            Debug.LogWarning("CharacterDodge was unable to find Dodge Animation, default 0.5 Dodge Length assigned.");
        }
    }

    private void ChangeToRollLayer(){
        gameObject.layer = 9;
    }

    private void ChangeBackToOriginalLayer(){
        gameObject.layer = _OriginalLayer;
    }
}
