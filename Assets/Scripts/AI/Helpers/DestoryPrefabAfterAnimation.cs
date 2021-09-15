using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryPrefabAfterAnimation : MonoBehaviour
{
    private Animator _Animator;

     
    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!AnimatorIsPlaying()){
            Destroy(gameObject);
        }
    }

    bool AnimatorIsPlaying(){
     return _Animator.GetCurrentAnimatorStateInfo(0).length >
            _Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
  }
}
