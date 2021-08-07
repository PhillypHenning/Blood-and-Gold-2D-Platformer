using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Chest : Interactable
{
    protected override bool InputEnabled()
    {
        return Input.GetKeyDown(KeyCode.C);
    }

    protected override void Reward()
    {
        base.Reward();
        Debug.Log("Player Rewarded!");
    }
}
