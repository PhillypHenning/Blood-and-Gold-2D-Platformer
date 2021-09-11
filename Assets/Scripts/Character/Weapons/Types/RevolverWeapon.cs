using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverWeapon : Weapon
{
    protected override void Start()
    {
        base.Start();
        if (_WeaponOwner.CharacterType != Character.CharacterTypes.Player) return;
        _WeaponAnimationManager = GameObject.Find("RevolverClip").GetComponent<WeaponAnimationManager>();
    }

    protected override void PlayShootingSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Character/Revolver/Revolver_Shoot");
        // So finally!
        // This is an override, here we are going to specialize the sound we want to use by "overriding" 
        // the template code.
        // This is advantageous because this function will run the templates version of the Reload or Shooting functions 
        // while using our newly overwritten Play<>SFX() function.
    }

    protected override void PlayReloadSFX()
    {
        //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Character/Revolver/Revolver_Reload_Spin");
        // base.PlayReloadSFX(); <-- This would have called our templates PlayReloadSFX function
        // What this allows you to do is have some sort of normalized work in the template class
        // then specializing what you need here. You will see this is many CharacterComponents.
        // For your purposes you'll likely just comment that base out and write your new logic. 
    }

    protected override void PlayUIAnimationShoot()
    {
        if (_WeaponAnimationManager == null) return;
        _WeaponAnimationManager.FireShot();
    }

    protected override void PlayUIAnimationReload()
    {
        if (_WeaponAnimationManager == null) return;
        _WeaponAnimationManager.Reload();
    }
}
