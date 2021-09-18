using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss/Intro")]
public class BossIntro : AIAction
{
    private bool _IntroStarted = false;
    private bool _ScreamStarted = false;
    private float _TimeUntilIntroIsDone = 0f;
    private float _TotalTimeOfIntro = 4f;
    private float _ScreamDelay = 1f;
    private float _TimeUntilScremFinished = 0f;

    public override void Act(StateController controller)
    {
        InitiateBossIntro(controller);
    }

    private void InitiateBossIntro(StateController controller)
    {
        if (!controller._BossFlags.IntroDone)
        {
            if (!_IntroStarted)
            {
                _IntroStarted = true;

                _TimeUntilIntroIsDone = Time.time + _TotalTimeOfIntro;
                _TimeUntilScremFinished = Time.time + _ScreamDelay;
            }
            else if (!_ScreamStarted && _IntroStarted && Time.time > _TimeUntilScremFinished)
            {
                _ScreamStarted = true;
                controller._CharacterAnimator.Scream();
                Instantiate(controller._BossFlags.Attack2Prefab, controller._BossFlags.WeaponHolder.position, Quaternion.identity);
            }
            else if (_IntroStarted && _ScreamStarted && Time.time > _TimeUntilIntroIsDone)
            {
                _IntroStarted = false;
                _ScreamStarted = false;
                controller._BossFlags.IntroDone = true;
                _TimeUntilIntroIsDone = 0;
            }
        }
    }
}
