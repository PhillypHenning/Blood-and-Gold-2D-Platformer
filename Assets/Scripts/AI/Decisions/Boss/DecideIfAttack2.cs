using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Boss/DecideBossAttack2")]
public class DecideIfAttack2 : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return DecideIfAttack2Ready(controller);
    }

    private bool DecideIfAttack2Ready(StateController controller){
        if(controller._BossFlags.MovesUntilAttack2 <= 0){
            controller._BossFlags.MovesUntilAttack2 = controller._BossFlags.FixedMovesUntilAttack2;
            return true;
        }
        else return false;
    }
}
