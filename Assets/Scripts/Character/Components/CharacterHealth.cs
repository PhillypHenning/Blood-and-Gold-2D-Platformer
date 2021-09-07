using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : Health
{
    private float _CharacterMaxHealth = 50f;
    private CharacterAnimation _CharacterAnimation;
    private Character _Character;
    [SerializeField] private Lives _PlayerLives;

    protected override void SetToDefault()
    {
        _Character = GetComponent<Character>();
        _CharacterAnimation = GetComponent<CharacterAnimation>();
        if (_Character == null) Debug.LogError("CharacterHealth was unable to find 'Character' component.");
        if (_CharacterAnimation == null) Debug.LogWarning("CharacterHealth was unable to find 'CharacterAnimation' component.");
        _DefaultMaxHealth = _CharacterMaxHealth;
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
        base.Damage(amount);

        UpdateLivesUI();

        if (_CharacterAnimation == null) return;
        _CharacterAnimation.Hurt();
    }

    protected override void Die()
    {
        base.Die();
        _Character.IsLocked = true;

        if (_CharacterAnimation == null) return;
        _CharacterAnimation.Die();
    }

    private void UpdateLivesUI()
    {
        if (_Character.CharacterType == Character.CharacterTypes.AI || _PlayerLives == null) return;
        _PlayerLives.UpdateLives((int)_CurrentHealth / 10);
    }
}
