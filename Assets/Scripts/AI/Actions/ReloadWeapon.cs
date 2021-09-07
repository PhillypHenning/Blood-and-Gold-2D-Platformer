using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Reload")]
public class ReloadWeapon : AIAction
{
    public override void Act(StateController controller)
    {
        ReloadAction(controller);
    }

    private void ReloadAction(StateController controller){
        controller._CharacterWeapon._CurrentWeapon.Reload();
    }
}
