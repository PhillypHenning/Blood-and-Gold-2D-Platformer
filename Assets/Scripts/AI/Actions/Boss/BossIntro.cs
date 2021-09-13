using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss/Intro")]
public class BossIntro : AIAction
{
    private bool _IntroStarted = false;
    private float _TimeUntilIntroIsDone = 0f;
    private float _TotalTimeOfIntro = 3f;

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
                Debug.Log("Intro Started");

                _TimeUntilIntroIsDone = Time.time + _TotalTimeOfIntro;

                // Start Scream animation
                controller._CharacterAnimator.Attack2();
            }
            
            //Debug.Log("BOSS SCREAMING");

            if (_IntroStarted && Time.time > _TimeUntilIntroIsDone)
            {
                Debug.Log("Intro Finished");
                _IntroStarted = false;
                controller._BossFlags.IntroDone = true;
                _TimeUntilIntroIsDone = 0;
            }
        }
    }
}
