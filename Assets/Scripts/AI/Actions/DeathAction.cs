using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Death")]
public class DeathAction : AIAction
{
    private float DealthDelay = 25f;

    public override void Act(StateController controller)
    {
        Death(controller);
    }

    private void Death(StateController controller){

        if(controller._BossFlags){
            if(!controller._BossFlags.DeathStarted){
                controller._BossFlags.TimeUntilDeath = Time.time + DealthDelay; 
                controller._BossFlags.DeathStarted = true;
            }
            if(controller._BossFlags.DeathStarted && Time.time > controller._BossFlags.TimeUntilDeath){
                controller._CharacterMovement.LockMovement();
                controller._CharacterMovement.SetHorizontal(0);
                controller._CharacterMovement.SetVertical(-1);
                controller._Character.IsMoving = false;
                controller.Actionable = false;
            }

        }
        
        else{
            controller._CharacterMovement.LockMovement();
            controller._CharacterMovement.SetHorizontal(0);
            controller._CharacterMovement.SetVertical(-1);
            controller._Character.IsMoving = false;
            controller.Actionable = false;
        }
    }
}
