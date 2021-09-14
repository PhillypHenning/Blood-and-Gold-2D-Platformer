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
    public CharacterAnimation _CharacterAnimator { get; set; }
    public Character _Character { get; set; }
    public MinibossAltAttack _MiniBossAltAttack { get; set; }

    public Transform _Target { get; set; }
    public Path _Path { get; set; }
    public Paths _Paths { get; set; }
    public BossFlags _BossFlags { get; set; }

    public Collider2D _Collider2D { get; set; }

    public bool Actionable { get; set; }

    public bool _IntroDone = false;
    private bool TargetSet = false;

    private void Awake()
    {
        Actionable = true;
        _CharacterMovement = GetComponent<CharacterMovement>();
        _CharacterWeapon = GetComponent<CharacterWeapon>();
        _CharacterJump = GetComponent<CharacterJump>();
        _MiniBossAltAttack = GetComponent<MinibossAltAttack>();
        _CharacterFlip = GetComponent<CharacterFlip>();
        _Character = GetComponent<Character>();
        _Collider2D = GetComponent<Collider2D>();
        _CharacterAnimator = GetComponent<CharacterAnimation>();



        // Try and break these up?
        if (_Character.AIType == Character.AITypes.Boss)
        {
            var go = GameObject.Find("BossRoomPath");
            _Paths = go.GetComponent<Paths>();
            _BossFlags = GetComponent<BossFlags>();
        }
    }

    private void Update()
    {
        if (Actionable)
        {
            currentState.EvaluateState(this);
        }
        if(_Target && !TargetSet){
            _CharacterMovement.SetTarget(_Target);
            TargetSet=true;
        }
    }

    public void TransitionToState(AIState nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }

    public void SetLayerToEnemy(){
        gameObject.layer = 8;
    }
    public void ResetLayer(){
        gameObject.layer = 10;
    }
}
