using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : CharacterComponent
{
    private GameObject _WeaponHolder;

    protected override void Start()
    {
        base.Start();
        //_WeaponHolder = GameObject.Find("WeaponHolder");
        
        SingletonExtendedGameObject ExtendedGameObjSingleton = new SingletonExtendedGameObject();
        

        var findItem = ExtendedGameObjSingleton.GetChildWithName(this.gameObject, "WeaponHolder");
        if(findItem != null){
           _WeaponHolder = findItem;
        }
    }

    // Used to flip the Weapon Holder
    protected override void HandleAbility()
    {
        base.HandleAbility();
        if(_Character.FacingRight){
            _WeaponHolder.transform.localPosition = new Vector3(0.4f,0.8f,1);
        }else{
            _WeaponHolder.transform.localPosition = new Vector3(-0.4f,0.8f,1);
        }
    }
}
