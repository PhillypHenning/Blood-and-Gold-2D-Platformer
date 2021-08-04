using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComponent : MonoBehaviour
{
    // Base component that other level components will be based on
    // Level components include;
    //  1. doors
    //  2. platforms

    protected Rigidbody2D _LevelComponentRigidBody;

    protected virtual void Start(){
        _LevelComponentRigidBody = GetComponent<Rigidbody2D>();
    }
    
    protected virtual void Update(){

    }

    protected virtual void FixedUpdate(){

    }

    protected virtual void HandleAbility(){

    }
}   
