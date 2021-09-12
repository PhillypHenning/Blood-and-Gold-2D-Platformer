using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/MinibossAltAttack")]
public class ShotgunAltAttack : AIAction
{
    public override void Act(StateController controller)
    {
        AltAttack(controller);
    }

    private void AltAttack(StateController controller){
        // Look towards Player
        if(controller._Character.FacingRight && controller.transform.position.x > controller._Target.position.x){
            controller._CharacterFlip.FlipCharacter();
        }
        else if(!controller._Character.FacingRight && controller.transform.position.x < controller._Target.position.x){
            controller._CharacterFlip.FlipCharacter();
        }
        
        controller._MiniBossAltAttack.MinibossAltAttackAction(controller._Target);
    }
}
