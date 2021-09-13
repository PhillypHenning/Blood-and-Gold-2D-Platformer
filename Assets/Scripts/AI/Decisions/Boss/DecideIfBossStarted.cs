using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Boss/DecideIsBossStarted")]
public class DecideIfBossStarted : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return BossStarted(controller);
    }

    private bool BossStarted(StateController controller){
        if(controller._BossFlags.BossStarted){
            return true;
        }
        return false;
    }
}
