using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{   
    // Brain of the AI

    [Header("State")]
    [SerializeField] private AIState currentState;
    [SerializeField] private AIState remainState;

    public CharacterMovement _CharacterMovement { get; set; }
    public CharacterWeapon _CharacterWeapon { get; set; }
    public CharacterJump _CharacterJump { get; set; }
    public CharacterFlip _CharacterFlip { get; set; }
    public Character _Character { get; set; }
    public MinibossAltAttack _MiniBossAltAttack  { get; set; }

    public Transform _Target { get; set; }
    public Path _Path { get; set; }

    public Collider2D _Collider2D { get; set; }

    public bool Actionable { get; set; }

    private void Awake() {
        Actionable = true;
        _CharacterMovement = GetComponent<CharacterMovement>();
        _CharacterWeapon = GetComponent<CharacterWeapon>();
        _CharacterJump = GetComponent<CharacterJump>();
        _MiniBossAltAttack = GetComponent<MinibossAltAttack>();
        _CharacterFlip = GetComponent<CharacterFlip>();
        _Character = GetComponent<Character>();
        
        _Path = GetComponent<Path>();
        _Collider2D = GetComponent<Collider2D>();
    }

    private void Update() {
        if(Actionable){
            currentState.EvaluateState(this);
        }
    }

    public void TransitionToState(AIState nextState){
        if(nextState != remainState){
            currentState = nextState;
        }
    }
}
