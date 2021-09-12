using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKeyCollectable : Collectable
{
    protected override void PickUp()
    {
        _Character._HasBossKey = true;
    }
}
