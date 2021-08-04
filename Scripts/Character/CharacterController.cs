using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Controller Class
    /*
        Handles interactions for the Character Rigidbody  

        Basic Functions should include;
            1. Movement
            2. Gravity
    */

    private Rigidbody2D _CharacterRigidBody2D;
    private Vector2 _CurrentMovement { get; set; }
    // public Vector2 CurrentMovement => _CurrentMovement; (RESULTS IN A READ ONLY VARIABLE)


    // Start is called before the first frame update
    void Start()
    {
        _CharacterRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //MoveCharacter(); <-- Was causing issue with Dyanamic Gravity    
    }

    private void MoveCharacter(){
        Vector2 currentMovementPosition = _CharacterRigidBody2D.position + _CurrentMovement * Time.deltaTime;
        _CharacterRigidBody2D.MovePosition(currentMovementPosition);
    }

    public void SetMovement(Vector2 newPosition){
        _CurrentMovement = newPosition;
    }

    public void MovePosition(Vector2 newPosition){
        _CharacterRigidBody2D.MovePosition(newPosition);
    }

    //TODO: Apply Gravity
    // Should Gravity be put in here? 
    // If I put the gravity logic in here, then creatures that fly/hover will need to modify the gravity value upon their creation
    // Alternatively I could create another script that's designed to handle gravity.. Hmm 
}
