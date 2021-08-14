using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    private Weapon _Weapon;

    // Start is called before the first frame update
    void Start()
    {
        _Weapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConsumeAmmo(){

    }

    public void RefillAmo(){

    }

    public bool CanUseWeapon(){
        
    }
}
