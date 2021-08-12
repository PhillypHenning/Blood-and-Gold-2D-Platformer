using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _player;
    private Animator _animator;
    private float _speed = 10.0f;
    private float _jumpPower = 20.0f;
    private float _gravity = -.2f;

    private bool _isGrounded = false;
    private bool _isMoving = false;
    private bool _isAttacking = false;
    private bool _facingRight = true;
    private bool _takingDamage = false;
    private bool _isDead = false;


    private int _combo = 0;

    /* TODO: store animation clips as Hash ID's (Animator.StringToHash("your_clip_name") to improve performance */
    public enum AnimationState {
        Idle,
        Run,
        Jump,
        Fall,
        Attack1,
        Attack2,
        Attack3,
        Block,
        BlockIdle,
        Hurt,
        Die,
        BlockFlash
    }

    private AnimationState _currentAnimation;

    public float Speed => _speed;
    public float JumpPower => _jumpPower;
    public float Gravity => _gravity;
    public bool IsGrounded => _isGrounded;
    public bool IsMoving => _isMoving;
    public bool IsAttacking => _isAttacking;
    public bool FacingRight => _facingRight;
    public bool TakingDamage => _takingDamage;
    public bool IsDead => _isDead;
    public int Combo => _combo;
    public AnimationState CurrentAnimation => _currentAnimation;

    private void Start()
    {
        _player = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        if (_player == null) print("PlayerController couldn't find a Rigidbody2D component to assign to _player.");
        if (_animator == null) print("PlayerController couldn't find Animator component to assign to _animator.");
    }

    void Update()
    {
        // for testing 
        if (Input.GetKeyDown("t")) TakeDamage(0);
        if (Input.GetKeyDown("k")) Die();
        if (Input.GetKeyDown("p")) Spawn();

        if (_isDead) return;
        VerticalVelocity();
        Jump();
        if (!_isAttacking) HorizontalMovement();
        Attack();
        Animations();
    }

    private void Spawn()
    {
        _isDead = false;
        _player.velocity = new Vector2(0, 0);
        transform.position = new Vector3(-15, -5, 0);
    }

    private void Die()
    {
        // TODO: death logic
        _isDead = true;
        _player.velocity = new Vector2(0, 0);
        ChangeAnimationState(AnimationState.Die);
    }

    private void Animations()
    {
        if (_isAttacking || _takingDamage || _isDead) return;
        if (_isGrounded)
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
        else if (_player.velocity.y < 0)
        {
            ChangeAnimationState(AnimationState.Fall);
        }
    }

    void ChangeAnimationState(AnimationState newState)
    {
        if (_currentAnimation == newState) return;

        _animator.Play(newState.ToString());

        _currentAnimation = newState;
    }

    private void VerticalVelocity()
    {
        _player.velocity = new Vector2(_player.velocity.x, _player.velocity.y + _gravity);
    }

    private void Jump()
    {
        // TODO: double jump functionality ?

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _isGrounded = false;
            ChangeAnimationState(AnimationState.Jump);
            _player.velocity = new Vector2(_player.velocity.x, _jumpPower);
        }
    }

    private void Attack()
    {
        if (!_isAttacking)
        {
            // TODO: update logic to use states or switches instead of if-else
            // Left click & left click held
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0))
            {
                _isAttacking = true;
                if (_combo == 0)
                {
                    ChangeAnimationState(AnimationState.Attack1);
                    _combo = 1;

                }
                else if (_combo == 1)
                {
                    ChangeAnimationState(AnimationState.Attack2);
                    // TODO: update combo logic to increment _combo and reset after x time, modulo if's
                    _combo = 0;
                }
            }
            // Right click
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _isAttacking = true;
                ChangeAnimationState(AnimationState.BlockIdle);
                // TODO: add blocking damage animation with effect
                //ChangeAnimationState(AnimationState.Block);
            }
            // Mouse wheel down
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
            {
                _isAttacking = true;
                ChangeAnimationState(AnimationState.Attack3);
            }

            if (_isAttacking)
            {
                float attackDelay = _animator.GetCurrentAnimatorStateInfo(0).length;
                // TODO: add attackDelay functionality (above function yields time that is too lengthy)
                Invoke("AttackComplete", 0.4f);
            }
        }
    }

    private void TakeDamage(int damage)
    {
        _takingDamage = true;
        ChangeAnimationState(AnimationState.Hurt);
        Invoke("TakeDamageComplete", 0.1f);
    }

    private void AttackComplete()
    {
        _isAttacking = false;
    }

    private void TakeDamageComplete()
    {
        _takingDamage = false;
    }

    private void HorizontalMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");

        _isMoving = x != 0;
        _facingRight = x > 0;

        if (_facingRight && x > 0 || !_facingRight && x < 0)
        {
            FlipPlayer();
        }

        _player.velocity = new Vector2(_speed * x, _player.velocity.y);
    }

    private void FlipPlayer()
    {
        _player.transform.localRotation = Quaternion.Euler(transform.rotation.x, _facingRight ? 0 : -180, transform.rotation.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* Not yet implemented fully, currently using Unity collision detection */
        if (collision.transform.CompareTag("Platform"))
        {
            _player.velocity = new Vector2(_player.velocity.x, 0);
            _isGrounded = true;
        }
        else if (collision.transform.CompareTag("Spikes"))
        {
            Die();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            _isGrounded = false;
        }
    }
}
