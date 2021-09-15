using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string _WeaponName;
    [SerializeField] private float _TimeBetweenShots = 0.5f;
    [SerializeField] private float _ShotDelay = 0f;
    [SerializeField] private float _TimeBetweenReloads = 0.5f;
    [SerializeField] private int _MaxMagazineSize;
    [SerializeField] private bool _UsesBullets = true;
    [SerializeField] private bool _TrackingBullets = false;
    [SerializeField] protected WeaponAnimationManager _WeaponAnimationManager;
    private Vector3 _ProjectileSpawnPosition;
    private Transform _BulletSpawnPos;
    private bool _CanReload = true;
    private float _NextShotTime = 0;
    private float _NextShotAnimationTime = 0;
    protected bool _IsAttacking;
    private bool _delayedShot = false;

    // Properties
    public Character _WeaponOwner { get; set; }
    public Vector3 ProjectileSpawnPosition { get; set; }
    public ObjectPooler ObjectPooler { get; set; }
    public int _CurrentAmmo { get; set; }
    public bool _CanShoot { get; set; }
    public bool _Actionable = true;
    public bool _IsEmpty;
    public float _TimeModifier = 1.0f;
    //public bool _CanReload { get; set; }

    public int MagazineSize => _MaxMagazineSize;
    public string WeaponName => _WeaponName;
    public bool CanReload => _CanReload;
    public bool IsAttacking => _IsAttacking;

    // Start is called before the first frame update
    virtual protected void Start()
    {

        if (_UsesBullets)
        {
            ObjectPooler = GetComponent<ObjectPooler>();
            // Bad Phil. That is a bad Phil. No. You make things scalable.
            // newG = GameObject.Find("Revolver Bullet Spawn Point");

            _BulletSpawnPos = this.transform.Find(_WeaponName + " Bullet Spawn Point");

            // Because of the Generic naming, the spawn point is being misreferenced. 
            // This needs to find the component that is already attached to this weapon this
            // class is instantiated on. 

            _ProjectileSpawnPosition = _BulletSpawnPos.position;
            //_ProjectileSpawnPosition = GameObject.Find("Bullet Spawn Point")
        }
    }

    virtual protected void Awake()
    {
        RefillAmmo();
        _CanShoot = true;
    }

    virtual protected void FixedUpdate()
    {
        EvaluateProjectileSpawn();
        if (!_Actionable)
        {
            _CanShoot = false;
        }
    }

    virtual protected void Update()
    {
        if (_CurrentAmmo == 0)
        {
            _IsEmpty = true;
        }
        else
        {
            _IsEmpty = false;
        }
        WeaponCanShoot();
    }

    virtual protected void RequestShot()
    {
        ConsumeAmmo();
        SpawnProjectile(ProjectileSpawnPosition);
        // Revolver SFX
        PlayShootingSFX();
        PlayUIAnimationShoot();
        // ^ We place the template function here ^ 
        // When another class specializes using this template, the child script will also run this function.
        // So all we need to do now is take your below code and add it to Play<_>SFX!
        // FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Character/Revolver_Shoot");
        // But wait before you do that, Let's open the RevolverWeapon script
    }
    private IEnumerator RequestShotWithDelay()
    {
        _delayedShot = true;
        yield return new WaitForSeconds(_ShotDelay);
        RequestShot();
        _delayedShot = false;
    }

    virtual protected void PlayUIAnimationShoot()
    {

    }
    virtual protected void PlayUIAnimationReload()
    {

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
    private void TriggerShot(bool doAnimation)
    {

        // Add timer between shots here
        if (_CanShoot && _Actionable)
        {
            _CanShoot = false;
            _NextShotTime = Time.time + _TimeBetweenShots;

            if (doAnimation)
            {
                var animator = _WeaponOwner.GetComponent<CharacterAnimation>();
                animator.Attack1();
            }

            if (_ShotDelay > 0)
            {
                Debug.Log("Hmm.");
                if (_delayedShot) return;
                StartCoroutine("RequestShotWithDelay");
            }
            else
            {
                RequestShot();
            }
        }
    }

    private void RefillAmmo()
    {
        _CurrentAmmo = _MaxMagazineSize;
        _NextShotTime = Time.time + _TimeBetweenShots * _TimeModifier;
        PlayReloadSFX();
        PlayUIAnimationReload();
    }

    private void ConsumeAmmo()
    {
        _CurrentAmmo -= 1;
    }

    private void WeaponCanShoot()
    {
        if (_CanShoot) { return; }

        if (Time.time > _NextShotTime)
        {
            _CanShoot = true;
        }
        else
        {
            _CanShoot = false;
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
        var directional = _WeaponOwner.FacingRight ? transform.right : transform.right * - 1;
        Vector2 newDirection = _WeaponOwner.FacingRight ? transform.right : transform.right * - 1;

        if (!_TrackingBullets)
        {
            projectile.SetDirection(newDirection, transform.rotation, _WeaponOwner.FacingRight);
        }
        else{
            projectile.SetDirection(newDirection, transform.rotation, _WeaponOwner.FacingRight);
        }
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

    public virtual void UseWeapon(bool doAnimation = false)
    {
        if (CanUseWeapon())
        {
            TriggerShot(doAnimation);
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
        if (_UsesBullets)
        {
            _BulletSpawnPos = this.transform.Find(_WeaponName + " Bullet Spawn Point");
            _ProjectileSpawnPosition = _BulletSpawnPos.position;
        }
    }

    public void Disable()
    {
        _CanShoot = false;
        this.enabled = false;
    }

    public void Enable()
    {
        if (!_Actionable) { return; }
        _CanShoot = true;
    }

    public void SetWeaponSpeedMod(float newMod)
    {
        _TimeModifier = newMod;
    }

}
