using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string _WeaponName;
    [SerializeField] private float _TimeBetweenShots = 0.5f;
    [SerializeField] private float _TimeBetweenReloads = 0.5f;
    [SerializeField] private int _MaxMagazineSize;
    private Vector3 _ProjectileSpawnPosition;
    private Transform _BulletSpawnPos;
    private bool _CanReload = true;
    private float _NextShotTime = 0;
    private float _NextReloadTime = 0;

    // Properties
    public Character _WeaponOwner { get; set; }
    public Vector3 ProjectileSpawnPosition { get; set; }
    public ObjectPooler ObjectPooler { get; set; }
    public int _CurrentAmmo { get; set; }
    public bool _CanShoot { get; set; }
    //public bool _CanReload { get; set; }

    public int MagazineSize => _MaxMagazineSize;
    public string WeaponName => _WeaponName;
    public bool CanReload => _CanReload;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        ObjectPooler = GetComponent<ObjectPooler>();
        // Bad Phil. That is a bad Phil. No. You make things scalable.
        // newG = GameObject.Find("Revolver Bullet Spawn Point");

        _BulletSpawnPos = this.transform.Find(_WeaponName + " Bullet Spawn Point");
        Debug.Log(this.gameObject.name);

        // Because of the Generic naming, the spawn point is being misreferenced. 
        // This needs to find the component that is already attached to this weapon this
        // class is instantiated on. 

        _ProjectileSpawnPosition = _BulletSpawnPos.position;
        //_ProjectileSpawnPosition = GameObject.Find("Bullet Spawn Point")
    }

    virtual protected void Awake()
    {
        RefillAmmo();
    }

    virtual protected void FixedUpdate()
    {
        EvaluateProjectileSpawn();
    }

    virtual protected void RequestShot()
    {
        ConsumeAmmo();
        SpawnProjectile(ProjectileSpawnPosition);
        // Revolver SFX
        PlayShootingSFX();
        // ^ We place the template function here ^ 
        // When another class specializes using this template, the child script will also run this function.
        // So all we need to do now is take your below code and add it to Play<_>SFX!
        // FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Character/Revolver_Shoot");
        // But wait before you do that, Let's open the RevolverWeapon script
    }

    virtual protected void PlayShootingSFX()
    {
        // Cas - Lesson 1
        // We create these little bad bois. 
        // These functions act as templates that we can later specialize. 
        // Read RequestShot or Reload functions for the next part. 
    }

    virtual protected void PlayReloadSFX()
    {

    }

    // Private \\
    // -------- \\
    private void TriggerShot()
    {
        // Add timer between shots here
        RequestShot();
    }

    private void RefillAmmo()
    {
        _CurrentAmmo = _MaxMagazineSize;

        PlayReloadSFX();
    }

    private void ConsumeAmmo()
    {
        _CurrentAmmo -= 1;
    }

    // I dislike how these work.. 
    // private void WeaponCanShoot(){
    //     if(Time.time > _NextShotTime){
    //         _CanShoot = true;
    //         _NextShotTime = Time.time + _TimeBetweenShots;
    //     }else{
    //         _CanShoot = false;
    //     }
    // }

    private void WeaponCanReload()
    {
        if (Time.time > _NextReloadTime)
        {
            _CanReload = true;
            _NextReloadTime = Time.time + _TimeBetweenReloads;
        }
        else
        {
            _CanReload = false;
        }
    }

    private void EvaluateProjectileSpawn()
    {
        if (_BulletSpawnPos != null)
        {
            ProjectileSpawnPosition = _BulletSpawnPos.position;
        }
    }

    private void SpawnProjectile(Vector2 spawnPosition)
    {
        // EvaluateProjectileSpawn();
        // Start by getting a gameobject from the ObjectPooler
        GameObject pooledProjectile = ObjectPooler.GetGameObjectFromPool();

        pooledProjectile.transform.position = ProjectileSpawnPosition;
        pooledProjectile.SetActive(true);

        // Gets the reference to the Projectile class on the GameObject
        // TODO: MOVE THE "FACING RIGHT" out of the fucking animation component.
        // PHILTODO: no more swears </3
        Projectile projectile = pooledProjectile.GetComponent<Projectile>();
        Vector2 newDirection = _WeaponOwner.FacingRight ? transform.right : transform.right * -1;

        projectile.SetDirection(newDirection, transform.rotation, _WeaponOwner.FacingRight);
    }
     /*
    //\\
   //  \\
  //    \\          
 //      \\       /                     \
 // PUBLIC \\    /                       \
 // -------- \\  |                       |
 || ( .) ( .)||  |                       |
<||      >   ||> |                       |
 ||      _   ||  |                       |
 \\ ________//   |                       |_
    | |          |                         \
    | |__________|                        _ \____
    |____________________________________/ |_____\   
    */


    // Dis is our frend Dunceboiiu 
    // He dis good at finding bod cude
    // HI smels bad cuase my socks r unawashhhhed


    public void Reload()
    {
        // Check if the revolver is full
        if (_CurrentAmmo == _MaxMagazineSize)
        {
            return;
        }


        RefillAmmo();
    }

    public void StartShooting()
    {
        if (CanUseWeapon())
        {
            TriggerShot();
        }
    }
    public void SetOwner(Character character)
    {
        _WeaponOwner = character;
    }

    public bool CanUseWeapon()
    {
        if (_CurrentAmmo > 0)
        {
            return true;
        }
        return false;
    }

    public void ResetProjectileSpawn()  
    {
        _BulletSpawnPos = this.transform.Find(_WeaponName + " Bullet Spawn Point");
        //newG = GameObject.Find(_WeaponName + " Bullet Spawn Point");
        _ProjectileSpawnPosition = _BulletSpawnPos.position;
    }

    public void Destroy()
    {
        Destroy(this);
    }



}
