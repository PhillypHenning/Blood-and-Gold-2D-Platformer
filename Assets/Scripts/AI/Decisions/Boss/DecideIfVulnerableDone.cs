using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Boss/DecideIfVulnerableDone", fileName = "DecideIfVulnerableDone")]
public class DecideIfVulnerableDone : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return DecideIfInvunDone(controller);
    }

    private bool DecideIfInvunDone(StateController controller){
        if(!controller._BossFlags.VulnerableStarted && controller._BossFlags.VulnerableFinished){
            return true;
        }
        return false;
    }
}
