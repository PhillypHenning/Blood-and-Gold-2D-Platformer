using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Decide Reload Weapon", fileName = "WeaponReloadDetect")]
public class DecideReloadWeapon : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return DecideToReload(controller);
    }

    private bool DecideToReload(StateController controller){
        if(controller._CharacterWeapon._CurrentWeapon._CurrentAmmo == 0){
            return true;
        }

        return false;
    }
}
