using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Actionable state
    // Used to contain metadata of the character state

    // Private
    private float _GameStartTime;
    private float _GameEndTime;

    private bool _CharacterIsAlive = true;
    private bool _CharacterIsActionable; // Actionable is used to specify abilities.
    private bool _CharacterMovementLocked; // Movement Locked is to specify movement
    private bool _CharacterIsMoving;
    private bool _CharacterIsFacingRight = true;
    private bool _CharacterIsGrounded;
    private bool _CharacterIsHitable;

    private StateOfInteractions _CharacterStateOfInteraction;
    private LayerMask _OriginalLayer;
    private Rigidbody2D _CharacterRigidBody2D;

    // Serialized
    [SerializeField] private LayerMask _CurrentLayer;
    [SerializeField] private CharacterTypes _CharacterType;
    [SerializeField] private GameObject _CharacterSprite;

    // Public 
    public bool CharacterIsAlive { get => _CharacterIsAlive; set => _CharacterIsAlive = value; }
    public bool CharacterIsActionable { get => _CharacterIsActionable; set => _CharacterIsActionable = value; }
    public bool CharacterMovementLocked { get => _CharacterMovementLocked; set => _CharacterMovementLocked = value; }
    public bool CharacterIsMoving { get => _CharacterIsMoving; set => _CharacterIsMoving = value; }
    public bool CharacterIsFacingRight { get => _CharacterIsFacingRight; set => _CharacterIsFacingRight = value; }
    public bool CharacterIsGrounded { get => _CharacterIsGrounded; set => _CharacterIsGrounded = value; }
    public bool CharacterIsHitable { get => _CharacterIsHitable; set => _CharacterIsHitable = value; }
    
    public CharacterTypes CharacterType { get => _CharacterType; set => _CharacterType = value; }
    public StateOfInteractions CharacterStateOfInteraction { get => _CharacterStateOfInteraction; set => _CharacterStateOfInteraction = value; }
    public Rigidbody2D CharacterRigidBody2D { get => _CharacterRigidBody2D; set => _CharacterRigidBody2D = value; }
    
    // READ ONLY
    public LayerMask OriginalLayer => _OriginalLayer;
    public GameObject CharacterSprite => _CharacterSprite;

    // Enums
    public enum CharacterTypes {
        Inactive,
        Player,
        AI
    }

    public enum StateOfInteractions {
        // AI
        Inactive,
        Active,
        // Player
        Intro,
        CutScene,
        Playing,
        Pause,
        Locked
    }

    private void Start() {
        if(_CharacterType == CharacterTypes.Player){ CharacterStateOfInteraction = StateOfInteractions.Intro;}
        if(_CharacterType == CharacterTypes.AI){ CharacterStateOfInteraction = StateOfInteractions.Active; }

        _OriginalLayer = _CurrentLayer;

        CharacterRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        
    }
}
