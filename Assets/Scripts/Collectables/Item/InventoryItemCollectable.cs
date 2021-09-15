using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemCollectable : Collectable
{
    [Header ("Settings")]
    [SerializeField] private int _ValueToAdd;
    [SerializeField] private ItemData _ItemData;
    // Incase there are different sizes to the value that is added.

    private bool _Consumed = false;

    private InventoryManager _Inventory;

    protected override bool PickUp()
    {
        if (!_Inventory.AddToInventory(_ItemData, _ValueToAdd)) return false;
        _Consumed = true;

        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Items/Revolver_Ammo_Pickup", gameObject);
        return true;
    }

    protected override void SetReferences(GameObject gameObject)
    {
        _Inventory = gameObject.GetComponent<InventoryManager>();
    }

    protected override void DestroySelf()
    {
        if (_Consumed) Destroy(gameObject);
    }
}
