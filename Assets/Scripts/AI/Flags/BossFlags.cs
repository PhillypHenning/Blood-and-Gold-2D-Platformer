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

    // Vulnerable
    public float movesSinceVunerable = 0;
    public bool VulnerableStarted = false;
    public bool VulnerableFinished = false;
    public bool VulnerableActive = false;
    public float TimeUntilVulnerableStarts = 0;
    public float TimeUntilVulnerableIsDone = 0;
    public bool MoveBossHeadDown = false;
    public bool MoveBossHeadUp = false;
    public float TimeUntilVulnerableFinishes = 0;


    public Vector3 _StartPosition;
    public Vector3 _MovePointTo;
}
