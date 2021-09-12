using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : CharacterComponent
{
    private GameObject _WeaponHolder;
    [SerializeField] private bool _StartsRight = true;

    protected override void Start()
    {
        base.Start();
        //_WeaponHolder = GameObject.Find("WeaponHolder");
        
        SingletonExtendedGameObject ExtendedGameObjSingleton = new SingletonExtendedGameObject();
        

        var findItem = ExtendedGameObjSingleton.GetChildWithName(this.gameObject, "WeaponHolder");
        if(findItem != null){
           _WeaponHolder = findItem;
        }

        if(!_StartsRight){
            FlipCharacter();
        }
    }

    /*
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
    */

    public void FlipCharacter()
    {
        var character = _Character.CharacterSprite.transform;
        _Character.FacingRight = !_Character.FacingRight;
        character.localRotation = Quaternion.Euler(character.rotation.x, _Character.FacingRight ? 0 : -180, character.rotation.z);

        var weaponHolderPos = _WeaponHolder.transform.localPosition;
        _WeaponHolder.transform.localPosition = new Vector3(weaponHolderPos.x * -1, weaponHolderPos.y, weaponHolderPos.z);
    }
}
