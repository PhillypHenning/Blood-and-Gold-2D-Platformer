using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Death")]
public class DeathAction : AIAction
{
    public override void Act(StateController controller)
    {
        Death(controller);
    }

    private void Death(StateController controller){
        controller._CharacterMovement.LockMovement();
        controller._CharacterMovement.SetHorizontal(0);
        controller._CharacterMovement.SetVertical(-1);
        controller._Character.IsMoving = false;
        controller.Actionable = false;
    }
}
