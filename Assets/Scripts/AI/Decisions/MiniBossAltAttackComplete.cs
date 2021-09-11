using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/IsAltAttackOver", fileName = "IsAltAttackOver")]
public class MiniBossAltAttackComplete : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return IsAltAttackOver(controller);
    }

    private bool IsAltAttackOver(StateController controller){
        if(!controller._MiniBossAltAttack.Attacking){
            return true;
        }
        
        return false;
    }
}
