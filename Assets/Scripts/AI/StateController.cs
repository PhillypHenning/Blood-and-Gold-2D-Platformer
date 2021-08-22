using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{   
    // Brain of the AI

    [Header("State")]
    [SerializeField] private AIState currentState;
    [SerializeField] private AIState remainState;

    private void Update() {
        currentState.EvaluateState(this);
    }

    public void TransitionToState(AIState nextState){
        if(nextState != remainState){
            currentState = nextState;
        }
    }
}
