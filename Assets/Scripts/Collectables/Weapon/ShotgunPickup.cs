using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPickup : Collectable
{
    [SerializeField] WeaponData _WeaponItemData;

    protected override void PickUp()
    {
        EquipWeapon();
    }

    private void EquipWeapon(){
        if(_Character != null){
            _Character.GetComponent<CharacterWeapon>()._SecondaryWeapon = _WeaponItemData._WeaponToEquip;
        }
    }
}
