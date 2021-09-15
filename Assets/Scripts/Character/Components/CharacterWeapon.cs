using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : CharacterComponent
{
    [Header("Weapon Settings")]
    // TODO: Update to use programmatic solution over Unity setting. [_WeaponToUse]
    [SerializeField] private Weapon _PrimaryWeapon;
    //[SerializeField] private Weapon _AltAttackWeapon;
    [SerializeField] private Transform _WeaponHolderPosition;

    private WeaponHolder _WeaponHolder;

    public Weapon _CurrentWeapon { get; set; }
    public SpriteRenderer _CurrentWeaponsSprite { get; set; }

    public Weapon _SecondaryWeapon { get; set; }
    private bool _Actionable = true;

    private float TimeUntilNextReload = 0f;
    private float TimeBetweenReloads = 1.5f;


    protected override void Start()
    {
        base.Start();
        EquipWeapon(_PrimaryWeapon, _WeaponHolderPosition);
        _WeaponHolder = GetComponentInChildren<WeaponHolder>();
        //if(_AltAttackWeapon != null){
        //    _SecondaryWeapon = _AltAttackWeapon;
        // }
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
        if (!_CharacterHealth.IsAlive)
        {
            _Actionable = false;
            _CurrentWeapon._Actionable = false;

            if (_Character.CharacterType == Character.CharacterTypes.AI)
            {
                //_CurrentWeapon.Destroy();
                // Find GameObject "Weapon Holder" 
                if (_WeaponHolder == null) return;
                _WeaponHolder.DestroyWeaponsHeld();
            }
        }
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        if (!_HandleInput) { return; }
        if (!_Actionable) { return; }

        if (Input.GetKey(KeyCode.K))
        {
            Aim();
            if (Input.GetKeyDown(KeyCode.J))
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

        if (Input.GetKeyDown(KeyCode.L) && _SecondaryWeapon != null)
        {
            SwapWeapons();
        }
    }

    protected override void InternalInput()
    {
        base.InternalInput();
        if (!_HandleInternalInput) { return; }
        if (!_Actionable) { return; }
        Aim();
    }

    public void Shoot()
    {
        if (!_Actionable) { return; }
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
            if (_CurrentWeapon._CurrentAmmo != _CurrentWeapon.MagazineSize) {
                _CurrentWeapon.PlayUIAnimationReload();
            }
            // If the player switches weapons, while there are bullets out, those bullets are deleted from this. 
            // I can think of two solutions;
            //  1. Create a Destory method in ObjectPooler that waits until all bullets are deleted before destorying itself
            //  2. Add a "switch weapons" timer that doesn't allow the player to shoot his wepaon until the weapon swap is complete. (lasting longer then the bullet life.) 
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

    public void SwapWeapons()
    {
        // Set time until reload can happen again for the player
        if(_Character.CharacterType == Character.CharacterTypes.Player){
            if(Time.time < TimeUntilNextReload){
                return;
            }
        }

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
        TimeUntilNextReload = Time.time + TimeBetweenReloads;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Character/Weapon_Switch");
    }

    public void UnequiptAll()
    {

        if (_CurrentWeapon.WeaponName == "Claw"){return;}
        
            var objectpoolername = _CurrentWeapon.GetComponent<ObjectPooler>()._ObjectPooledFullName;
            var objectpooler = GameObject.Find(objectpoolername);
            Destroy(objectpooler);
            Destroy(_CurrentWeapon.gameObject);
            Destroy(_CurrentWeapon);
        
    }
}
