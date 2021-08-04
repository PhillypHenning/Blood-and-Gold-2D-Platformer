using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private AIState currentState;
    [SerializeField] private AIState remainState;

    public Transform Target { get; set; }
    public CharacterMovement CharacterMovement { get; set; }
    //public CharacterWeapon CharacterWeapon { get; set; }

    public Path Path { get; set; }

    public Collider2D Collider2D { get; set; }

    void Awake()
    {
        CharacterMovement = GetComponent<CharacterMovement>();
        Path = GetComponent<Path>();
        Collider2D = GetComponent<Collider2D>();
        //CharacterWeapon = GetComponent<CharacterWeapon>();
    }

    void Update()
    {
        currentState.EvaluateState(this);
    }

    public void TransitionToState(AIState nextState) 
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }
}
