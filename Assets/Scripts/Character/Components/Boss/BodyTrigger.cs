using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTrigger : MonoBehaviour
{

    // Get BossFlags from BossHead
    private Transform _Target;
    private BossFlags _BossFlags;
    private CharacterHealth _CharacterHealth;
    private bool WaitForVulnerable = false;

    // Start is called before the first frame update
    void Start()
    {
        var holdCreature = GetComponent<HoldCreature>();
        _Target = holdCreature.Target;
        _BossFlags = _Target.GetComponent<BossFlags>();
        _CharacterHealth = _Target.GetComponent<CharacterHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_CharacterHealth.CurrentHealth <= 0){
            _BossFlags.MovesSinceVunerable = 5;
            // Make body unhitable until _BossFlags Active starts then finished starts
            WaitForVulnerable = _BossFlags.VulnerableStarted;
            gameObject.layer = 9;
        }
        if(WaitForVulnerable){
            if(_BossFlags.VulnerableFinished){
                _CharacterHealth.Heal(50);
                WaitForVulnerable = false;
                gameObject.layer = 8;
            }
        }
    }
}
