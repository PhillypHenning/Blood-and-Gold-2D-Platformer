using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/ShotgunDecideAltAttack", fileName = "ShotgunDecideAltAttack")]
public class MiniBossDecideToAltAttack : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return TimeForAltAttack(controller);
    }

    private bool TimeForAltAttack(StateController controller){
        // if IsAttacking = false && _CanShoot = false
        if(!controller._CharacterWeapon._CurrentWeapon.IsAttacking && !controller._CharacterWeapon._CurrentWeapon._CanShoot){
            return true;
        }
        return false;
    }
}
