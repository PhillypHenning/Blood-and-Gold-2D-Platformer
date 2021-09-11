using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Decide to Jump", fileName = "DecideToJump")]
public class DecideToJump : AIDecision
{
    private Collider2D _TargetCollider;

    public float _DetectArea = 3f;
    public LayerMask _TargetMask;


    public override bool Decide(StateController controller)
    {
        return DecidingToJump(controller);
    }

    private bool DecidingToJump(StateController controller)
    {
        _TargetCollider = Physics2D.OverlapCircle(controller.transform.position, _DetectArea, _TargetMask);
        if (_TargetCollider != null && controller._CharacterJump.CharacterCanJump)
        {   
            controller._Target = _TargetCollider.transform;
            controller._CharacterJump.TriggerJump();
            return true;
        }
        return false;
    }
}