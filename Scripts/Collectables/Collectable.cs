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

    [Header("Settings")]
    [SerializeField] private bool _DestroyItemOnPickUp = true;
    
    private void Start() {
        
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Collider2D = GetComponent<Collider2D>();

    }

    private void OnTriggerEnter2D(Collider2D other) {
        _CollidingGameObject = other.gameObject;
        Debug.Log("Check + " + _CollidingGameObject);

        if(IsUsableByPlayer()){
            // Pick up Object
            PickUp();

            if(_DestroyItemOnPickUp){
                Destroy(gameObject);
            }
            else{
                // TODO: This may need to be tweaked. 
                _SpriteRenderer.enabled = false;
                _Collider2D.enabled = false;
            }
        }        
    }

    protected virtual bool IsUsableByPlayer(){
        // Checks the colliding Objects tag
        _Character = _CollidingGameObject.GetComponent<Character>();

        if(_Character == null){
            return false;
        }

        return _Character.CharacterType == Character.CharacterTypes.Player;
    }

    protected virtual void PickUp(){
        // --- Basic Empty Function -- \\

    }

}
