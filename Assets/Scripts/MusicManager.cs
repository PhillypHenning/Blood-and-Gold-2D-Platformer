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
        Music.start();
        Music.release();
    }

    void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
