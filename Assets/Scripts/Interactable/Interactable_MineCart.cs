using System.Collections;
using UnityEngine;

public class Interactable_MineCart : Interactable
{
    [SerializeField] protected Interactable_MineCart _Target;
    protected Fader _Fader;

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

        RelocatePlayer();
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
    }

    protected void RelocatePlayer()
    {
        // holds _Character in case reference is lost when player leaves collision 
        var player = _Character;
        // var characterMovement = _Character.GetComponent<CharacterMovement>();
        //player.IsLocked = true;

        player.transform.position = _Target.transform.position;
        player.IsLocked = false;
        _Target.LockPlayerIn();
    }

    private void LockPlayerIn()
    {
        print("lock me in capn");
        _Character.transform.SetParent(transform);
    }

}