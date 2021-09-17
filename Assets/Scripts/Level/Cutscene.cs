using System.Collections;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [SerializeField] Animator _Animator;
    public CutScenes _Cutscene;
    private float _CutSceneDuration = 3f;
    private bool _CutScenePlayed = false;

    public enum CutScenes
    {
        MiniBoss,
        FinalBoss
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_CutScenePlayed) return;
        if (collision.tag == "Player")
        {
            _CutScenePlayed = true;
            var player = collision.GetComponent<Character>();
            // adds 1 second to account for camera returning to initial position
            player.LockCharacter(_CutSceneDuration + 1f);
            switch (_Cutscene)
            {
                case (CutScenes.MiniBoss):
                    _Animator.SetBool("MinibossCutscene", true);
                    break;
                case (CutScenes.FinalBoss):
                    _Animator.SetBool("BossCutscene", true);
                    break;
            }
            Invoke(nameof(EndCutscene), _CutSceneDuration);
        }
    }

    private void EndCutscene()
    {
        _Animator.SetBool("MinibossCutscene", false);
        _Animator.SetBool("BossCutscene", false);
    }
}