using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : ScriptableObject
{
    [SerializeField] [TextArea(3,15)]
    private string Description;

    public abstract bool Decide(StateController controller);
}
