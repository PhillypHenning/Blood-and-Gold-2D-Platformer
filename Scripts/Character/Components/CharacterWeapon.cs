using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : CharacterComponent
{
    [Header("Weapon Settings")]
    [SerializeField] private Weapon _WeaponToUse;
    [SerializeField] private Transform _WeaponHolderPosition;

    public Weapon _CurrentWeapon { get; set; }

    protected override void Start()
    {
        base.Start();
        EquipWeapon(_WeaponToUse, _WeaponHolderPosition);
    }

    protected override void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            Aim();
        }   

        if(Input.GetKeyDown(KeyCode.R)){
            Reload();
        }
    }

    public void Shoot(){

    }

    public void Reload(){

    }

    public void EquipWeapon(Weapon weapon, Transform weaponPosition){
        // Instantiates the Weapon that is set in the Unity settings.
        // By default it has been set to the Revolver.
        _CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);

        // Make the instantiated weapon a child object of the Player Gameobject Weapon Holder
        _CurrentWeapon.transform.parent = weaponPosition;
    }

    public void Aim(){
        // Lock the character's movement
        
    }
}
