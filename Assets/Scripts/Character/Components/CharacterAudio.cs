using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    public MusicManager end;

    FMOD.Studio.EventInstance Footsteps;
    FMOD.Studio.EventInstance Jump;
    FMOD.Studio.EventInstance Land;
    FMOD.Studio.EventInstance Hurt;
    FMOD.Studio.EventInstance Die;
    FMOD.Studio.EventInstance Dodge;

    void Start()
    {
        Footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Character/Player_Footsteps");
        Jump = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Character/Player_Jump");
        Land = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Character/Player_Land");
        Hurt = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Character/Player_Take_Damage");
        Die = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Character/Player_Death");
        Dodge = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Character/Player_Dodge");
    }
    void PlayerFootsteps()
    {
        Footsteps.start();
    }

    void PlayerJump()
    {
        Jump.start();
    }

    void PlayerLand()
    {
        Land.start();
    }

    void PlayerHurt()
    {
        Hurt.start();
    }

    void PlayerDodge()
    {
        Dodge.start();
    }

    void PlayerDie()
    {
        Die.start();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Music/Death_Sting");
        end.StopMusic();
    }

    void OnDestroy()
    {
        Footsteps.release();
        Jump.release();
        Land.release();
        Hurt.release();
        Die.release();
        Dodge.release();
    }
}
