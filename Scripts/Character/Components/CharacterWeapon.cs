using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : CharacterComponent
{
    [Header("Weapon Settings")]
    [SerializeField] private Weapon _WeaponToUse;
    [SerializeField] private Transform _WeaponHolderPosition;

    public Weapon _CurrentWeapon { get; set; }
    private SpriteRenderer _CurrentWeaponsSprite;

    protected override void Start()
    {
        base.Start();
        EquipWeapon(_WeaponToUse, _WeaponHolderPosition);
    }

    protected override void HandleInput()
    {
        if(Input.GetKey(KeyCode.LeftControl)){
            Aim();
        }else{
            StopAim();
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

        // Find the gameobject by the Serialized Weapon Name variable (Weapon script)
        GameObject SearchingForWeaponSprite = GameObject.Find(_CurrentWeapon.WeaponName + " Model");

        // Disable the sprite until it's being used. 
        _CurrentWeaponsSprite = SearchingForWeaponSprite.GetComponent<SpriteRenderer>();
        _CurrentWeaponsSprite.enabled = false;
    }

    private void Aim(){
        // Lock the character's movement

        // Put character into aiming animation

        // Enable the gameobject
        _CurrentWeaponsSprite.enabled= true;

        // Changes the gun Direction
        if(_CharacterAnimation.FacingRight){
            _CurrentWeapon.transform.localScale = new Vector3(1,1,1);
        }else{
            _CurrentWeapon.transform.localScale = new Vector3(-1,1,1);
        }
    }

    private void StopAim(){
        _CurrentWeaponsSprite.enabled = false;
    }
}
