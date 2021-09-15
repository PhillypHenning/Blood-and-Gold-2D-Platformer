using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilRefillCollectable : Collectable
{
    /* DEPRECIATED */
        [Header ("Settings")]
    [SerializeField] private int _ValueToAdd = 15;
    // Incase there are different sizes to the value that is added.

    private InventoryManager _InventoryManager;
    private bool _Consumed = false;

    protected override bool PickUp()
    {
        // Lantern refil here
        //_CharacterLantern.AddOil(_ValueToAdd);
        _Consumed = true;

        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/Lantern/Oil_Refill");
        return true;
    }

    protected override void SetReferences(GameObject gameObject)
    {
        _InventoryManager = gameObject.GetComponent<InventoryManager>();
    }

    protected override void DestroySelf()
    {
        if (_Consumed) Destroy(gameObject);
    }
}
