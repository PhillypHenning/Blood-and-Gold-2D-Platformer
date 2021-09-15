using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCollectable : Collectable
{
    [Header ("Settings")]
    [SerializeField] private int _ValueToAdd = 10;
    // Incase there are different sizes to the value that is added.

    private bool _Consumed = false;

    private CharacterHealth _CharacterHealth;

    protected override bool PickUp()
    {
        if (!_CharacterHealth.Heal(_ValueToAdd)) return false;
        _Consumed = true;
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Items/Health/Restore_Health", gameObject);
        return true;
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
