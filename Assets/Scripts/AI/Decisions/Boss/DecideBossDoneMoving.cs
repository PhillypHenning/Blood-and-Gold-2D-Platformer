using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Decide Is Boss Moving", fileName = "DecideIfBossMoving")]
public class DecideBossDoneMoving : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return IsBossDoneMoving(controller);
    }

    private bool IsBossDoneMoving(StateController controller){
        if(controller._BossFlags.IsMoving){
            controller._BossFlags.IsMovingDone = false;
            return true;
        }
        return false;
    }
}
