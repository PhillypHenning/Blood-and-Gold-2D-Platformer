using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Weapon
{
    [SerializeField] private float _TimeBetweenHits = 1f;
    [SerializeField] private int _DamageToDeal;
    private BoxCollider2D _BoxCollider;
    private float _NextHitTime = 0;

    protected override void Start()
    {
        base.Start();
        _BoxCollider = GetComponent<BoxCollider2D>();
        _BoxCollider.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
        if(Time.time > _NextHitTime){
            _CanShoot = true;
            _IsAttacking = false;
            _BoxCollider.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterHealth>().Damage(_DamageToDeal);
        }
    }

    public override void UseWeapon(bool doAnimation = false)
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
        _NextHitTime = Time.time + _TimeBetweenHits;

        // TODO: Animation added here
    }
}
