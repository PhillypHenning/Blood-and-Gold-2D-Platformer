using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterHealth : Health
{
    public float _CharacterMaxHealth = 50f;
    private CharacterAnimation _CharacterAnimation;
    private Character _Character;
    [SerializeField] private Lives _PlayerLives;
    private float _TimeUntilDespawn = 0;
    [SerializeField] private float _TimeBetweenDespawn = 3f;
    private bool _Despawn = false;
    [SerializeField] private bool EndBoss = false;
    public bool _IsShield;

    private Fader _Fader;

    public bool _Damagable { get; set; }
    private bool HasMoved = false;
    private Vector3 StartPos;
    private CharacterWeapon _CharacterWeapon;

    protected override void SetToDefault()
    {
        _Character = GetComponent<Character>();
        _CharacterAnimation = GetComponent<CharacterAnimation>();
        _CharacterWeapon = GetComponent<CharacterWeapon>();
        if (_Character && _Character.CharacterType == Character.CharacterTypes.Player) {
            _Fader = FindObjectOfType<Fader>();
            if (_Fader == null) Debug.LogError("CharacterHealth was unable to locate Fader");
        }
        _DefaultMaxHealth = _CharacterMaxHealth;
        _Damagable = true;
        Physics2D.IgnoreLayerCollision(7, 9);  // "Player" layer ignores "Dead Body" layer
        Physics2D.IgnoreLayerCollision(8, 9);  // "Enemy" layer ignores "Dead Body" layer
        base.SetToDefault();
        StartPos = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        if (_Despawn && Time.time > _TimeUntilDespawn)
        {
            // Disable all child objects
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(false);
            }
        }



        if (_Character != null)
        {
            if (_Character.AIType == Character.AITypes.Boss && EndBoss)
            {
                if (!_Character.IsAlive)
                {
                    var playergo = GameObject.Find("Player");
                    var player = playergo.GetComponent<Character>();

                    player._GameFinishTime = Time.time;
                }
            }
        }
    }

    protected override void HandleInput()
    {
        base.HandleInput();
    }

    public override void Damage(float amount)
    {
        if (!_Hitable) { return; }
        if (!_Damagable) { return; }

        base.Damage(amount);
        UpdateLivesUI();

        if (_IsShield && _CurrentHealth > 0)
        {
            var parentAnimator = GetComponentInParent<CharacterAnimation>();
            if (parentAnimator == null) return;

            parentAnimator.ShieldHurt();
        }

        if (_CharacterAnimation == null || !_Character.IsAlive) return;
        _CharacterAnimation.Hurt();
    }

    public override bool Heal(float amount)
    {
        if (!base.Heal(amount)) return false;
        UpdateLivesUI();
        return true;
    }

    protected override void Die()
    {
        base.Die();
        if (_Character != null)
        {
            if (_Character.CharacterType == Character.CharacterTypes.Player)
            {
                _Fader.FadeOut();
                Invoke("GameOver", 1.5f);
            }
            else if (_Character.AIType == Character.AITypes.Boss)
            {
                //_Fader.FadeOut();
                Invoke("Victory", 10f);
            }
            _Character.ForceLockCharacter();
            _Character.IsAlive = false;
            _Character.Actionable = false;
            if (_CharacterWeapon)
            {
                _CharacterWeapon.UnequiptAll();
            }
            _TimeUntilDespawn = Time.time + _TimeBetweenDespawn;
            _Despawn = true;
        }
        gameObject.layer = 9; // Changes layer to "Dead Body"
        _Damagable = false;

        if (_IsShield)
        {
            var parentAnimator = GetComponentInParent<CharacterAnimation>();
            if (parentAnimator == null) return;

            parentAnimator.ShieldBreak();
        }

        if (_CharacterAnimation != null)
        {
            _CharacterAnimation.Die();
            
        }
    }

    private void UpdateLivesUI()
    {
        if (_Character == null) { return; }
        if (_Character.CharacterType == Character.CharacterTypes.AI || _PlayerLives == null) { return; }
        _PlayerLives.UpdateLives((int)_CurrentHealth / 5);
    }

    private void GameOver()
    {
        SceneManager.LoadScene(2);
    }
    private void Victory()
    {
        SceneManager.LoadScene(3);
    }

}
