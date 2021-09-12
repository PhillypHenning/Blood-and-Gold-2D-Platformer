using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinibossAltAttack : CharacterComponent
{
    public bool Actionable;
    public bool Attacking;
    private float _TimeUntilNextAltAttack = 0;
    private float _TimeBetweenAttacks = 10f;

    private float _TimeToCompleteAttack = 5f;
    private float _TimeUntilAttackIsComplete = 0;

    private Transform _Target;

    protected override void Start()
    {
        base.Start();
        Actionable = true;
        Attacking = false;
        //_TimeUntilNextAttack = 0;
    }

    protected override void Update()
    {
        base.Update();
        if (Time.time > _TimeUntilNextAltAttack)
        {

            Actionable = true;
        }

    }

    public void MinibossAltAttackAction(Transform target)
    {
        // Start animation?

        // Start Coroutine to keep character in spot while attacking
        //StartCoroutine(AltAttack());
        //_CharacterMovement.SetHorizontal(0);
        //_CharacterJump.TriggerJump();
        
        // Target player
        if(_Target == null){
            _Target = target;
        }

        if(!Attacking){
            _Character.IsLocked = true;
            Attacking = true;
            Actionable = false;

            _TimeUntilNextAltAttack = Time.time + _TimeBetweenAttacks;
            _TimeUntilAttackIsComplete = Time.time + _TimeToCompleteAttack;
            
            
            // Cas and Weston place effects here
            Debug.Log("Alt attack Started");
            
            // Put crosshair on character
            


            // wait "first length time"
            // Initiate attack
            // Finish attack
        }
        Debug.Log("Alt attacking");
        if (Time.time > _TimeUntilAttackIsComplete)
        {
            _Character.IsLocked = false;
            Attacking = false;
            Debug.Log("Alt attack complete");
        }

    }
}
