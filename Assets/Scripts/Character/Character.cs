using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Global character state variables
    private bool _IsGrounded;
    private bool _IsLocked;
    private bool _ForcedLock;
    private bool _IsMoving;
    private bool _Actionable = true;
    private bool _FacingRight = true;

    // public property accessors
    public bool IsLocked { get => _IsLocked; }
    public bool IsGrounded { get => _IsGrounded; set => _IsGrounded = value; }
    public bool IsMoving { get => _IsMoving; set => _IsMoving = value; }
    public bool FacingRight { get => _FacingRight; set => _FacingRight = value; }
    public bool Actionable { get => _Actionable; set => _Actionable = value; }
    public bool IsAlive = true;
    public bool IsIntro = true;
    public bool _HasIntro = false;
    public bool _HasBossKey = false;
    public float _GameStartTime = 0f;
    public float _GameFinishTime = 0f;
    //public bool _IsFacingRight => _FacingRight;

    public bool HoldingRevolver = false;
    public bool HoldingShotgun = false;

    private float _LockTimeEclapsed = 0f;
    private float _LockEndTime;

    private int _OriginalLayer;


    // TODO: use character types to determine default stats (like movementspeed)
    // scriptable object or a simple switch case within respective character component setToDefault functions
    public enum CharacterTypes
    {
        Player, 
        AI,
        Inactive,
    }

    public enum AITypes
    {
        None,
        Regular, 
        Miniboss,
        Boss,
    }

    // Class to contain other "Character" GameObjects
    [SerializeField] private CharacterTypes _CharacterType;
    [SerializeField] private AITypes _AIType;
    [SerializeField] private GameObject _CharacterSprite;
    private Rigidbody2D _RigidBody2D;
    private Collider2D _Collider2D;

    public CharacterTypes CharacterType {get; set;}
    public AITypes AIType => _AIType;
    public GameObject CharacterSprite => _CharacterSprite;
    public Rigidbody2D RigidBody2D => _RigidBody2D;
    public Collider2D Collider2D => _Collider2D;

    private void Start()
    {
        CharacterType = _CharacterType;
        _OriginalLayer = gameObject.layer;
        //AIType = _AIType;
        _RigidBody2D = GetComponent<Rigidbody2D>();
        _Collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_ForcedLock) return;
        if (_IsLocked && _LockTimeEclapsed < _LockEndTime)
        {
            _LockTimeEclapsed += Time.deltaTime;
        }
        else
        {
            ForceUnlockCharacter();
        }
    }

    public void DeactivateCharacter(){
        CharacterType = CharacterTypes.Inactive;
        Actionable = false;
    }

    public void ReactivateCharacter(){
        CharacterType = _CharacterType;
        Actionable = true;
    }

    public void ForceLockCharacter()
    {
        _IsLocked = true;
        _ForcedLock = true;
    }

    public void LockCharacter(float lockTime)
    {
        _IsLocked = true;
        if (_LockEndTime < lockTime)
        {
            _LockEndTime = lockTime;
        }
    }

    public void ForceUnlockCharacter()
    {
        _IsLocked = false;
        _ForcedLock = false;
        _LockTimeEclapsed = 0;
        _LockEndTime = 0;
    }

    public void ChangeToDeadLayer()
    {
        gameObject.layer = 9;
    }

    public void ChangeToOriginalLayer()
    {
        gameObject.layer = _OriginalLayer;
    }

}
