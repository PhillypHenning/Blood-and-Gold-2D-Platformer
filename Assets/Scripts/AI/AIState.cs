using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class AIState : ScriptableObject
{
    public AIAction[] _AIActions;
    public AITransition[] _AITransitions;

    public void EvaluateState(StateController controller)
    {
        if (controller == null)
        {
            Debug.Log("AI StateController is null..");
            return;
        }
        PerformActions(controller);
        EvaluateTransitions(controller);
    }

    public void EvaluateTransitions(StateController controller)
    {
        if (_AITransitions != null || _AITransitions.Length > 1)
        {
            for (int i = 0; i < _AITransitions.Length; i++)
            {
                bool decisionResult = _AITransitions[i]._Decision.Decide(controller);
                if (decisionResult)
                {
                    controller.TransitionToState(_AITransitions[i]._TrueState);
                }
                else
                {
                    controller.TransitionToState(_AITransitions[i]._FalseState);
                }
            }
        }
    }

    public void PerformActions(StateController controller)
    {
        if (_AIActions != null)
        {
            foreach (AIAction action in _AIActions)
            {
                //action.Act(controller);
            }
        }
    }
}
