using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Detect Target", fileName = "DecisionDetect")]
public class DetectPlayer : AIDecision
{
    private Collider2D _TargetCollider;

    public float _DetectArea = 3f;
    public LayerMask _TargetMask;

    public override bool Decide(StateController controller)
    {
        return CheckTarget(controller);
    }

    private bool CheckTarget(StateController controller){
        _TargetCollider = Physics2D.OverlapCircle(controller.transform.position, _DetectArea, _TargetMask);
        if(_TargetCollider != null){
            controller._Target = _TargetCollider.transform;
            return true;
        }
        return false;
    }
}
