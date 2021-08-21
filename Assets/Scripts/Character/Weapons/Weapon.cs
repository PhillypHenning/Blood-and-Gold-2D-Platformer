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
    private GameObject newG;
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
        newG = GameObject.Find(_WeaponName + " Bullet Spawn Point");
        _ProjectileSpawnPosition = newG.transform.position;

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
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Character/Revolver_Shoot");
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

    // private void WeaponCanReload(){
    //     if(Time.time > _NextReloadTime){
    //         _CanReload = true;
    //         _NextReloadTime = Time.time + _TimeBetweenReloads;
    //     }else{
    //         _CanReload = false;
    //     }
    // }

    private void EvaluateProjectileSpawn()
    {
        if (newG != null)
        {
            ProjectileSpawnPosition = newG.transform.position;
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
//      \\
// PUBLIC \\
// -------- \\
|| ( .) ( .)|| 
||      >   ||
||      _   ||
\\ ________//
    | |
    |*/



    public void Reload()
    {
        // Check if the revolver is full
        if (_CurrentAmmo == _MaxMagazineSize)
        {
            return;
        }
        RefillAmmo();
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Character/Revolver_Reload");
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
        newG = GameObject.Find(_WeaponName + " Bullet Spawn Point");
        _ProjectileSpawnPosition = newG.transform.position;
    }

    public void Destroy()
    {
        Destroy(this);
    }

}
