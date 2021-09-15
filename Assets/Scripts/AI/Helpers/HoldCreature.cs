using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCreature : MonoBehaviour
{
    [SerializeField] private Transform _Target;
    [SerializeField] private Sprite _MovingSprite;
    [SerializeField] private Sprite _FallSprite;
    private Animator _Animator;
    private Character _TargetCharacter;
    private BossFlags _BossFlags;
    private bool _IsMoving = false;
    private bool _IsVunerable = false;
    private bool _VulnerableWait = false;
    private SpriteRenderer _MovingSpriteRenderer;
    private SpriteRenderer _VulnerableSpriteRenderer;

    public bool IsMoving => _IsMoving;
    public bool IsVunerable => _IsVunerable;

    public Transform Target => _Target;

    void Start()
    {
        _TargetCharacter = _Target.GetComponent<Character>();
        _BossFlags = _Target.GetComponent<BossFlags>();
        _MovingSpriteRenderer = GetComponent<SpriteRenderer>();
        _VulnerableSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!_TargetCharacter.IsAlive){
            Destroy(this);
        }

        _Animator.SetBool("IsMoving", _TargetCharacter.IsMoving);
        _IsVunerable = _BossFlags.VulnerableStarted;
        

        if(_IsVunerable){
            // Change to falling sprite
            _Animator.SetBool("IsMoving", false);
            _Animator.SetBool("FallingDown", true);
        }

        if(_BossFlags.MoveBossHeadUp){
            _Animator.SetBool("FallingDown", false);
            _Animator.SetBool("GettingUp", true);
        }
        if(_BossFlags.VulnerableFinished){
            _Animator.SetBool("GettingUp", false);
        }
    }

    private void FixedUpdate() {
        
        if(!_TargetCharacter.FacingRight && !_IsVunerable){
            transform.position = new Vector3(_Target.position.x+1, transform.position.y, transform.position.z);
        }else{
            transform.position = new Vector3(_Target.position.x-1, transform.position.y, transform.position.z);
        }
        
    }
    

}
