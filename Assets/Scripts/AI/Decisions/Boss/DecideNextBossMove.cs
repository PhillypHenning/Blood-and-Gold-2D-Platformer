using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Boss/DecideBossNextMoveAttackOrMove")]
public class DecideNextBossMove : AIDecision
{
    
    // Decide between becoming invunerable or move or another attack
    // if return true, move (50% chance)
    // if return false, return to Boss attack (50% chance)

    public override bool Decide(StateController controller)
    {
        return NextMove(controller);
    }

    private bool NextMove(StateController controller){
        if(controller._BossFlags.VulnerableActive){return false;}
        var rang = Random.Range(0, 100);
        controller._BossFlags.movesSinceVunerable += 1;

        if(rang > controller._BossFlags.AttackThreshhold){
            return true;
        }
        return false;
    }

}
