using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss/Start")]
public class BossStart : AIAction
{
    public override void Act(StateController controller)
    {
        SetToStart(controller);
    }

    private void SetToStart(StateController controller)
    {
        if (!controller._BossFlags.BossIntroStarted)
        {
            //var _StartPosition = controller._Paths.CurrentPosition + controller.transform.position;

            if (controller.transform.position != controller._BossFlags.BossHeadStartPos.position)
            {
                controller.transform.position = controller._BossFlags.BossHeadStartPos.position;
            }

            controller._Target = GameObject.Find("Player").transform;
            controller._BossFlags._StartPosition = controller._BossFlags.BossHeadStartPos.position;

            // Set Boss Start bool to false && move to trigger object on door
            controller._BossFlags.BossIntroStarted = true;
        }
    }
}
