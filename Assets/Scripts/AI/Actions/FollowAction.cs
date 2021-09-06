using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Follow")]
public class FollowAction : AIAction
{
    /*
        Summary
        -------

        1. Get a reference to the CharacterMovement Class to move the character
    */

    [SerializeField] private AIState _RemainState;
    [SerializeField] private AIState _CurrentState;

    public Transform _Target {get; set;}
    public float _MinDistanceToFollow = 1; // Set by Unity settings.

    public override void Act(StateController controller)
    {
        FollowTarget(controller);
    }

    private void FollowTarget(StateController controller){
        if(controller._Target == null){
            return;
        }

        // Horizontal Follow
        if(controller.transform.position.x < controller._Target.position.x){
            controller._CharacterMovement.SetHorizontal(1);
        }
        else{
            controller._CharacterMovement.SetHorizontal(-1);
        }

        //If follow distance is reached stop
        if(Mathf.Abs(controller.transform.position.x - controller._Target.position.x) < _MinDistanceToFollow){
           controller._CharacterMovement.SetHorizontal(0);
        }
    }

}
