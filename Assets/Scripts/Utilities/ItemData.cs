using UnityEngine;

public enum ItemType
{
    None,
    Oil,
    AmmoHandgun,
    AmmoShotgun,
    Key,
    Shotgun,
    Lever
}

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Item/InventoryItem", order = 0)]
public class ItemData : ScriptableObject
{
    public ItemType Type;
    public int MaxStack;
    public bool IsStackable;
}