using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Wander")]
public class WanderAction : AIAction
{

    public float _WanderArea = 3f;
    public float _WanderTime = 2f;
    public float _WanderAreaNoMoveZone = 0.75f; // The closer to _WanderArea this becomes, the less likely movement will occur.

    private Vector2 _WanderDirection;
    private float _WanderCheckTime;

    public Vector2 _ObstacleBoxCheckSize = new Vector2(2, 2);
    public LayerMask _ObstacleMask;

    public override void Act(StateController controller)
    {
        Wander(controller);
        EvaluateObstacle(controller);
    }

    private void Wander(StateController controller)
    {
        //Debug.Log("Wandering..");
        //Debug.Log("Time:" + Time.time + " _WanderCheckTime: " + _WanderCheckTime);
        if (Time.time > _WanderCheckTime)
        {
            //Debug.Log("Moving in random direction");
            _WanderDirection.x = Random.Range(-_WanderArea, _WanderArea);

            // If movement range is basically nothing, just remain still
            if (_WanderDirection.x < _WanderAreaNoMoveZone && _WanderDirection.x > -_WanderAreaNoMoveZone)
            {
                controller._CharacterMovement.SetHorizontal(0);
            }
            else
            {

                controller._CharacterMovement.SetHorizontal(_WanderDirection.x);
            }
            _WanderCheckTime = Time.time + _WanderTime;
        }

        //if(controller.transform.position.x == _WanderDirection.x){
        //    controller._CharacterMovement.SetHorizontal(0);
        //}
    }

    private void EvaluateObstacle(StateController controller)
    {
        // Raycasting if obstacle collision in movement
        RaycastHit2D hit = Physics2D.BoxCast(controller._Collider2D.bounds.center, _ObstacleBoxCheckSize, 0f, _WanderDirection, _WanderDirection.magnitude, _ObstacleMask);
        Debug.DrawRay(controller._Collider2D.bounds.center, _WanderDirection);
        // If there is, pick a new direction
        if (hit)
        {
            _WanderDirection.x = Random.Range(-_WanderArea, _WanderArea);

            _WanderCheckTime = Time.time;
        }
    }

    void OnEnable()
    {
        _WanderCheckTime = 0;
    }
}
