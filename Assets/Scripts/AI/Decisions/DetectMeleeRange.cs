using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Melee Range Detect", fileName = "MeleeRangeDetect")]
public class DetectMeleeRange : AIDecision
{
    public float _MinDistanceToAttack = 3f; // Unity Setting.

    public override bool Decide(StateController controller)
    {
        return PlayerInRangeToAttack(controller);
    }

    private bool PlayerInRangeToAttack(StateController controller){
        if(controller._Target != null){
            //controller._CharacterWeapon._CurrentWeapon.Enable();
            float distanceToPlayer = (controller._Target.position - controller.transform.position).sqrMagnitude;
            return distanceToPlayer < _MinDistanceToAttack;
        }
        controller._CharacterWeapon._CurrentWeapon.Disable();
        return false;
    }
}
