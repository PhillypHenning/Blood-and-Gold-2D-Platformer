using System.Collections;
using UnityEngine;

public class Interactable_Door : Interactable 
{
    [SerializeField] protected Transform _Target;
    protected Fader _Fader;
     

    [SerializeField] private bool _IsLocked;
    [SerializeField] private ItemType _RequiredItem;

    private EnvironmentManager _EnvironmentManager;
    private GameObject _EnvironmentManagerGameObject;

    protected override void Start()
    {
        base.Start();
        _Fader = FindObjectOfType<Fader>();
        if (_Fader == null) Debug.LogError("Door was unable to locate Fader");

        _EnvironmentManagerGameObject = GameObject.Find("Parallax_Background");
        _EnvironmentManager = _EnvironmentManagerGameObject.GetComponent<EnvironmentManager>();
    }

    protected override bool InputEnabled()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

    protected override void Reward()
    {
        if (_IsLocked && !CheckForKeyItem())
        {
            UpdateMessage("Door is locked. Obtain " + _RequiredItem.ToString() + " to proceed.");
            return;
        }
        RemoveVisualQue();
        StartCoroutine(RelocatePlayer());
    }

    private bool CheckForKeyItem()
    {
        var playerInventory = _Character.GetComponent<InventoryManager>();
        if (_RequiredItem == ItemType.None) Debug.LogError("Locked door is missing key item reference");
        if (playerInventory == null) Debug.LogError("Player inventory not found.");

        return playerInventory.HasItem(_RequiredItem);
    }

    protected IEnumerator RelocatePlayer()
    {
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
    }

    protected override void SetToDefault()
    {
        base.SetToDefault();
        _DefaultMessage = "Press 'F' to open.";
    }
}