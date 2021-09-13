using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss/MoveToNextPoint")]
public class BossMoveToNextPathPoint : AIAction
{
    private float threshhold = .15f;

    public override void Act(StateController controller)
    {
        MoveToNextPosition(controller);
    }

    private void MoveToNextPosition(StateController controller)
    {
        if (!controller._BossFlags.IsMoving && !controller._BossFlags.IsMovingDone)
        {
            // Randomly select a value between 0 and Paths.count
            var rand = Random.Range(0, controller._Paths._PathCount);
            var moveTowardsPoint = controller._Paths.GetPathPoint(rand);

            // Based on starting position.
            controller._BossFlags._MovePointTo = controller._BossFlags._StartPosition + moveTowardsPoint;
            Debug.Log("Moving towards; " + controller._BossFlags._MovePointTo);
            // Debug.Log("Moving to position: " + movePointTo);
            controller._BossFlags.IsMoving = true;

            //Debug.Log("Simulated movement");
            //controller.transform.position = movePointTo;

            // Move towards point
            //controller._BossFlags.IsMoving = false
        }

        if (controller._BossFlags.IsMoving)
        {
            // Based on the position we are moving towards and the GO's current position
            if (controller.transform.position.x < controller._BossFlags._MovePointTo.x)
            {
                controller._CharacterMovement.SetHorizontal(1);
            }
            else
            {
                controller._CharacterMovement.SetHorizontal(-1);
            }

            // If transform.position is within the range between the point
            // Enumerable.Range(1,100).Contains(x)

            if (IsBetween(controller.transform.position.x, controller._BossFlags._MovePointTo.x + threshhold, controller._BossFlags._MovePointTo.x - threshhold))
            {
                controller._CharacterMovement.SetHorizontal(0);
                controller._BossFlags.IsMoving = false;
                controller._BossFlags.IsMovingDone = true;
            }

            
        }



        // Stop when point is reached
    }

    public bool IsBetween(float testValue, float bound1, float bound2)
    {
        return (testValue >= Mathf.Min(bound1, bound2) && testValue <= Mathf.Max(bound1, bound2));
    }
}
