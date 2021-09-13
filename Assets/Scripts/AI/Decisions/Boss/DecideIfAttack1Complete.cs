using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Boss/DecideIsBossAttack1Done")]
public class DecideIfAttack1Complete : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return IsAttack1Done(controller);
    }

    private bool IsAttack1Done(StateController controller){
        if(controller._BossFlags.Attack1Active){
            return false;
        }
        return true;
    }
}
