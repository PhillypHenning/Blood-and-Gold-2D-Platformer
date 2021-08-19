using System.Collections;
using UnityEngine;

public class Interactable_Door : Interactable 
{
    [SerializeField] protected Transform _Target;
    protected Fader _Fader;

    protected override void Start()
    {
        base.Start();
        _Fader = FindObjectOfType<Fader>();
        if (_Fader == null) Debug.LogError("Door was unable to locate Fader");
    }

    protected override bool InputEnabled()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

    protected override void Reward()
    {
        // we don't call the base because the door can be used more than once
        StartCoroutine(RelocatePlayer());
    }

    protected IEnumerator RelocatePlayer()
    {
        // holds _Character in case reference is lost when player leaves collision 
        var player = _Character;
        // var characterMovement = _Character.GetComponent<CharacterMovement>();

        yield return _Fader.FadeOut();
        // TODO: fix "MovePosition" call (player is being moved to wrong position when running commented code below)
        // characterMovement.MovePosition(new Vector2(_Target.position.x, _Target.position.y));
        player.transform.position = _Target.position;
        yield return _Fader.FadeWait();
        yield return _Fader.FadeIn();
    }

}