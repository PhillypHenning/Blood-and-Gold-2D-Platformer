using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartTrigger : MonoBehaviour
{
    private BossFlags _BossFlags;
    public bool _StartBoss = false;

    private void Start() {
        _BossFlags = GameObject.Find("BossHead").GetComponent<BossFlags>();
    }

    void Update()
    {
        if(_StartBoss){
            _BossFlags.BossStarted = true;
            _StartBoss = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _BossFlags.BossStarted = true;
    }
}
