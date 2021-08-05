using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : CharacterComponent
{
    // Keeps track of stats that multiple scripts may use
    public bool _CanUseAbility { get; set; }


    protected override void Start()
    {
        base.Start();
        _CanUseAbility = true;
    }
}
