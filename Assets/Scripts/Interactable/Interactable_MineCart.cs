using System;
using System.Collections;
using UnityEngine;

public class Interactable_MineCart : Interactable
{
    [SerializeField] protected Interactable_MineCart _Target;
    [SerializeField] protected Transform _CutsceneExitTarget = null;
    [SerializeField] protected float _MovementSpeed = 0.1f;
    [SerializeField] private bool _ExitOnly = false;
    protected Fader _Fader;
    protected Path _Path;
    protected float _PathTimer;
    protected bool _SetInMotion;
    protected Vector3 _StartPosition;

    protected override void Start()
    {
        base.Start();
        _PathTimer = 0;
        _SetInMotion = false;
        _StartPosition = transform.position;
        _Path = GetComponent<Path>();
        _Fader = FindObjectOfType<Fader>();

        if (_Path == null) Debug.LogWarning("Minecart was unable to locate Path.");
        if (_Fader == null) Debug.LogError("Door was unable to locate Fader");
    }

    protected override void Update()
    {
        base.Update();
        SetInMotion();
    }

    protected override bool InputEnabled()
    {
        if(_ExitOnly){ return false;}
        return Input.GetKeyDown(KeyCode.F);
    }

    protected override void Reward()
    {
        if (_Character == null) return;

        RemoveVisualQue();
        if (_Target != null)
        {
            StartCoroutine(RelocatePlayer());
        }
    }

    protected override void SetToDefault()
    {
        base.SetToDefault();
        if (_ExitOnly)
        {
            _DefaultMessage = "";
        }
        else
        {
            _DefaultMessage = "[f] to ride";
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        ExitCart();
        base.OnTriggerExit2D(other);
        // TODO: BUG: forces player out of lock, could be bad
    }

    protected IEnumerator RelocatePlayer()
    {
        SetExitDoor();
        // holds _Character in case reference is lost when player leaves collision 
        var player = _Character;
        if (player.CharacterType != Character.CharacterTypes.Player) yield return null;
        player.IsLocked = true;

        yield return _Fader.FadeOut();
        // var characterMovement = _Character.GetComponent<CharacterMovement>();
        //player.IsLocked = true;

        player.transform.position = _Target.transform.position;
        _Target.LockPlayerIn(player);
        yield return _Fader.FadeWait();
        yield return _Fader.FadeIn();
    }

    private void LockPlayerIn(Character player)
    {
        _SetInMotion = true;
        if (!player.FacingRight) player.GetComponent<CharacterFlip>().FlipCharacter();
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.SetParent(transform);
        player.GetComponentInChildren<CharacterAnimation>().EnterMineCart();
    }

    private void SetInMotion()
    {
        if (!_SetInMotion) return;

        _PathTimer += Time.deltaTime * _MovementSpeed;
        var nextPos = _Path.CurrentPoint;

        if (transform.position != nextPos)
        {
            transform.position = Vector2.Lerp(_StartPosition, nextPos, _PathTimer);
        }
    }

    private void ExitCart()
    {
        if (!_SetInMotion) return;
        _Character.transform.SetParent(null);
        _Character.IsLocked = false;
        _Character.GetComponentInChildren<CharacterAnimation>().ExitMineCart();
        _SetInMotion = false;
        transform.position = _StartPosition;
        _PathTimer = 0;
    }

    private void SetExitDoor()
    {
        if (_CutsceneExitTarget == null) return;

        var exit = GameObject.Find("Door_RightExit").GetComponent<Interactable_Door>();
        if (exit == null) return;

        exit.SetTarget(_CutsceneExitTarget);
    }
}