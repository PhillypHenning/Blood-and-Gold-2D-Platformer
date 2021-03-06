using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlags : MonoBehaviour
{
    // Intro cutscene
    public bool BossStarted = false;
    public bool BossIntroStarted = false;
    public bool IntroDone = false;
    [SerializeField] private Transform _BossHeadStartPos;
    public Transform BossHeadStartPos => _BossHeadStartPos;

    [SerializeField] private Transform _BossHeadDeadPos;
    public Transform BossHeadDeadPos => _BossHeadDeadPos;

    [SerializeField] private GameObject _Attack2Prefab;
    public GameObject Attack2Prefab => _Attack2Prefab;

    [SerializeField] private Transform _WeaponHolder;
    public Transform WeaponHolder => _WeaponHolder;
    
    // Movement
    public bool IsMoving = false;
    public bool IsMovingDone = false;

    // Attacking
    public bool IsAttacking = false;
    public float AttackThreshhold = 25f;

    // Attack 1
    public bool Attack1Active = false;
    public float TimeUntilAttack1IsDone = 0;
    public float TimeOfAttack1 = 5f;
    public float TimeUntilAttack1 = 0;
    public float TimeBetweenAttack1 = 3f;
    public bool Attack1Actionable = false;

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

    // Enrage
    public bool _Enrage = false;
    private bool _EnrageActive = false;

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
    public float TimeOfInvunState = 8f;
    public float TimeOfInvun = 2f; 
    public float TimeOfInvunEnds = 6f;

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

    private void Update()
    {
        if(_Enrage && !_EnrageActive){
            _EnrageActive = true;
            TimeOfAttack1 = 8f;
            TimeBetweenAttack1 = 2f;
            TimeOfInvunState = 6f;
            TimeOfInvunEnds = 5f;
            AttackThreshhold = 60f;
        }
    }
}
