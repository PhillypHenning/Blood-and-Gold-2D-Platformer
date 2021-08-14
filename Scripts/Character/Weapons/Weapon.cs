using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   

    public Character _WeaponOwner { get; set; }

    [Header("Settings")]
    [SerializeField] private string _WeaponName;
    [SerializeField] private float _TimeBetweenShots =0.5f;
    [SerializeField] private float _MagazineSize;

    public string WeaponName => _WeaponName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void StartShooting(){

    }

    public void TriggerShot(){
        Debug.Log("Shooting");
    }

    public void SetOwner(Character character){
        _WeaponOwner = character;
    }

    public void Reload(){
        Debug.Log("Reload");
    }
}
