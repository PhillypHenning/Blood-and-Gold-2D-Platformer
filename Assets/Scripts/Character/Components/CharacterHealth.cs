using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : Health
{
    public float _CharacterMaxHealth = 50f;
    private CharacterAnimation _CharacterAnimation;
    private Character _Character;
    [SerializeField] private Lives _PlayerLives;
    
    public bool _Damagable { get; set; }

    protected override void SetToDefault()
    {
        _Character = GetComponent<Character>();
        _CharacterAnimation = GetComponent<CharacterAnimation>();
        if (_Character == null) Debug.LogError("CharacterHealth was unable to find 'Character' component.");
        if (_CharacterAnimation == null) Debug.LogWarning("CharacterHealth was unable to find 'CharacterAnimation' component.");
        _DefaultMaxHealth = _CharacterMaxHealth;
        _Damagable = true;
        Physics2D.IgnoreLayerCollision(7, 9);  // "Player" layer ignores "Dead Body" layer
        Physics2D.IgnoreLayerCollision(8, 9);  // "Enemy" layer ignores "Dead Body" layer
        base.SetToDefault();
    }
    
    protected override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(KeyCode.J))
        {
            Damage(10);
        }
    }

    public override void Damage(float amount)
    {
        
        if(!_Damagable){ return; }

        base.Damage(amount);

        UpdateLivesUI();

        if (_CharacterAnimation == null) return;
        _CharacterAnimation.Hurt();
    }

    protected override void Die()
    {
        base.Die();
        if(_Character != null){
            _Character.IsLocked = true;
        }

        // Disable collision
        gameObject.layer = 9; // Changes layer to "Dead Body"
        if(gameObject.tag == "Shield"){
            _Collider2D.enabled = false;   
        }  


        if (_CharacterAnimation == null) return;
        _CharacterAnimation.Die();
        base.Die();
        _Damagable = false;
    }

    private void UpdateLivesUI()
    {
        if(_Character == null){return;}
        if (_Character.CharacterType == Character.CharacterTypes.AI || _PlayerLives == null) {return;}
        _PlayerLives.UpdateLives((int)_CurrentHealth / 10);
    }
}
