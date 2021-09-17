using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss/Attack1")]
public class BossAttack1 : AIAction
{
    public override void Act(StateController controller)
    {
        Attack1(controller);
    }

    private void Attack1(StateController controller){

        // Projectile Attack
        if(!controller._BossFlags.IsAttacking){
            controller._BossFlags.IsAttacking = true;
            controller._BossFlags.Attack1Active = true;
            controller._BossFlags.TimeUntilAttack1IsDone = Time.time + controller._BossFlags.TimeOfAttack1;
            controller._BossFlags.TimeUntilAttack1 = Time.time - 1; // Set it up to always happen the first time
            controller._BossFlags.Attack1Actionable = true;
        }

        if(controller._BossFlags.Attack1Active && Time.time > controller._BossFlags.TimeUntilAttack1){
            controller._BossFlags.TimeUntilAttack1 = Time.time + controller._BossFlags.TimeBetweenAttack1;
            // play animation1
            controller._CharacterAnimator.Puke();
            controller._CharacterWeapon._CurrentWeapon.UseWeapon();
        }


        if(controller._BossFlags.Attack1Active && Time.time > controller._BossFlags.TimeUntilAttack1IsDone){
            controller._BossFlags.IsAttacking = false;
            controller._BossFlags.Attack1Active = false;
        }
    }
}
