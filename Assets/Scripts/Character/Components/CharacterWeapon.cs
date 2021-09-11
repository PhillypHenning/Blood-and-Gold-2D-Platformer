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
    private bool _Actionable = true;


    protected override void Start()
    {
        base.Start();
        EquipWeapon(_PrimaryWeapon, _WeaponHolderPosition);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_CurrentWeapon == null) { return; }
        if (_Character.FacingRight)
        {
            _CurrentWeapon.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            _CurrentWeapon.transform.localScale = new Vector3(-1, 1, 1);
        }
        if(!_CharacterHealth.IsAlive){
            _Actionable = false;
            _CurrentWeapon._Actionable = false;
        }
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        if (!_HandleInput) { return;}
        if(!_Actionable){ return;}

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

    protected override void InternalInput()
    {
        base.InternalInput();
        if (!_HandleInternalInput) { return; }
        if(!_Actionable){ return;}
        Aim();
    }

    public void Shoot()
    {
        if(!_Actionable){return;}
        if (_CurrentWeapon == null)
        {
            return;
        }
        _CurrentWeapon.UseWeapon();
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
        if (weaponPosition == null) { return; }

        if (_CurrentWeapon != null)
        {
            // If the player switches weapons, while there are bullets out, those bullets are deleted from this. 
            // I can think of two solutions;
            //  1. Create a Destory method in ObjectPooler that waits until all bullets are deleted before destorying itself
            //  2. Add a "switch weapons" timer that doesn't allow the player to shoot his wepaon until the weapon swap is complete. (lasting longer then the bullet life.) 
            //Destroy(GameObject.Find(_CurrentWeapon.GetComponent<ObjectPooler>()._ObjectPooledFullName));
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
        // AI Naming pattern thoughts; <CharacterType>_<GameObjectName>
        // In this case the player model would need to be;
        // Player_Player_Model
        // An AI character would look like;
        // AI_BasicEnemy1

        //GameObject SearchingForWeaponSprite = GameObject.Find(name);
        _CurrentWeaponsSprite = _CurrentWeapon.GetComponentInChildren<SpriteRenderer>();
        if (_CurrentWeaponsSprite)
        {
            _CurrentWeaponsSprite.enabled = false;
        }
        // Disable the sprite until it's being used. 
        //_CurrentWeaponsSprite = SearchingForWeaponSprite.GetComponent<SpriteRenderer>();
        //_CurrentWeaponsSprite.enabled = false;
    }

    private void Aim()
    {
        // Enable the gameobject
        if (_CurrentWeaponsSprite != null)
        {
            _CurrentWeaponsSprite.enabled = true;
        }


        // Change the direction of the shot based on additional keys held
        
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

        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Character/Weapon_Switch");
    }
}
