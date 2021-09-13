using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss/Attack1")]
public class BossAttack1 : AIAction
{
    //private float TimeUntilAttackIsDone = 0;
    private float TimeOfAttack = 5f;

    public override void Act(StateController controller)
    {
        Attack1(controller);
    }

    private void Attack1(StateController controller){
        // Projectile Attack
        if(!controller._BossFlags.IsAttacking){
            controller._BossFlags.IsAttacking = true;
            controller._BossFlags.Attack1Active = true;
            controller._BossFlags.TimeUntilAttackIsDone = Time.time + TimeOfAttack;
            Debug.Log("Attack 1 Started");
        }

        if(controller._BossFlags.Attack1Active){
            //Debug.Log("Using Attack1");
        }

        
        if(controller._BossFlags.Attack1Active && Time.time > controller._BossFlags.TimeUntilAttackIsDone){
            Debug.Log("Attack 1 Finished");
            controller._BossFlags.IsAttacking = false;
            controller._BossFlags.Attack1Active = false;
        }
    }
}
