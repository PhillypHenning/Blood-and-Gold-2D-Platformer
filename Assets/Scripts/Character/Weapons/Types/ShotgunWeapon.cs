using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : Weapon
{
    protected override void PlayShootingSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Character/Shotgun_Shoot");
    }

    protected override void PlayReloadSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Character/Shotgun_Reload");
    }
}
