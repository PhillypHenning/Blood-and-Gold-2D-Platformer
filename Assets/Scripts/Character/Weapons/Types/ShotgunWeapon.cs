using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : Weapon
{
    protected override void Start()
    {
        base.Start();
        if (_WeaponOwner.CharacterType != Character.CharacterTypes.Player) return;
        _ShotgunUI = GameObject.Find("ShotgunChamber").GetComponent<ShotgunUI>();
    }

    protected override void PlayShootingSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Character/Shotgun_Shoot");
    }

    protected override void PlayReloadSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Character/Shotgun_Reload");
    }
    public override void PlayUIAnimationShoot()
    {
        if (_ShotgunUI != null)
        {
            _ShotgunUI.FireShot();
        }
    }

    public override void PlayUIAnimationReload()
    {
        if (_ShotgunUI != null)
        {
            _ShotgunUI.Reload();
        }
    }
}
