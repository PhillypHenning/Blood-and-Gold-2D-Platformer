using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Collectable

    protected SpriteRenderer _SpriteRenderer;
    protected Collider2D _Collider2D;

    // GameObject Colliding
    protected Character _Character;
    protected GameObject _CollidingGameObject;
    protected CharacterLantern _CharacterLantern;

    protected TextPopupUI _TextPopupUI;

    public string PickupMessage;

    [Header("Settings")]
    [SerializeField] private bool _DestroyItemOnPickUp = true;
    
    private void Start() {

        _TextPopupUI = GameObject.Find("Dosh_Tracker").GetComponent<TextPopupUI>();
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _Collider2D = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        // if item is a belt item, then check if belt has slots before adding

        _CollidingGameObject = other.gameObject;

        if(IsUsableByPlayer()){
            // Pick up Object
            _CharacterLantern = other.GetComponent<CharacterLantern>();
            SetReferences(_CollidingGameObject);
            if (PickUp()) _TextPopupUI.UpdateText(PickupMessage); 

            if(_DestroyItemOnPickUp){
                DestroySelf();
            }
            else{
                // TODO: This may need to be tweaked. 
                _SpriteRenderer.enabled = false;
                _Collider2D.enabled = false;
            }
        }      
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    protected virtual bool IsUsableByPlayer(){
        // Checks the colliding Objects tag
        _Character = _CollidingGameObject.GetComponent<Character>();

        if(_Character == null){
            return false;
        }

        return _Character.CharacterType == Character.CharacterTypes.Player;
    }

    protected virtual bool PickUp(){
        // --- Basic Empty Function -- \\
        return true;
    }

    protected virtual void SetReferences(GameObject gameObject){

    }

}
