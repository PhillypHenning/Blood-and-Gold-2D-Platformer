using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AITransition
{
    [SerializeField] [TextArea(3,15)]
    private string Description;

    // Controls AI transitions from one state to another
    public AIDecision Decision;
    public AIState TrueState;
    public AIState FalseState;
}
