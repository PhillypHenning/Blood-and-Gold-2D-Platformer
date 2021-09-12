using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Decide Melee Attack Again", fileName = "MeleeAttackAgain")]
public class DecideMeleeAttackAgain : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return MeleeAttackAgain(controller);
    }

    private bool MeleeAttackAgain(StateController controller){
        // TODO: (maybe) check if character is alive 
        if(!controller._CharacterWeapon._CurrentWeapon.IsAttacking){

            return true;
        }
        
        return false;
    }
}
