using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon", fileName = "Item Weapon")]

public class WeaponData : ScriptableObject
{
    public Weapon _WeaponToEquip;
    public Sprite _WeaponSprite;

}
