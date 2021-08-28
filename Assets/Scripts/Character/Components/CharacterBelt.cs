using System.Collections;
using UnityEngine;

/* represents the "inventory" of the player */
public class CharacterBelt : CharacterComponent 
{
    private ItemData[] _ItemSlots = new ItemData[2];

    // no item selected
    private int _SelectedItem = -1;

    protected override void HandleInput()
    {
        base.HandleInput();
        HandleItemSelection();
    }

    private void HandleItemSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && _ItemSlots[0] != null)
        {
            // sets or unsets selected item
            _SelectedItem = _SelectedItem != 0 ? 0 : -1;

            print("select item 0 : " + _SelectedItem);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && _ItemSlots[1] != null)
        {
            print("select item 1 : " + _SelectedItem);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && _ItemSlots[2] != null)
        {
            print("select item 2 : " + _SelectedItem);
        }
    }

    private void UseItem()
    {
        // how do we actually USE an item??
        if (_SelectedItem == -1) return;
    }

    private void DropItem()
    {
        if (_SelectedItem == -1) return;
        // item gets removed from inventory
        // item drops on the ground
    }

    private bool AddItem(ItemData item)
    {
        // try to add item to belt
        for (int i = 0; i < _ItemSlots.Length; i++)
        {
            if (_ItemSlots[i] == null)
            {
                // add item to this slot and break loop
                print("add item to slot " + i);
                return true;
            }
        }

        // if we reach here, all item slots are filled
        // "bump" the item
        return false;
    }

}