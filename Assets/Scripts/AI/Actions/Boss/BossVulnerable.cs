using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss/Vulnerable")]
public class BossVulnerable : AIAction
{
    private float TimeOfInvunState = 10f;
    private float TimeOfInvun = 2f; 
    private float TimeOfInvunEnds = 8f;
    private Vector3 HeadPos;
    // This makes the head vulnerable between TimeOfInvun and TimeOfInvunEnds
    
    public override void Act(StateController controller)
    {
        BecomeVulnerable(controller);
    }

    private void BecomeVulnerable(StateController controller){
        // Drop down 7f;
        // Activate hitbox
        if(!controller._BossFlags.VulnerableStarted){
            Debug.Log("Boss is becoming vulnerable");
            controller._BossFlags.VulnerableStarted = true;
            controller._BossFlags.MoveBossHeadDown = true;
            controller._BossFlags.TimeUntilVulnerableIsDone = Time.time + TimeOfInvunState;
            controller._BossFlags.TimeUntilVulnerableStarts = Time.time + TimeOfInvun;
            controller._BossFlags.TimeUntilVulnerableFinishes = controller._BossFlags.TimeUntilVulnerableIsDone - TimeOfInvun;

            // Move head position down
            HeadPos = controller.transform.position + new Vector3(0, -10, 0);
        }

        if(controller._BossFlags.MoveBossHeadDown){
            //Debug.Log("controller: " + controller.transform.position.y + " | HeadPos.y " + HeadPos.y);
            if(controller.transform.position.y > HeadPos.y){ 
                controller._CharacterMovement.SetVertical(-75);
            // Move head down to position
            }
        }

        if(controller._BossFlags.VulnerableStarted && Time.time > controller._BossFlags.TimeUntilVulnerableStarts){
            //Debug.Log("Boss is Vulnerable");
            controller._BossFlags.MoveBossHeadDown = false;
        }


        if(controller._BossFlags.VulnerableStarted && Time.time > controller._BossFlags.TimeUntilVulnerableFinishes){
            //Debug.Log("Boss is Vulnerable");
            controller._BossFlags.MoveBossHeadUp = true;
        }

        if(controller._BossFlags.MoveBossHeadUp){
            if(controller.transform.position.y < controller._BossFlags._StartPosition.y){ 
                controller._CharacterMovement.SetVertical(150);
            }

            if(controller.transform.position.y >= controller._BossFlags._StartPosition.y){ 
                controller._CharacterMovement.SetVertical(0);
            }
        }

        if(controller._BossFlags.VulnerableStarted && Time.time > controller._BossFlags.TimeUntilVulnerableIsDone){
            Debug.Log("Boss finished Vulnerable");
            controller._BossFlags.VulnerableStarted = false;
            controller._BossFlags.VulnerableFinished = true;
            controller._CharacterMovement.SetVertical(0);
        }
    }
}
