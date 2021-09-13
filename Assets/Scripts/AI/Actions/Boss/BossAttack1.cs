using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss/Attack1")]
public class BossAttack1 : AIAction
{
    public override void Act(StateController controller)
    {
        Attack1(controller);
    }

    private void Attack1(StateController controller){
        // Projectile Attack
        Debug.Log("Using Attack1");
    }
}
