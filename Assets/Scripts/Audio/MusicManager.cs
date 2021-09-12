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
        if(other.tag == "IntroDoor")
        Music.start();
        Music.release();
    }

    void Update()
    {
        
    }

    void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
