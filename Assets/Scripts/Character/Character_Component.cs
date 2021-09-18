using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Component : MonoBehaviour
{
    protected Character _Character;

    // Start is called before the first frame update
    private void Start()
    {
        _Character = GetComponent<Character>();
    }

    // Calculations, checks and the such should be in here
    private void Update()
    {
        HandleInput();
        HandleBasicComponentFunction();
    }

    // Physics based in here
    private void FixedUpdate() {
        HandlePhysicsComponentFunction();
    }

    protected virtual void HandleInput(){
        if(_Character.CharacterType == Character.CharacterTypes.Player){
            HandlePlayerInput();
        }else if(_Character.CharacterType == Character.CharacterTypes.AI){
            HandleAIInput();
        }else{
            // Just in case.
        }
    }

    protected virtual void HandlePlayerInput(){
        
    }

    protected virtual void HandleAIInput(){

    }

    protected virtual void HandleBasicComponentFunction(){

    }

    protected virtual void HandlePhysicsComponentFunction(){

    }
}
