using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlags : MonoBehaviour
{
    // Intro cutscene
    public bool BossStarted = false;
    public bool BossIntroStarted = false;
    public bool IntroDone = false;
    
    // Movement
    public bool IsMoving = false;
    public bool IsMovingDone = false;

    // Attacking
    public bool IsAttacking = false;
    public float AttackThreshhold = 25f;

    // Attack 1
    public bool Attack1Active = false;
    public float TimeUntilAttackIsDone = 0;

    // Attack 2
    public bool IsAttack2Active = false;
    public bool IsAttack2Done = false;
    public float TimeUntilAttack2IsDone = 0f;
    public float TimeUntilAttack2Active = 0f;
    public float Attack2TimeBetweenAttacks = 2f; // Technically could do a counter as well..
    public float TotalTimeOfAttack2 = 7f;
    public int FixedMovesUntilAttack2 = 3;
    public int MovesUntilAttack2 = 1; // TODO: Switch back after testing
    public int DetectAreaOfAttack2 = 8;
    public float Attack2Damage = 15f;

    // Vulnerable
    public float MovesSinceVunerable = 0;
    public bool VulnerableStarted = false;
    public bool VulnerableFinished = false;
    public bool VulnerableActive = false;
    public float TimeUntilVulnerableStarts = 0;
    public float TimeUntilVulnerableIsDone = 0;
    public bool MoveBossHeadDown = false;
    public bool MoveBossHeadUp = false;
    public float TimeUntilVulnerableFinishes = 0;

    // Death 
    public bool DeathStarted = false;
    public float TimeUntilDeath = 0f;

    public Vector3 _StartPosition;
    public Vector3 _MovePointTo;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, DetectAreaOfAttack2);
    }
}
