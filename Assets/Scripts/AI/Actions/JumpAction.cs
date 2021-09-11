using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Jump")]
public class JumpAction : AIAction
{
    public override void Act(StateController controller)
    {
        Jump(controller);
    }

    private void Jump(StateController controller){
        Debug.Log("Jumping");
        controller._CharacterJump.TriggerJump();
    }
}
