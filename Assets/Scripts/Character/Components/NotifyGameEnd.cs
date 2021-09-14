using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyGameEnd : MonoBehaviour
{
    private Character Player;
    private CharacterHealth BossHealth;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Character>();
        BossHealth = GetComponent<CharacterHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(BossHealth.CurrentHealth == 0){
            Player._GameFinishTime = Time.time;
        }
    }
}
