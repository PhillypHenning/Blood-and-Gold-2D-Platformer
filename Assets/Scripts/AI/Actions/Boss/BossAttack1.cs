using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss/Attack1")]
public class BossAttack1 : AIAction
{
    //private float TimeUntilAttackIsDone = 0;
    private float TimeOfAttack = 5f;
    private float TimeUntilAttack = 0;
    private float TimeBetweenAttacks = 3f;
    private bool Actionable = false;


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
            TimeUntilAttack = Time.time - 1; // Set it up to always happen the first time
            Actionable = true;
        }

        if(controller._BossFlags.Attack1Active && Time.time > TimeUntilAttack){
            TimeUntilAttack = Time.time + TimeBetweenAttacks;
            // play animation1
            controller._CharacterAnimator.Puke();
            controller._CharacterWeapon._CurrentWeapon.UseWeapon();
        }


        if(controller._BossFlags.Attack1Active && Time.time > controller._BossFlags.TimeUntilAttackIsDone){
            controller._BossFlags.IsAttacking = false;
            controller._BossFlags.Attack1Active = false;
        }
    }
}
