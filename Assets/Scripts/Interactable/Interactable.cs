using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Review the Collectable for an idea of how to build this
    // This will be a base script for Interactables
    // The differences between an interactable and a collectable are;
    //      1. Interactables lock normal character motion while
    //      2. Are interacted with manually instead of automatically (requires a button press)
    protected Character _Character;
    protected GameObject _CollidingGameObject;
    protected Interactable_Component_Health _InteractableHealth;

    protected bool _EnableRewardable;
    protected bool _RewardIssued = false;


    [SerializeField] public bool _Breakable = false;

    protected virtual void Start(){
        SetToDefault();

        _InteractableHealth = GetComponent<Interactable_Component_Health>();
    }

    protected virtual void Update() {   
        // An input should initiate the interactable
        if(_EnableRewardable && InputEnabled() && !_RewardIssued){
            Reward();
        }
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other) {
        // When Character enters the ontrigger collider
        _CollidingGameObject = other.gameObject;

        if(IsUsableByCollider(other)){
            DisplayVisualQue();
            _EnableRewardable = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        _CollidingGameObject = null;
        _Character = null;
        _EnableRewardable = false;
    }

    protected virtual bool IsUsableByCollider(Collider2D other){
        // Checks the colliding objects tag
        _Character = _CollidingGameObject.GetComponent<Character>();

        if (_Character == null){
            return false;
        }

        return _Character.CharacterType == Character.CharacterTypes.Player && other.CompareTag("Player");
    }

    protected virtual void DisplayVisualQue(){
        // --- TODO
    }

    protected virtual bool InputEnabled(){
        // TODO: Scripts that inherit from this will need to implement that below;
        // return Input.GetKeyDown(Keycode.C);
        // NOTE: DO NOT USE THE BASE OF THIS FUNCTION.
        return false;
    }

    protected virtual void PlayAudio(){
        // ---
    }

    protected virtual void Reward(){
        // ---
        _RewardIssued = true;
    }

    protected virtual void SetToDefault(){
        
    }

    public void DamageInteractable(float damage){
        if(_InteractableHealth != null){
            _InteractableHealth.Damage(damage);
        }
    }
}
