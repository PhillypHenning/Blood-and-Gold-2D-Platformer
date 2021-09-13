using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilRefillCollectable : Collectable
{
        [Header ("Settings")]
    [SerializeField] private int _ValueToAdd = 15;
    // Incase there are different sizes to the value that is added.

    private bool _Consumed = false;

    private CharacterHealth _CharacterHealth;

    protected override void PickUp()
    {
        // Lantern refil here
        _CharacterLantern.AddOil(_ValueToAdd);
        _Consumed = true;

        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/Lantern/Oil_Refill");
    }

    protected override void SetReferences(GameObject gameObject)
    {
        _CharacterHealth = gameObject.GetComponent<CharacterHealth>();
    }

    protected override void DestroySelf()
    {
        if (_Consumed) Destroy(gameObject);
    }
}
