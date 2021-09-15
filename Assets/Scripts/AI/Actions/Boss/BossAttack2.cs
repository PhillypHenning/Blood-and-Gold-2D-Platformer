using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss/Attack2")]
public class BossAttack2 : AIAction
{

    private Collider2D _TargetCollider;
    private Transform _Transform;

    public float _DetectArea = 3f;
    public LayerMask _TargetMask;

    public override void Act(StateController controller)
    {
        if(_Transform == null){
            _Transform = controller.transform;
        }
        Attack2(controller);
    }

    private void Attack2(StateController controller)
    {
        if (!controller._BossFlags.IsAttack2Active)
        {
            // Part 1
            controller._BossFlags.IsAttack2Active = true;
            controller._BossFlags.TimeUntilAttack2IsDone = Time.time + controller._BossFlags.TotalTimeOfAttack2;
            controller._BossFlags.TimeUntilAttack2Active = Time.time + controller._BossFlags.Attack2TimeBetweenAttacks;
            // Boss tell would go here
            //controller._CharacterMovement.SetVertical(-5);
        }

        if (controller._BossFlags.IsAttack2Active && Time.time > controller._BossFlags.TimeUntilAttack2Active)
        {
            //controller._CharacterMovement.SetVertical(0);
            controller._BossFlags.TimeUntilAttack2Active = Time.time + controller._BossFlags.Attack2TimeBetweenAttacks;

            // Start Attack2
            _TargetCollider = Physics2D.OverlapCircle(controller.transform.position, controller._BossFlags.DetectAreaOfAttack2, _TargetMask);
            
            // Run animation for yell
            //Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Instantiate(controller._BossFlags.Attack2Prefab, controller._BossFlags.WeaponHolder.position, Quaternion.identity);
        }

        if (_TargetCollider != null)
        {
            var characterHealth = _TargetCollider.GetComponent<CharacterHealth>();
            characterHealth.Damage(controller._BossFlags.Attack2Damage);
            _TargetCollider = null;
        }

        if (controller._BossFlags.IsAttack2Active && Time.time > controller._BossFlags.TimeUntilAttack2IsDone)
        {
            //controller._CharacterMovement.SetVertical(10);
            controller._BossFlags.IsAttack2Active = false;
            controller._BossFlags.IsAttack2Done = true;

            Debug.Log("Attack 2 finished");
        }
    }
}
