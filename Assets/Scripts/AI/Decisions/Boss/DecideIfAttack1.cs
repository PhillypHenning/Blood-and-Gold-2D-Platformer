using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Decide If boss attack 1", fileName = "DecideIfBossAttack1")]
public class DecideIfAttack1 : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return DecideIfAttack(controller);
    }

    private bool DecideIfAttack(StateController controller){

        return true;
    }
}
