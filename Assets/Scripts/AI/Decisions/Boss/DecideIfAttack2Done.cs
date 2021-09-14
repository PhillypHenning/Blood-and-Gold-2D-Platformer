using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Boss/DecideBossAttack2Done")]
public class DecideIfAttack2Done : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return IsAttack2Done(controller);
    }

    private bool IsAttack2Done(StateController controller){
        if(controller._BossFlags.IsAttack2Done){
            controller._BossFlags.IsAttack2Done = false;
            controller._BossFlags.MovesSinceVunerable += 1;
            return true;
        } else return false;
    }
}
