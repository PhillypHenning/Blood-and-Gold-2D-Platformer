using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _Animator;
    private Character _Character;

    private bool _IsDead;
    private bool _IsFalling;
    private bool _IsRunning;

    private float _StaticAnimationTime;
    private float _PriorityAnimationTime;


    private Dictionary<AnimationState, float> _AnimationTimes = new Dictionary<AnimationState, float>();
    private AnimationState _CurrentAnimation;

    public Dictionary<AnimationState, float> AnimationTimes => _AnimationTimes;

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
    public enum AnimationState {
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
        Hurt,
        Death,
        ShieldHurt,
        ShieldBreak
    }

    void Start()
    {
        _Animator = GetComponentInChildren<Animator>();
        _Character = GetComponent<Character>();

        if (_Character == null) print("CharacterAnimation couldn't find Character to assign to _Character.");
        if (_Animator == null) print("CharacterAnimation couldn't find Animator component to assign to _Animator.");

        UpdateAnimationTimes();
    }

    // Populates _AnimationTime Dict with animation states and their respective durations
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

        if (_Character.IsGrounded)
        {
            if (_IsFalling)
            {
                _IsFalling = false;
                Landing();
            }
            else if (_IsRunning && !_Character.IsLocked)
            {
                ChangeAnimationState(AnimationState.Run);
            }
            else
            {
                ChangeAnimationState(AnimationState.Idle);
            }
        }
        else if (_Character.RigidBody2D.velocity.y < 0)
        {
            if (_IsFalling)
            {
                ChangeAnimationState(AnimationState.Fall);
            }
            else
            {
                _IsFalling = true;
                ChangeAnimationState(AnimationState.JumpToFall);
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
        if (!_AnimationTimes.ContainsKey(newState)) return;
        if (_CurrentAnimation == newState || PriorityAnimationPlaying()) return;

        _Animator.Play(newState.ToString());

        _CurrentAnimation = newState;

        if (!_AnimationTimes.ContainsKey(newState)) return;

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

    public void Movement()
    {
        if (_Character.IsMoving)
        {
            RunStart();
        }
        else
        {
            RunStop();
        }
    }

    private void RunStart()
    {
        if (!_Character.IsGrounded || _IsRunning) return;
        _IsRunning = true;

        ChangeAnimationState(AnimationState.RunStart, AnimationType.Static);
    }

    private void RunStop()
    {
        if (!_Character.IsGrounded || !_IsRunning) return;
        _IsRunning = false;

        if (_CurrentAnimation != AnimationState.Run) return;
        ChangeAnimationState(AnimationState.RunStop, AnimationType.Static);
    }

    public void Jump()
    {
        ChangeAnimationState(AnimationState.Jump, AnimationType.Priority);
    }

    private void Landing()
    {
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

    public void ShieldHurt()
    {
        ChangeAnimationState(AnimationState.ShieldHurt, AnimationType.Priority); 
    }

    public void ShieldBreak()
    {
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
