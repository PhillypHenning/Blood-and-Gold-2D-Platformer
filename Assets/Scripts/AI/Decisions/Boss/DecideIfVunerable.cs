using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Boss/DecideIfVulnerable", fileName = "DecideIfVulnerable")]
public class DecideIfVunerable : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return DecideVulnerable(controller);
    }

    private bool DecideVulnerable(StateController controller){
        if(controller._BossFlags.MovesSinceVunerable < 1){
            return false;
        }

        if(controller._BossFlags.MovesSinceVunerable == 2){
            var rand = Random.Range(0, 100);
            if(rand < 50){
                controller._BossFlags.MovesSinceVunerable = 0;
                controller._BossFlags.VulnerableActive = true;;
                return true;
            }
            return false;
        }

        if(controller._BossFlags.MovesSinceVunerable >= 3){
            controller._BossFlags.MovesSinceVunerable = 0;
            return true;
        }
        
        return false;
    }
}
