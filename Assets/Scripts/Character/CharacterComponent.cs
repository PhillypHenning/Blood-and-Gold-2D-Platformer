using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    protected Character _Character;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _Character = GetComponent<Character>();
    }

    // Calculations, checks and the such should be in here
    protected virtual void Update()
    {
        HandleInput();
        HandleBasicComponentFunction();

    }

    // Physics based in here
    private void FixedUpdate() {
        HandlePhysicsComponentFunction();
    }

    protected virtual void HandleInput(){
        HandlePlayerInput();
        HandleAIInput();
    }

    protected virtual bool HandlePlayerInput(){
        return _Character.CharacterType == Character.CharacterTypes.Player;
    }

    protected virtual bool HandleAIInput(){
       return _Character.CharacterType == Character.CharacterTypes.AI;
    }

    protected virtual void HandleBasicComponentFunction(){

    }

    protected virtual void HandlePhysicsComponentFunction(){

    }
}
