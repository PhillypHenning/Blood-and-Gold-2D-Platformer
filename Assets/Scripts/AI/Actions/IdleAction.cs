using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Idle")]
public class IdleAction : AIAction
{
    public override void Act(StateController controller)
    {
        controller._CharacterMovement.SetHorizontal(0);
    }
}
