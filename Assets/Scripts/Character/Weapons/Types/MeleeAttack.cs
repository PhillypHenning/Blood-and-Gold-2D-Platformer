using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Weapon
{
    [SerializeField] private float _TimeBetweenHits = 5f;
    [SerializeField] private int _DamageToDeal;
    [SerializeField] private float _ActiveHitboxTime = 0.5f;
    private BoxCollider2D _BoxCollider;
    private float _NextHitTime;

    private float _HitboxEndTime = 0;
    private bool _IsHitboxActive = false;


    protected override void Start()
    {
        base.Start();
        _BoxCollider = GetComponent<BoxCollider2D>();
        _BoxCollider.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
        if (!_IsAttacking || !_IsHitboxActive) return;
        if (Time.time > _HitboxEndTime)
        {
             _BoxCollider.enabled = false;
        }
        if(Time.time > _NextHitTime){
             _CanShoot = true;
             _IsAttacking = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // player is dodging
            if (other.gameObject.layer == 9) return;
            other.GetComponent<CharacterHealth>().Damage(_DamageToDeal);
        }
    }

    public override void UseWeapon(bool doAnimation = true)
    {
        if (_CanShoot && !IsAttacking && _Actionable)
        {

            Attack();
        }

    }

    private void Attack()
    {
        /*
            Though this method does work, I believe the coroutine is fucking up the timing and states. 
        
        // if (_IsAttacking)
        // {
        //     yield break;
        // }

        // // Disables melee box collider after hit is registered
        // _BoxCollider.enabled = false;
        // _IsAttacking = true;

        // yield return new WaitForSeconds(_AttackDelay);

        // _BoxCollider.enabled = true;
        // _IsAttacking = false;
        */

        // _NextShotTime == 0 == first calc
        
        _IsAttacking = true;
        _CanShoot = false;
        _BoxCollider.enabled = true;
        _IsHitboxActive = true;
        _NextHitTime = Time.time + _TimeBetweenHits;
        _HitboxEndTime = Time.time + _ActiveHitboxTime;

        // TODO: Animation added here
        _WeaponOwner.GetComponent<CharacterAnimation>().Attack1();
    }
}
