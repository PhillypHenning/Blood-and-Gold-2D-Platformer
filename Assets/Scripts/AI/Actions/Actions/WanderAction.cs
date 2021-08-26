using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Wander")]
public class WanderAction : AIAction
{

    public float _WanderArea = 3f;
    public float _WanderTime = 2f;

    private Vector2 _WanderDirection;
    private float _WanderCheckTime;

    public override void Act(StateController controller)
    {
        Wander(controller);
    }

    private void Wander(StateController controller){
        Debug.Log("Wandering..");
        Debug.Log("Time:" + Time.time + " _WanderCheckTime: " + _WanderCheckTime);
        if(Time.time > _WanderCheckTime){
            Debug.Log("Moving in random direction");
            _WanderDirection.x = Random.Range(-_WanderArea, _WanderArea);

            controller._CharacterMovement.SetHorizontal(_WanderDirection.x);
            _WanderCheckTime = Time.time + _WanderTime;
        }
    }

    void OnEnable()
    {
        _WanderCheckTime = 0;
    }
}
