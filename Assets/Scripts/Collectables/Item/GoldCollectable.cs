using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollectable : Collectable
{
    [Header ("Settings")]
    [SerializeField] private int _ValueToAdd = 10; 
    // Incase there are different sizes to the value that is added.

    protected override void PickUp()
    {
        // base.PickUp(); <--Can be removed since there is no base code that would otherwise be called. 
        
        // TODO: Call Player Managr responsible for Coin / Wealth management
    }
}
