using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Decide Reload Weapon", fileName = "WeaponReloadDetect")]
public class DecideReloadWeapon : AIDecision
{
    private float _TimeUntilReload = 0;
    private float _TimeBetweenReloads = 2f;

    public override bool Decide(StateController controller)
    {
        return DecideToReload(controller);
    }

    private bool DecideToReload(StateController controller){
        if(controller._CharacterWeapon._CurrentWeapon._IsEmpty){
            _TimeUntilReload = Time.time + _TimeBetweenReloads;
            return true;
        }

        return false;
    }
}
