using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Melee")]
public class MeleeAttackAction : AIAction
{
    public override void Act(StateController controller)
    {
        MeleeAttack(controller);
    }

    private void MeleeAttack(StateController controller){
        //controller._CharacterMovement.SetHorizontal(0);
        controller._CharacterWeapon._CurrentWeapon.UseWeapon();
    }
}
