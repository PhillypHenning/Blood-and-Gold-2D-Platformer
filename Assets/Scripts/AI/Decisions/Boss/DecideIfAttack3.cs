using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideIfAttack3 : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return DecideIfAttack2(controller);
    }

    private bool DecideIfAttack2(StateController controller){
        if(controller._BossFlags.MovesUntilAttack2 == 0){
            controller._BossFlags.MovesUntilAttack2 = controller._BossFlags.FixedMovesUntilAttack2;
            return true;
        }
        else return false;
    }
}
