using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Shotgun Range Detect", fileName = "ShotgunRangeDetect")]
public class ShotgunRangeDetect : AIDecision
{
    public float _MinDistanceToAttack = 100f; // Unity Setting.

    public override bool Decide(StateController controller)
    {
        return PlayerInRangeToAttack(controller);
    }

    private bool PlayerInRangeToAttack(StateController controller){
        if(controller._Target != null){
            float distanceToPlayer = (controller._Target.position - controller.transform.position).sqrMagnitude;
            return distanceToPlayer < _MinDistanceToAttack;
        }
        return false;
    }
}
