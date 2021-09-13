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
        if(controller._BossFlags.movesSinceVunerable < 1){
            return false;
        }

        if(controller._BossFlags.movesSinceVunerable == 2){
            var rand = Random.Range(0, 100);
            if(rand < 50){
                controller._BossFlags.movesSinceVunerable = 0;
                controller._BossFlags.VulnerableActive = true;;
                Debug.Log("Returned true?");
                return true;
            }
            return false;
        }

        if(controller._BossFlags.movesSinceVunerable >= 3){
            Debug.Log("Returned true?");
            controller._BossFlags.movesSinceVunerable = 0;
            return true;
        }
        
        return false;
    }
}
