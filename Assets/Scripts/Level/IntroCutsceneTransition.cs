using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutsceneTransition : MonoBehaviour
{
    FMOD.Studio.EventInstance BGAudio;

    // Start is called before the first frame update
    void Start()
    {
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
            BGAudio.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
