using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Weapon
{
    [SerializeField] private float _AttackDelay = 0.5f;
    [SerializeField] private int _DamageToDeal;
    private BoxCollider2D _BoxCollider;


    protected override void Start()
    {
        base.Start();
        _BoxCollider = GetComponent<BoxCollider2D>();
        _BoxCollider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterHealth>().Damage(_DamageToDeal);
        }
    }

    public override void UseWeapon()
    {
        if (_CanShoot)
        {
            StartCoroutine((Attack()));
        }
    }

    private IEnumerator Attack()
    {
        if (_IsAttacking)
        {
            yield break;
        }

        // Disables melee box collider after hit is registered
        _BoxCollider.enabled = false;
        _IsAttacking = true;

        yield return new WaitForSeconds(_AttackDelay);

        _BoxCollider.enabled = true;
        _IsAttacking = false;

        // TODO: Animation added here
    }
}
