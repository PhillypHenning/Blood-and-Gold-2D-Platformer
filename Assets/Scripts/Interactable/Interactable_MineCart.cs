using System.Collections;
using UnityEngine;

public class Interactable_MineCart : Interactable
{
    protected override void Start()
    {
        base.Start();
    }

    protected override bool InputEnabled()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

    protected override void Reward()
    {
        if (_Character == null) return;

        _Character.transform.SetParent(transform);
        _Character.IsLocked = true;
        RemoveVisualQue();
    }

    protected override void SetToDefault()
    {
        base.SetToDefault();
        _DefaultMessage = "Press 'F' to enter.";
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        // TODO: BUG: forces player out of lock, could be bad
        _Character.IsLocked = false;
    }
    protected IEnumerator RelocatePlayer()
    {
        /*
        // holds _Character in case reference is lost when player leaves collision 
        var player = _Character;
        // var characterMovement = _Character.GetComponent<CharacterMovement>();
        player.IsLocked = true;

        yield return _Fader.FadeOut();
        // TODO: fix "MovePosition" call (player is being moved to wrong position when running commented code below)
        // characterMovement.MovePosition(new Vector2(_Target.position.x, _Target.position.y));
        player.transform.position = _Target.position;

        // Reset the background to the players position
        _EnvironmentManager.ResetParallaxToNewPosition(player.transform);

        yield return _Fader.FadeWait();
        yield return _Fader.FadeIn();
        player.IsLocked = false;
        */
        yield return null;
    }
}