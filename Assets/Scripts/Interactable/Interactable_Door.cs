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
    [SerializeField] private bool _ForcedEntry = false;
    [SerializeField] private bool _ExitOnly = false;
    [SerializeField] private bool _IsIntroDoor = false;
    

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
        if(_ExitOnly){ return false;}
        if (_ForcedEntry) { return true; }
        return Input.GetKeyDown(KeyCode.F);
    }

    protected override void Reward()
    {
        if (_IsLocked && !CheckForKeyItem())
        {
            UpdateMessage("Door is locked. Obtain " + _RequiredItem.ToString() + " to proceed.");
            return;
        }

        // if(!_Character._HasBossKey && _IsLocked){
        //     UpdateMessage("Door is locked. Obtain " + _RequiredItem.ToString() + " to proceed.");
        //     return;
        // }

        if(_IsIntroDoor){
            // 
            _Character.IsIntro = false;
            //_EnvironmentManager.ActivateBuckets();
        }

        RemoveVisualQue();
        StartCoroutine(RelocatePlayer());

        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Interactables/Door/Door_Open", gameObject);
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
        //_EnvironmentManager.ResetParallaxToNewPosition(player.transform);

        // Add music here Cas

        yield return _Fader.FadeWait();
        yield return _Fader.FadeIn();
        player.IsLocked = false;
    }

    protected override void SetToDefault()
    {
        base.SetToDefault();
        if (_ForcedEntry || _ExitOnly)
        {
            _DefaultMessage = "";
        }
        else
        {
            _DefaultMessage = "Press 'F' to open.";
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}