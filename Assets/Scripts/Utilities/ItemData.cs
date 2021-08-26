using UnityEngine;

public enum ItemType
{
    Oil,
    AmmoHandgun,
    AmmoShotgun
}

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Item/InventoryItem", order = 0)]
public class ItemData : ScriptableObject
{
    public ItemType Type;
    public int MaxStack;
    public bool IsStackable;
}