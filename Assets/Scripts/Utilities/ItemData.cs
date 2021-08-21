using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "item/Weapon", fileName = "Item Weapon")]

public class ItemData : ScriptableObject
{
    public Weapon _WeaponToEquip;
    public Sprite _WeaponSprite;

}
