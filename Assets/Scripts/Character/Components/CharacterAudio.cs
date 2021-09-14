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
    FMOD.Studio.EventInstance MineCart;
    FMOD.Studio.EventInstance CartBrake;

    void Start()
    {
        Footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Character/Player_Footsteps");
        Jump = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Character/Player_Jump");
        Land = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Character/Player_Land");
        Hurt = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Character/Player_Take_Damage");
        Die = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Character/Player_Death");
        Dodge = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Character/Player_Dodge");
        MineCart = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Interactables/Mine_Cart/Mine_Cart_Rolling");
        CartBrake = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Interactables/Mine_Cart/Mine_Cart_Brake");
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

    void Ride()
    {
        FMOD.Studio.PLAYBACK_STATE PbState;
        MineCart.getPlaybackState(out PbState);

        if(PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            MineCart.start();
            MineCart.release();
        }
    }

    void StopRide()
    {
        FMOD.Studio.PLAYBACK_STATE PbState;
        MineCart.getPlaybackState(out PbState);

            MineCart.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        if(PbState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            CartBrake.start();
        }
    }

    void OnDestroy()
    {
        Footsteps.release();
        Jump.release();
        Land.release();
        Hurt.release();
        Die.release();
        Dodge.release();
        CartBrake.release();
    }
}
