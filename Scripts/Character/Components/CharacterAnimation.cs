using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _Animator;
    private Rigidbody2D _Player;

    // animation component needs data to know which animations to play

    // TODO: update publicity of these bools, we may house them elsewhere
    public bool _isMoving;
    public bool _isGrounded;
    public bool _isJumping;
    public bool _isTakingDamage;
    public bool _isDead;
    public bool _isFalling;
    public bool _staticAnimation;

    private bool _facingRight = true;

    public bool FacingRight => _facingRight;

    private Dictionary<AnimationState, float> _AnimationTimes = new Dictionary<AnimationState, float>();

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
        Attack1,
        Attack2,
        Attack3,
        Hurt,
        Die
    }

    private AnimationState _CurrentAnimation = AnimationState.None;

    void Start()
    {
        _Animator = GetComponentInChildren<Animator>();
        _Player = GetComponent<Rigidbody2D>();

        if (_Player == null) print("CharacterAnimation couldn't find RigidBody2D to assign to _Player.");
        if (_Animator == null) print("CharacterAnimation couldn't find Animator component to assign to _Animator.");

        UpdateAnimationTimers();
    }

    void Update()
    {
        UpdateAnimation();
    }

    // Sets the animation timers for one-time-play animations
    private void UpdateAnimationTimers()
    {
        AnimationClip[] clips = _Animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            // TODO: update logic... nested looping is ineffecient.
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

    public void ChangeAnimationState(AnimationState newState)
    {
        if (_CurrentAnimation == newState) return;

        _Animator.Play(newState.ToString());

        _CurrentAnimation = newState;
    }

    // handles dynamic animations
    public void UpdateAnimation()
    {
        if (_staticAnimation) return;
        // TODO: We shouldn't need to include the "isJumping" flag, but the current logic 
        // leaves "isGrounded" true for a few frames because the collision detection overlaps
        if (_isGrounded && !_isJumping)
        {
            if (_isMoving)
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
            _isJumping = false;
            if (_isFalling)
            {
                ChangeAnimationState(AnimationState.Fall);
            }
            else
            {
                _isFalling = true;
                ChangeAnimationState(AnimationState.JumpToFall);
                StaticAnimationDelay(_AnimationTimes[AnimationState.JumpToFall]);
            }
        }
    }

    public void FlipCharacter()
    {
        // we could store this globally instead
        var character = transform.Find("Sprite").transform;
        _facingRight = !_facingRight;
        character.localRotation = Quaternion.Euler(character.rotation.x, _facingRight ? 0 : -180, character.rotation.z);
    }

    public void RunStart()
    {
        // TODO: after landing, RunStart should be called again, but if horizontal input is still being read, it will not
        if (!_isGrounded) return;
        _isMoving = true;
        ChangeAnimationState(AnimationState.RunStart);
        StaticAnimationDelay(_AnimationTimes[AnimationState.RunStart]);
    }

    public void RunStop()
    {
        if (!_isGrounded) return;
        _isMoving = false;
        ChangeAnimationState(AnimationState.RunStop);
        StaticAnimationDelay(_AnimationTimes[AnimationState.RunStop]);
    }

    public void Jump()
    {
        _isGrounded = false;
        if (_isJumping) return;
        _isJumping = true;
        ChangeAnimationState(AnimationState.Jump);
    }

    public void Falling()
    {
        // NOT IN USE, logic is now housed within Dynamic Animations
        if (_isFalling) return;
        _isFalling = true;
        ChangeAnimationState(AnimationState.JumpToFall);
        StaticAnimationDelay(_AnimationTimes[AnimationState.JumpToFall]);
    }

    public void Landing()
    {
        if (_isGrounded || _isJumping) return;
        _isGrounded = true;
        _isFalling = false;
        ChangeAnimationState(AnimationState.Landing);
        StaticAnimationDelay(_AnimationTimes[AnimationState.Landing]);
    }

    public void Dodge()
    {
        // not yet functional
        ChangeAnimationState(AnimationState.Dodge);
        StaticAnimationDelay(_AnimationTimes[AnimationState.Dodge]);
    }

    // used for animations that should not be interrupted by dynamic animations
    private void StaticAnimationDelay(float delay)
    {
        _staticAnimation = true;
        StartCoroutine("EndStaticAnimation", delay);
    }

    private IEnumerator EndStaticAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        _staticAnimation = false;
    }
}
