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

    private bool _CharacterIsAlive;
    private bool _CharacterIsActionable; // Actionable is used to specify abilities.
    private bool _CharacterMovementLocked; // Movement Locked is to specify movement
    private bool _CharacterIsMoving;
    private bool _CharacterIsFacingRight;
    private bool _CharacterIsGrounded;
    private bool _CharacterIsHitable;

    // Serialized
    [SerializeField] private LayerMask _OriginalLayer;
    [SerializeField] private CharacterTypes _CharacterType;
    [SerializeField] private StateOfInteractions _CharacterStateOfInteraction;

    // Public 
    public bool CharacterIsAlive { get => _CharacterIsAlive; set => _CharacterIsAlive = value; }
    public bool CharacterIsActionable { get => _CharacterIsActionable; set => _CharacterIsActionable = value; }
    public bool CharacterMovementLocked { get => _CharacterMovementLocked; set => _CharacterMovementLocked = value; }
    public bool CharacterIsMoving { get => _CharacterIsMoving; set => _CharacterIsMoving = value; }
    public bool CharacterIsFacingRight { get => _CharacterIsFacingRight; set => _CharacterIsFacingRight = value; }
    public bool CharacterIsGrounded { get => _CharacterIsGrounded; set => _CharacterIsGrounded = value; }
    public bool CharacterIsHitable { get => _CharacterIsHitable; set => _CharacterIsHitable = value; }

    public LayerMask OriginalLayer => _OriginalLayer; // READ-ONLY
    public CharacterTypes CharacterType { get => _CharacterType; set => _CharacterType = value; }
    public StateOfInteractions CharacterStateOfInteraction { get => _CharacterStateOfInteraction; set => _CharacterStateOfInteraction = value; }

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
        CutScene,
        Playing,
        Pause
    }

    private void Start() {
        
    }

    private void Update() {
        
    }
    
}
