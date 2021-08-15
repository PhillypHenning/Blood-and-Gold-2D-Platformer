using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    protected Animator _Animator;

    protected float _StaticAnimationTime;
    protected float _PriorityAnimationTIme;

    protected Dictionary<AnimationState, float> _AnimationTimes = new Dictionary<AnimationState, float>();
    protected AnimationState _CurrentAnimation;

    // Dynamic animations are fluid and handeled within the UpdateAnimations function
    // Static animations will prevent dynamic animation logic until it has played through
    // Priority animations will prevent ALL other animations until it has played through
    private enum AnimationType
    {
        Dynamic,
        Static,
        Priority
    }

    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponentInChildren<Animator>();

        if (_Animator == null) print("CharacterAnimation couldn't find Animator component to assign to _Animator.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
