using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/MeleeAttackComplete", fileName = "MeleeAttackComplete")]
public class MeleeAttackCompleted : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return MeleeAttackComplete(controller);
    }

    private bool MeleeAttackComplete(StateController controller){
        // In the tutorial the way this was determined was by checking if the Animation was complete
        if(!controller._CharacterWeapon._CurrentWeapon.IsAttacking){
            return true;
        }
        
        return false;
    }
}
