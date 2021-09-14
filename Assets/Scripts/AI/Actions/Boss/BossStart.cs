using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss/Start")]
public class BossStart : AIAction
{
    //private Vector2 _StartPosition;
    public override void Act(StateController controller)
    {
        SetToStart(controller);
    }

    private void SetToStart(StateController controller){
        controller._BossFlags._StartPosition = controller.transform.position;
        Debug.Log("Current Position: " + controller._Paths.CurrentPosition);
        Debug.Log("BossHead Position: " + controller.transform.position);
        var _StartPosition = controller._Paths.CurrentPosition + controller.transform.position;

        if(controller.transform.position != _StartPosition){
            controller.transform.position = _StartPosition;
            controller._BossFlags._StartPosition = _StartPosition;
        }

        controller._Target = GameObject.Find("Player").transform;
        
        // Set Boss Start bool to false && move to trigger object on door
        controller._BossFlags.BossStarted = false;
    }
}
