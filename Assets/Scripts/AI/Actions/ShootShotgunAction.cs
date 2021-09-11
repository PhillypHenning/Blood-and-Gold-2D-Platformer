using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Shooting")]
public class ShootShotgunAction : AIAction
{
    public override void Act(StateController controller)
    {
        // Could increase time per shot here

        ShootPlayer(controller);
    }

    private void ShootPlayer(StateController controller){

        // Stop Enemy while shooting
        controller._CharacterMovement.SetHorizontal(0);

        controller._CharacterWeapon._CurrentWeapon.UseWeapon();
    }
}
