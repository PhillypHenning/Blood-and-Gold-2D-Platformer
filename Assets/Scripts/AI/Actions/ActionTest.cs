using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Test")]
public class ActionTest : AIAction
{
    public override void Act(StateController controller)
    {
        Debug.Log("Acting");
    }
}
