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
            //controller._BossFlags.IsMovingDone = false;
            //Debug.Log("Boss done moving");
            Debug.Log("Boss is moving");
            return true;
        }
        Debug.Log("Boss Done Moving");
        return false;
    }
}
