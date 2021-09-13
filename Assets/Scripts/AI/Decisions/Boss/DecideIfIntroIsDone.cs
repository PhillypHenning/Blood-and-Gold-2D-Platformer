using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/Decisions/Decide If Boss Intro Done ", fileName = "DecideIfBossIntroDone")]
public class DecideIfIntroIsDone : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return BossIntroDone(controller);
    }

    private bool BossIntroDone(StateController controller){
        if(controller._BossFlags.IntroDone){
            return true;
        }
        return false;
    }
}
