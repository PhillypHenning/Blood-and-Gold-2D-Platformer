using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Box : Interactable
{
    private Interactable_Component_Health _Interactable_Health;

    protected override void Start() {
        _Interactable_Health = GetComponent<Interactable_Component_Health>();
        base.Start();
    }
    
    protected override bool InputEnabled(){
        if(!_Interactable_Health.CharacterIsAlive()){
            return true;
        }
        return false;
    }


    protected override void Reward()
    {
        base.Reward();
        Debug.Log("Player Rewarded from box!"); 
    }

    protected override void SetToDefault()
    {
        // We do this because _EnableRewardable is by default set by the BoxCollider. Since we only care that the boxes health reach 0, we can disregard this check.
        _EnableRewardable = true;
        base.SetToDefault();
    }
}
