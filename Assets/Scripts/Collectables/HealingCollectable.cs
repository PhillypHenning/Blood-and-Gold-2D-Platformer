using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCollectable : Collectable
{
    [Header ("Settings")]
    [SerializeField] private int _ValueToAdd = 10; 
    // Incase there are different sizes to the value that is added.

    private CharacterHealth _CharacterHealth;

    protected override void PickUp()
    {
        _CharacterHealth.Heal(_ValueToAdd);
    }

    protected override void SetReferences(GameObject gameObject)
    {
        _CharacterHealth = gameObject.GetComponent<CharacterHealth>();
    }
}
