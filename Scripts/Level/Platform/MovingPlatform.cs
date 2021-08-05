using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : LevelComponent
{
    // Get the Platforms current position
    public Transform _Pos1, _Pos2;

    private float _PlatformSpeed = 5.0f;    
    private Vector3 _NextPosition;
    private Transform _Position1, _Position2;


    // Instead of 

    protected override void Start()
    {
        base.Start();
        _NextPosition = _Pos1.position;
        _Position1 = _Pos1;
        _Position2 = _Pos2;
    }

    protected override void Update()
    {   
        //Debug.Log("Transform current position: " + transform.position);
        // if(transform.position== _Pos1.position){
        //     Debug.Log("I didn't happen");
        //     _NextPosition = _Pos2.position;
        // }
        // if(transform.position == _Pos2.position){
        //     _NextPosition = _Pos1.position;
        // }
        Debug.Log("Distance: " + Vector3.Distance(_Pos1.position, transform.position));
        Debug.Log("Pos1: " + _Position1.position + "pos2: " + _Position2.position);
        if(Vector3.Distance(_Pos1.position, transform.position) > Vector3.kEpsilon){
            _NextPosition = _Pos1.position;
        }
        if(Vector3.Distance(_Pos2.position, transform.position) > Vector3.kEpsilon){
            _NextPosition = _Pos2.position;
        }
        
        //Debug.Log("Next Position: " + _NextPosition);
        transform.position = Vector3.MoveTowards(transform.position, _NextPosition, _PlatformSpeed*Time.deltaTime);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(_Pos1.position, _Pos2.position);
    }

}
