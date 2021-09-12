using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalagmiteDanger : MonoBehaviour
{
    private float _DamageToDeal = 5f;
    private float _TimeUntilNextDamage = 0;
    private float _InvulnerableFrames = .5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            other.GetComponent<CharacterHealth>().Damage(_DamageToDeal);
            _TimeUntilNextDamage = Time.time + _InvulnerableFrames;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(Time.time > _TimeUntilNextDamage && other.tag == "Player"){
            other.GetComponent<CharacterHealth>().Damage(_DamageToDeal);
            _TimeUntilNextDamage = Time.time + _InvulnerableFrames;
        }
    }
}
