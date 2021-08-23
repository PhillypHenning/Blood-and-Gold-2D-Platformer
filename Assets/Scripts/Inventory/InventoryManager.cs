using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    // tracks items and their quantity
    private Dictionary<ItemType, int> _Items;

    void Start()
    {
        _Items = new Dictionary<ItemType, int>
        {
            // adds starting items to inventory
            { ItemType.Oil, 100 },
            { ItemType.AmmoHandgun, 12 }
        };
    }

    public bool HasItem(ItemType item)
    {
        return _Items.ContainsKey(item);
    }

    public int GetQuantity(ItemType item)
    {
        if (!HasItem(item)) return 0;
        return _Items[item];
    }

    public bool AddToInventory(ItemData item, int quantity)
    {
        int currentStock = 0;
        if (HasItem(item.Type))
        {
            currentStock = _Items[item.Type];
        }
        else
        {
            _Items.Add(item.Type, currentStock);
        }

        // no room left, item was NOT added to inventory
        // use this logic to determine whether an item pickup should disappear
        if (currentStock == item.MaxStack) return false;

        if (currentStock + quantity >= item.MaxStack)
        {
            _Items[item.Type] = quantity;
        }
        else
        {
            _Items[item.Type] += quantity;
        }

        // item successfully added to inventory
        return true;
    }

    public void RemoveFromInventory(ItemType item, int quantity)
    {
        if (!HasItem(item)) return;

        if (_Items[item] - quantity <= 0)
        {
            _Items.Remove(item);
        }
        else
        {
            _Items[item] -= quantity;
        }
    }
}