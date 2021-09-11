using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{

    FMOD.Studio.EventInstance Event;

    void PlayEvent(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
    }

    void PlayAttackEvent(string path)
    {
        Event = FMODUnity.RuntimeManager.CreateInstance(path);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Event, transform, GetComponent<Rigidbody2D>());
        Event.start();
        Event.release();
    }

    void StopAttackEvent()
    {
        Event.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
