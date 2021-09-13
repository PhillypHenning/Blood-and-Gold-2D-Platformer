using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private static FMOD.Studio.EventInstance Music;

    [FMODUnity.EventRef] [SerializeField]
    private string musicEvent;

    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        FMOD.Studio.PLAYBACK_STATE PbState;
        Music.getPlaybackState(out PbState);

        if(other.tag == "IntroDoor")
        {
            if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                Music.start();
                Music.release();
            }
        }
        
        if(other.tag == "MiniBossRoom")
        {
            Music.setParameterByName("Area", 3);
        }

        if(other.tag == "Area2")
        {
            Music.setParameterByName("Area", 2);
        }

        if(other.tag == "BossRoom")
        {
            Music.setParameterByName("Area", 4);
        }

    }

    public void StopMusic()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    void OnDestroy()
    {
        StopMusic();
    }
}
