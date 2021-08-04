using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class AIState : ScriptableObject
{
    public AIAction[] Actions;
    public AITransition[] Transitions;


    [SerializeField] [TextArea(3,15)]
    private string Description;

    public void EvaluateState(StateController controller)
    {
        DoActions(controller);
        EvaluateTransition(controller);
    }

    public void DoActions(StateController controller)
    {
        // Calls all of the "Act" Methods in the AIActions variable
        foreach (AIAction action in Actions)
        {
            action.Act(controller);
        }

    }

    public void EvaluateTransition(StateController controller)
    {
        // Calls the "Decide" method for each Transition Decision
        if(Transitions != null || Transitions.Length > 1){
            for (int i = 0; i < Transitions.Length; i++)
            {
                bool decisionResult = Transitions[i].Decision.Decide(controller);
                if(decisionResult){
                    controller.TransitionToState(Transitions[i].TrueState);

                }else{
                    controller.TransitionToState(Transitions[i].FalseState);
                }
            }
        }
    }
}
