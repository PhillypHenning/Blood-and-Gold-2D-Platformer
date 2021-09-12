using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/MinibossAltAttack")]
public class ShotgunAltAttack : AIAction
{
    public override void Act(StateController controller)
    {
        AltAttack(controller);
    }

    private void AltAttack(StateController controller){
        controller._MiniBossAltAttack.MinibossAltAttackAction(controller._Target);
    }
}
