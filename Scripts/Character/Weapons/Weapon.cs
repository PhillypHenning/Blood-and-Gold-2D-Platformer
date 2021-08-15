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
    private bool _CanReload = true;
    private float _NextShotTime = 0;
    private float _NextReloadTime = 0;

    // Properties
    public Character _WeaponOwner { get; set; }
    public int _CurrentAmmo { get; set; }
    public bool _CanShoot { get; set; }
    //public bool _CanReload { get; set; }

    public int MagazineSize => _MaxMagazineSize;
    public string WeaponName => _WeaponName;
    public bool CanReload => _CanReload;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    void Awake()
    {
        RefillAmmo();
    }


    void FixedUpdate()
    {
        WeaponCanShoot();
        WeaponCanReload();
    }

    protected void RequestShot(){
        ConsumeAmmo();
    }

    // Private \\
    // -------- \\
    private void TriggerShot(){
        RequestShot();
    }

    private void RefillAmmo(){
        //if(_CanReload){
            _CurrentAmmo = _MaxMagazineSize;
        //}
    }

    private void ConsumeAmmo(){
        //if(_CanShoot){
            _CurrentAmmo -= 1;
        //}
    }

    // I dislike how these work.. 
    private void WeaponCanShoot(){
        if(Time.time > _NextShotTime){
            _CanShoot = true;
            _NextShotTime = Time.time + _TimeBetweenShots;
        }else{
            _CanShoot = false;
        }
    }

    private void WeaponCanReload(){
        if(Time.time > _NextReloadTime){
            _CanReload = true;
            _NextReloadTime = Time.time + _TimeBetweenReloads;
        }else{
            _CanReload = false;
        }
    }

    // PUBLIC \\
    // ------- \\
    public void Reload(){
        RefillAmmo();
    }

    public void StartShooting(){
        if(CanUseWeapon()){
            TriggerShot();
        }
    }

    public void SetOwner(Character character){
        _WeaponOwner = character;
    }

    public bool CanUseWeapon(){
        if(_CurrentAmmo > 0){
            return true;
        }
        return false;
    }
}
