using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlags : MonoBehaviour
{
    public bool BossStarted = false;
    public bool IntroDone = false;
    //public bool IntroDone = false;
    public bool IsMoving = false;
    public bool IsMovingDone = false;

    public Vector3 _StartPosition;
    public Vector3 _MovePointTo;
}
