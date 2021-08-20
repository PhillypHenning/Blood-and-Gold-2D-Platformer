using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Global character state variables
    private bool _IsGrounded;
    private bool _IsLocked;
    private bool _IsMoving;
    private bool _FacingRight = true;

    // public property accessors
    public bool IsLocked { get => _IsLocked; set => _IsLocked = value; }
    public bool IsGrounded { get => _IsGrounded; set => _IsGrounded = value; }
    public bool IsMoving { get => _IsMoving; set => _IsMoving = value; }
    public bool FacingRight { get => _FacingRight; set => _FacingRight = value; }


    // TODO: use character types to determine default stats (like movementspeed)
    // scriptable object or a simple switch case within respective character component setToDefault functions
    public enum CharacterTypes
    {
        Player, 
        AI
    }

    // Class to contain other "Character" GameObjects
    [SerializeField] private CharacterTypes _CharacterType;
    [SerializeField] private GameObject _CharacterSprite;
    private Rigidbody2D _RigidBody2D;

    public CharacterTypes CharacterType => _CharacterType;
    public GameObject CharacterSprite => _CharacterSprite;
    public Rigidbody2D RigidBody2D => _RigidBody2D;

    private void Start()
    {
        _RigidBody2D = GetComponent<Rigidbody2D>();
    }
}
