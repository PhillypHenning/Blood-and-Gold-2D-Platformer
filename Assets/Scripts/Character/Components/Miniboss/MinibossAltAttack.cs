using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinibossAltAttack : CharacterComponent
{
    public bool Actionable;
    public bool Attacking;

    private bool _GoodToShoot = false;
    private bool _ChargingAttack = false;
    private bool _SwapBack = false;
    private bool _GoodToSwap = false;

    private float _TimeUntilNextAltAttack = 0;
    private float _TimeUntilAttackStarts = 0;
    private float _TimeUntilAttackIsComplete = 0;

    private float _TimeBetweenAttacks = 15f; //Cooldown
    private float _TimeToStart = 2f; //Charge up (This is needed to avoid destorying in flight objects)
    private float _TimeToCompleteAttack = 6f; // Time the attack takes in total
    private float _SpawnObjectWaitTimer = 1;

    private Transform _Target;
    [SerializeField] private Weapon _WeaponToUse;
    //[SerializeField] private Weapon _AltAttackWeapon;

    protected override void Start()
    {
        base.Start();
        Actionable = true;
        Attacking = false;
        //_TimeUntilNextAttack = 0;
    }

    protected override void Update()
    {
        base.Update();
        if (Time.time > _TimeUntilNextAltAttack)
        {
            Actionable = true;
        }

    }

    public void MinibossAltAttackAction(Transform target)
    {
        // Start animation?

        // Start Coroutine to keep character in spot while attacking
        //StartCoroutine(AltAttack());
        //_CharacterMovement.SetHorizontal(0);
        //_CharacterJump.TriggerJump();
        
        // Target player
        if(_Target == null){
            _Target = target;
        }

        if(!Attacking && Actionable){
            _Character.IsLocked = true;
            Attacking = true;
            Actionable = false;

            _TimeUntilNextAltAttack = Time.time + _TimeBetweenAttacks;
            _TimeUntilAttackIsComplete = Time.time + _TimeToCompleteAttack;
            _TimeUntilAttackStarts = Time.time + _TimeToStart;
            
            // Cas and Weston place effects here
            _ChargingAttack = true;
            if(_CharacterWeapon._SecondaryWeapon != _WeaponToUse){
                _CharacterWeapon._SecondaryWeapon = _WeaponToUse;     
            }    
            // wait "first length time"
            // Initiate attack
            // Finish attack
            _GoodToSwap = true;
        }
        
        if(Attacking && _ChargingAttack && Time.time > _TimeUntilAttackStarts && _GoodToSwap){
            _GoodToSwap = false;
            _CharacterWeapon.SwapWeapons(); // This creates the weapon but the object pooler take a moment to spawn in
            _ChargingAttack = false;
            _GoodToShoot = true;
            
        }

        if(Attacking && Time.time > _TimeUntilAttackStarts + _SpawnObjectWaitTimer && _GoodToShoot){
            _CharacterWeapon._CurrentWeapon.UseWeapon();
            _GoodToShoot = false;
            _SwapBack = true;
        }


        if (Time.time > _TimeUntilAttackIsComplete - _SpawnObjectWaitTimer && Attacking && _SwapBack)
        {
            _CharacterWeapon.SwapWeapons();
            _SwapBack = false;
        }

        if (Time.time > _TimeUntilAttackIsComplete && !Actionable && Attacking)
        {
            _Character.IsLocked = false;
            Attacking = false;
        }

    }
}
