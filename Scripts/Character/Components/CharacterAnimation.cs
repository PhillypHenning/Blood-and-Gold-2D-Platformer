using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _Animator;
    private Rigidbody2D _Player;
    private Character _Character;

    // TODO: update isGrounded logic... it should not be manually set elsewhere
    //    public bool _isGrounded;

    private bool _IsMoving;
    private bool _IsDead;
    private bool _IsFalling;

    private float _StaticAnimationTime;
    private float _PriorityAnimationTime;

    private bool _FacingRight = true;

    public bool FacingRight => _FacingRight;
    public bool IsMoving => _IsMoving;

    private Dictionary<AnimationState, float> _AnimationTimes = new Dictionary<AnimationState, float>();
    private AnimationState _CurrentAnimation;

    // Dynamic animations are fluid and handeled within the UpdateAnimations function
    // Static animations will prevent dynamic animation logic until it has played through
    // Priority animations will prevent ALL other animations until it has played through
    private enum AnimationType
    {
        Dynamic,
        Static,
        Priority
    }

    /* TODO: store animation clips as Hash ID's (Animator.StringToHash("your_clip_name") to improve performance */
    private enum AnimationState {
        None,
        Idle,
        RunStart,
        Run,
        RunStop,
        Jump,
        JumpToFall,
        Fall,
        Landing,
        Dodge,
        Attack1,
        Attack2,
        Attack3,
        Hurt,
        Death
    }

    void Start()
    {
        _Animator = GetComponentInChildren<Animator>();
        _Player = GetComponent<Rigidbody2D>();
        _Character = GetComponent<Character>();

        if (_Player == null) print("CharacterAnimation couldn't find RigidBody2D to assign to _Player.");
        if (_Animator == null) print("CharacterAnimation couldn't find Animator component to assign to _Animator.");

        UpdateAnimationTimes();
    }

    // Sets the animation timers for one-time-play animations
    private void UpdateAnimationTimes()
    {
        AnimationClip[] clips = _Animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            // TODO: (potentially) update logic... nested looping is ineffecient.
            foreach (AnimationState state in Enum.GetValues(typeof(AnimationState)))
            {
                if (state.ToString() == clip.name)
                {
                    if (_AnimationTimes.ContainsKey(state)) break;
                    _AnimationTimes.Add(state, clip.length);
                }
            }
        }
    }

    void Update()
    {
        UpdateAnimationCooldowns();
        DynamicAnimations();
    }

    // handles dynamic animations
    public void DynamicAnimations()
    {
        if (PriorityAnimationPlaying() || StaticAnimationPlaying() || _IsDead) return;

        if (_Character._IsGrounded)
        {
            if (_IsFalling)
            {
                _IsFalling = false;
                Landing();
            }
            else if (_IsMoving)
            {
                ChangeAnimationState(AnimationState.Run);
            }
            else
            {
                ChangeAnimationState(AnimationState.Idle);
            }
        }
        else if (_Player.velocity.y < 0)
        {
            if (_IsFalling)
            {
                ChangeAnimationState(AnimationState.Fall);
            }
            else
            {
                _IsFalling = true;
                ChangeAnimationState(AnimationState.JumpToFall);
                SetStaticAnimationDelay(_AnimationTimes[AnimationState.JumpToFall]);
            }
        }
    }

    private void UpdateAnimationCooldowns()
    {
        if (StaticAnimationPlaying()) _StaticAnimationTime -= Time.deltaTime;
        if (PriorityAnimationPlaying()) _PriorityAnimationTime -= Time.deltaTime;
    }
    
    private bool StaticAnimationPlaying()
    {
        return _StaticAnimationTime > 0;
    }

    private bool PriorityAnimationPlaying()
    {
        return _PriorityAnimationTime > 0;
    }

    private void ChangeAnimationState(AnimationState newState, AnimationType animationType = AnimationType.Dynamic)
    {
        if (_CurrentAnimation == newState || PriorityAnimationPlaying()) return;

        _Animator.Play(newState.ToString());

        _CurrentAnimation = newState;

        switch (animationType)
        {
            case (AnimationType.Dynamic):
                break;
            case (AnimationType.Static):
                SetStaticAnimationDelay(_AnimationTimes[newState]);
                break;
            case (AnimationType.Priority):
                SetPriorityAnimationDelay(_AnimationTimes[newState]);
                break;
        }
    }

    public void FlipCharacter()
    {
        //var character = transform.Find("Sprite").transform;
        var character = _Character.CharacterSprite.transform;
        _FacingRight = !_FacingRight;
        character.localRotation = Quaternion.Euler(character.rotation.x, _FacingRight ? 0 : -180, character.rotation.z);
    }

    public void RunStart()
    {
        if (!_Character._IsGrounded || IsMoving) return;
        _IsMoving = true;

        ChangeAnimationState(AnimationState.RunStart, AnimationType.Static);
    }

    public void RunStop()
    {
        if (!_Character._IsGrounded || !IsMoving) return;
        _IsMoving = false;

        ChangeAnimationState(AnimationState.RunStop, AnimationType.Static);
    }

    public void Jump()
    {
        ChangeAnimationState(AnimationState.Jump, AnimationType.Priority);
    }

    public void Landing()
    {
        print("we landed");
        ChangeAnimationState(AnimationState.Landing, AnimationType.Static);
    }

    public void Dodge()
    {
        ChangeAnimationState(AnimationState.Dodge, AnimationType.Priority);
    }

    public void Hurt()
    {
        ChangeAnimationState(AnimationState.Hurt, AnimationType.Priority);
    }
    public void Die()
    {
        _IsDead = true;
        ChangeAnimationState(AnimationState.Death, AnimationType.Priority); 
    }

    private void SetStaticAnimationDelay(float delay)
    {
        _StaticAnimationTime = delay;
    }

    private void SetPriorityAnimationDelay(float delay)
    {
        _PriorityAnimationTime = delay;
    }
}
