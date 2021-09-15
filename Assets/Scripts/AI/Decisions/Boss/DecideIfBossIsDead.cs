using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Boss/DecideIfBossIsDead")]
public class DecideIfBossIsDead : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return IsBossDead(controller);
    }

    private bool IsBossDead(StateController controller){
        if(!controller._Character.IsAlive){
            return true;
        }
        return false;
    }
}
