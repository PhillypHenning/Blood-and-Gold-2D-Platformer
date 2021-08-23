using System.Collections;
using UnityEngine;

public class LanternDial : MonoBehaviour
{
    // scrap this, lets not have a reference to player
    // reference to player lantern
    //   - we can calculate the current rotation
    //   - we can know whether or not to show the dial
    // min / max rotations

    // functions
    // - Show / Hide dial (animate into position?)
    // - callibrate to current threshold

    private const float MAX_ROTATION = -30f;
    private const float MIN_ROTATION = 30f;

    private const float OFF_ROTATION = 40f;

    // y position for dial while adjustment mode is on/off
    private const float ADJUSTMENT_MODE_ON = 15f;
    private const float ADJUSTMENT_MODE_OFF = -15f;


    private void Start()
    {
        
    }

    public void AdjustmentModeOn()
    {
        AnimateDial(ADJUSTMENT_MODE_ON);
    }
    public void AdjustmentModeOff()
    {
        AnimateDial(ADJUSTMENT_MODE_OFF);
    }

    public void AnimateDial(float yPos)
    {
        transform.position = new Vector2(transform.position.x, yPos);
    }

    public void SetDialPosition(float percentage)
    {
        var newRotation = transform.rotation.eulerAngles;
        newRotation.z = Mathf.Lerp(MIN_ROTATION, MAX_ROTATION, percentage);

        transform.rotation = Quaternion.Euler(newRotation);
    }

    public void SetDialPositionOff()
    {
        var newRotation = transform.rotation.eulerAngles;
        newRotation.z = OFF_ROTATION;

        transform.rotation = Quaternion.Euler(newRotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}