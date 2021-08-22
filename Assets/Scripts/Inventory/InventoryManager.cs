using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Oil,
    AmmoHangun,
    AmmoShotgun
}

public class InventoryManager : MonoBehaviour
{
    // tracks items and their quantity
    private Dictionary<ItemType, int> _Items;

    void Start()
    {
        _Items = new Dictionary<ItemType, int>();

        // TODO: use data table to access item data
        AddToInventory(ItemType.Oil, 100);
    }

    void Update()
    {

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

    public bool AddToInventory(ItemType item, int quantity)
    {
        int currentStock = 0;
        if (HasItem(item))
        {
            currentStock = _Items[item];
        }
        else
        {
            _Items.Add(item, currentStock);
        }

        // no room left, item was NOT added to inventory
        // use this logic to determine whether an item pickup should disappear
        /*
        if (currentStock == item.MaxQuantity) return false;

        if (currentStock + quantity >= item.MaxQuantity)
        {
            _Items[item.Type] = quantity;
        }
        else
        {
            _Items[item.Type] += quantity;
        }
        */

        // temporary, until item data is sorted out.
        _Items[item] += quantity;

        // item was added to inventory
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