using System.Collections;
using UnityEngine;

public class WeaponAnimationManager : MonoBehaviour
{

    protected Animator _Animator;
    protected int _ClipSize;

    FMOD.Studio.EventInstance ReloadClick;

    // Use this for initialization
    protected virtual void Start()
    {
        _Animator = GetComponent<Animator>();
        // TODO: inherit from weapon manager and set in child
        _ClipSize = 6;

        ReloadClick = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Character/Revolver/Revolver_Reload_Click");

        if (_Animator == null) print("WeaponAnimationManager couldn't find Animator Component.");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // TODO: implement with weapon components
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            FireShot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public virtual void FireShot()
    {
        if (_ClipSize == 0) return;
        _Animator.Play(string.Format("Fire_{0}", _ClipSize));
        _ClipSize--;
    }

    public virtual void Reload()
    {
        float offset = 0;

        switch (_ClipSize)
        {
            case 1:
                offset = 1;
                break;
            case 2:
                offset = 4;
                break;
            case 3:
                offset = 7;
                break;
            case 4:
                offset = 10;
                break;
            case 5:
                offset = 13;
                break;
        }

        _ClipSize = 6;
        _Animator.Play("Revolver_Reload", -1, (offset / 24));
    }

    void Click()
    {
        ReloadClick.start();
    }

    void OnDestroy()
    {
        ReloadClick.release();
    }
}