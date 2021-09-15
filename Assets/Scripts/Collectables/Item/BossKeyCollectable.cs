using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKeyCollectable : Collectable
{
    protected override bool PickUp()
    {
        _Character._HasBossKey = true;
        return true;
    }
}
