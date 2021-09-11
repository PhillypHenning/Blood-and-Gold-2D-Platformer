using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinibossAltAttack : CharacterComponent
{
    public bool Actionable;
    public bool Attacking;
    private float _TimeUntilNextAltAttack = 0;
    private float _TimeBetweenAttacks = 10f;

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
        if(Time.time > _TimeUntilNextAltAttack){
            
            Actionable = true;
        }

    }

    public void MinibossAltAttackAction(){
        // Start animation?

        // Start Coroutine to keep character in spot while attacking
        //StartCoroutine(AltAttack());
        //_CharacterMovement.SetHorizontal(0);
        //_CharacterJump.TriggerJump();

        _Character.IsLocked = true;
        Attacking = true;
        Actionable = false;

        _TimeUntilNextAltAttack = Time.time + _TimeBetweenAttacks;
        // Whatever actions go here
        Debug.Log("Alt attack");


        _Character.IsLocked = false;
        Attacking = false;

    }

    IEnumerator AltAttack(){
        _Character.IsLocked = true;
        Attacking = true;
        Actionable = false;
        Debug.Log("Alt Attack");
        yield return new WaitForSeconds(5);
        _Character.IsLocked = false;
        Attacking = false;
        Debug.Log("Alt Attack Done");
        yield return new WaitForSeconds(15);
        Actionable = true;
        Debug.Log("Alt Attack Useable again");
    }
}
