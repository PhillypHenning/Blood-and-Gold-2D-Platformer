using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Detect Target", fileName = "DecisionDetect")]
public class DetectPlayer : AIDecision
{
    private Collider2D _TargetCollider;

    public float _DetectArea = 3f;
    public LayerMask _TargetMask;

    private float _CurrentMovementSpeed;

    public override bool Decide(StateController controller)
    {
        return CheckTarget(controller);
    }

    private bool CheckTarget(StateController controller)
    {
        _TargetCollider = Physics2D.OverlapCircle(controller.transform.position, _DetectArea, _TargetMask);
        //_CurrentMovementSpeed = controller._CharacterMovement.HorizontalMovement;
        if (_TargetCollider != null)
        {
            if (controller._Target != null)
            {
                if (controller._Target.position.y < _TargetCollider.transform.position.y)
                {
                    // slow down
                    //Debug.Log(_CurrentMovementSpeed);
                    return false;
                }
            }
            controller._Target = _TargetCollider.transform;
            return true;
        }
        return false;
    }
}
