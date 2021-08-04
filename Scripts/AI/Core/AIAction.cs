using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : ScriptableObject
{
    [SerializeField] [TextArea(3,15)]
    private string Description;

    public abstract void Act(StateController controller);
}
