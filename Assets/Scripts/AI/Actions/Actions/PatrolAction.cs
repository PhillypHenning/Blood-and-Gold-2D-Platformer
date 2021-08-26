using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Actions/Patrol")]
public class PatrolAction : AIAction
{   
    private Vector2 _newDirection;

    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller){
        _newDirection = controller._Path.CurrentPoint - controller.transform.position;
        _newDirection = _newDirection.normalized;

        controller._CharacterMovement.SetHorizontal(_newDirection.x);
    }
}
