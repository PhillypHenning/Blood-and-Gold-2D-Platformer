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

    public bool _Damagable { get; set; }
    private bool HasMoved = false;
    private Vector3 StartPos;

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
        StartPos = transform.position;
    }

    protected override void Update()
    {
        if(!HasMoved && transform.position != StartPos){
            HasMoved = true;
            _Character._GameStartTime = Time.time;
        }
        base.Update();
        if(_Despawn && Time.time > _TimeUntilDespawn){
            // Disable all child objects
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(false);
            }
        }
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        /* Debugging only 
        if (Input.GetKeyDown(KeyCode.J))
        {
            Damage(10);
        }
        */
    }

    public override void Damage(float amount)
    {
        if(!_Hitable) { return; }
        if (!_Damagable) { return; }

        base.Damage(amount);

        UpdateLivesUI();

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
                StartCoroutine("ReloadScene");
            }
            _Character.IsLocked = true;
            _Character.IsAlive = false;
            _Character.Actionable = false;
            _TimeUntilDespawn = Time.time + _TimeBetweenDespawn;
            _Despawn = true;
        }
        gameObject.layer = 9; // Changes layer to "Dead Body"
        _Damagable = false;

        if (_CharacterAnimation == null) return;
        _CharacterAnimation.Die();
        base.Die();
    }

    private void UpdateLivesUI()
    {
        if (_Character == null) { return; }
        if (_Character.CharacterType == Character.CharacterTypes.AI || _PlayerLives == null) { return; }
        _PlayerLives.UpdateLives((int)_CurrentHealth / 5);
    }

    // TODO: make a game manager to handle this...
    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(2f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    
}
