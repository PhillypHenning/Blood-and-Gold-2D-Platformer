using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : CharacterComponent
{
    [Header("Weapon Settings")]
    // TODO: Update to use programmatic solution over Unity setting. [_WeaponToUse]
    [SerializeField] private Weapon _PrimaryWeapon;
    [SerializeField] private Transform _WeaponHolderPosition;

    public Weapon _CurrentWeapon { get; set; }
    public SpriteRenderer _CurrentWeaponsSprite { get; set; }

    public Weapon _SecondaryWeapon { get; set; }


    protected override void Start()
    {
        base.Start();
        EquipWeapon(_PrimaryWeapon, _WeaponHolderPosition);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_Character.FacingRight)
        {
            _CurrentWeapon.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            _CurrentWeapon.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void HandleInput()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Aim();
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                Shoot();
            }
        }
        else
        {
            StopAim();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.T) && _SecondaryWeapon != null)
        {
            SwapWeapons();
        }
    }

    public void Shoot()
    {
        if (_CurrentWeapon == null)
        {
            return;
        }
        _CurrentWeapon.StartShooting();
    }

    public void Reload()
    {
        if (_CurrentWeapon == null)
        {
            return;
        }

        _CurrentWeapon.Reload();
    }

    public void EquipWeapon(Weapon weapon, Transform weaponPosition)
    {
        if (_CurrentWeapon != null)
        {
            Destroy(GameObject.Find(_CurrentWeapon.GetComponent<ObjectPooler>()._ObjectPooledFullName));
            Destroy(_CurrentWeapon.gameObject);
            Destroy(_CurrentWeapon);
        }
       
        // Instantiates the Weapon that is set in the Unity settings.
        // By default it has been set to the Revolver.
        _CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
        _CurrentWeapon.SetOwner(_Character);
        _CurrentWeapon.ResetProjectileSpawn();

        // Make the instantiated weapon a child object of the Player Gameobject Weapon Holder
        _CurrentWeapon.transform.parent = weaponPosition;

        // Find the gameobject by the Serialized Weapon Name variable (Weapon script)
        GameObject SearchingForWeaponSprite = GameObject.Find(_CurrentWeapon.WeaponName + " Model");
      
        // Disable the sprite until it's being used. 
        _CurrentWeaponsSprite = SearchingForWeaponSprite.GetComponent<SpriteRenderer>();
        _CurrentWeaponsSprite.enabled = false;
    }

    private void Aim()
    {
        // Lock the character's movement

        // Put character into aiming animation

        // Enable the gameobject
        if (_CurrentWeaponsSprite != null)
        {
            _CurrentWeaponsSprite.enabled = true;
        }
    }

    private void StopAim()
    {
        if (_CurrentWeaponsSprite != null)
        {
            _CurrentWeaponsSprite.enabled = false;
        }
    }

    private void SwapWeapons()
    {
        if (_CurrentWeapon.WeaponName == _PrimaryWeapon.WeaponName)
        {
            if (_SecondaryWeapon)
            {
                EquipWeapon(_SecondaryWeapon, _WeaponHolderPosition);
                //_CurrentWeapon = _SecondaryWeapon;
            }
        }
        else
        {
            //_CurrentWeapon = _WeaponToUse;
            EquipWeapon(_PrimaryWeapon, _WeaponHolderPosition);
        }
    }
}
