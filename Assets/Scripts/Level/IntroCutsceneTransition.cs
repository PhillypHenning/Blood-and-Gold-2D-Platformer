using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutsceneTransition : MonoBehaviour
{
    FMOD.Studio.EventInstance Reverb;
    FMOD.Studio.EventInstance BGAudio;

    // Start is called before the first frame update
    void Start()
    {
        Reverb = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Cave_Verb");
        BGAudio = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Environment/BGAudio");
        BGAudio.start();
        BGAudio.release();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            other.GetComponent<Character>().IsIntro = false;
            Reverb.start();
            BGAudio.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
