using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // tracks items and their quantity
    private Dictionary<ItemType, int> _Items;

    void Start()
    {
        _Items = new Dictionary<ItemType, int>
        {
            // adds starting items to inventory
            { ItemType.Oil, 100 }
        };
    }

    public bool HasItem(ItemType item)
    {
        return _Items != null ? _Items.ContainsKey(item) : false;
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
            _Items[item.Type] = item.MaxStack;
        }
        else
        {
            _Items[item.Type] += quantity;
        }

        // item successfully added to inventory
        UpdateUI(item);
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

    // TODO: add UI manager to deal with this
    public void UpdateUI(ItemData item)
    {
        switch (item.Type)
        {
            case ItemType.Key:
                GameObject.Find("BossKeyUI").transform.GetChild(0).gameObject.SetActive(true);
                break;
            case ItemType.Lever:
                GameObject.Find("BrakeUI").transform.GetChild(0).gameObject.SetActive(true);
                break;
            default:
                return;
        }
    }
}