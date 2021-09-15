using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PukeAttack : Weapon
{
    protected override void PlayShootingSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Final_Boss/Boss_Vomit_Splash", gameObject);
    }

    protected override void PlayReloadSFX()
    {
        
    }
}
