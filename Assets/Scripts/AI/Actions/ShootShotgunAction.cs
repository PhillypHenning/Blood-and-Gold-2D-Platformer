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

        // Look towards Player
        if(controller._Character.FacingRight && controller.transform.position.x > controller._Target.position.x){
            controller._CharacterFlip.FlipCharacter();
        }
        else if(!controller._Character.FacingRight && controller.transform.position.x < controller._Target.position.x){
            controller._CharacterFlip.FlipCharacter();
        }
        
        // Stop Enemy while shooting
        controller._CharacterMovement.SetHorizontal(0);
        controller._CharacterWeapon._CurrentWeapon.UseWeapon(true);
    }
}
