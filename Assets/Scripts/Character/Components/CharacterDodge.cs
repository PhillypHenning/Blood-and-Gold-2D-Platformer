using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDodge : CharacterComponent
{   
    public float _DodgeDistance;
    private float _DodgeDuration = 0.5f;
    private float _DodgeTimer;
    private bool _IsDodging;

    // Ensure this component is the second in the character gameobject hierarchy (Scripts; 1. Character, 2. CharacterComponents) 

    protected override void Start()
    {
        base.Start();
        SetToDefault();
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        if(!_HandleInput){return;}
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
        else
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
            _Character.RigidBody2D.AddForce(new Vector2(dodgeForce * _CharacterMovement.HorizontalMovement, 0), ForceMode2D.Impulse);
        }
    }

    private void Dodge()
    {
        // TODO: adjust collider to fit sprite animation
        _IsDodging = true;
        _Character.IsLocked = true;
        _DodgeTimer = 0f;
        _CharacterAnimation.Dodge();
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
        _DodgeDistance = 200f;
        _IsDodging = false;

        // matches dodge duration to length of dodge animation
        if (_CharacterAnimation.AnimationTimes.ContainsKey(CharacterAnimation.AnimationState.Dodge))
        {
            _DodgeDuration = _CharacterAnimation.AnimationTimes[CharacterAnimation.AnimationState.Dodge];
        } else
        {
            Debug.LogWarning("CharacterDodge was unable to find Dodge Animation, default 0.5 Dodge Length assigned.");
        }
    }
}
