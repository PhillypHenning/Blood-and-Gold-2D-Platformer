using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _Animator;
    private Rigidbody2D _Player;

    // TODO: update isGrounded logic... it should not be manually set elsewhere
    public bool _isGrounded;

    private bool _isMoving;
    private bool _isJumping;
    private bool _isTakingDamage;
    private bool _isDead;
    private bool _isFalling;

    private float _staticAnimationTime;
    private bool _staticAnimation;

    private bool _facingRight = true;

    public bool FacingRight => _facingRight;
    public bool IsMoving => _isMoving;

    private Dictionary<AnimationState, float> _AnimationTimes = new Dictionary<AnimationState, float>();
    private AnimationState _CurrentAnimation;

    public AnimationState CurrentAnimation => _CurrentAnimation;

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
        if (_staticAnimationTime > 0) _staticAnimationTime -= Time.deltaTime;
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
        if (_staticAnimationTime > 0) return;
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
        // maybe we could store this globally instead
        var character = transform.Find("Sprite").transform;
        _facingRight = !_facingRight;
        character.localRotation = Quaternion.Euler(character.rotation.x, _facingRight ? 0 : -180, character.rotation.z);
    }

    public void RunStart()
    {
        if (!_isGrounded) return;
        if (_CurrentAnimation == AnimationState.Run || _CurrentAnimation == AnimationState.RunStart) return;
        _isMoving = true;
        ChangeAnimationState(AnimationState.RunStart);
        StaticAnimationDelay(_AnimationTimes[AnimationState.RunStart]);
    }

    public void RunStop()
    {
        if (!_isGrounded || !_isMoving) return;
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
        // TODO: find a way to cancel coroutine or find another solution
        // currently there is an issue if a second static animation is triggered, the first static animation's delay coroutine
        // will cancel the second animation before it's finished
        // perhaps having a delay timer would be better..?
        _staticAnimation = true;

        _staticAnimationTime = delay;
        //StartCoroutine("EndStaticAnimation", delay);
    }

    private IEnumerator EndStaticAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        _staticAnimation = false;
    }
}
