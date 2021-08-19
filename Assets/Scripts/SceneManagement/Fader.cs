using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private CanvasGroup _CanvasGroup;
    const float _DefaultFadeTime = 0.3f;
    const float _DefaultWaitTime = 0.5f;

    private void Start()
    {
        _CanvasGroup = GetComponent<CanvasGroup>();
    }

    // Wrapper for fade sequence to auto fade out, wait, then fade in
    public void FadeOutIn(float fadeTime = _DefaultFadeTime, float waitTime = _DefaultWaitTime)
    {
        StartCoroutine(FadeSequence(fadeTime, waitTime));
    }

    private IEnumerator FadeSequence(float fadeTime, float waitTime)
    {
        yield return FadeOut(fadeTime);
        yield return FadeWait(waitTime);
        yield return FadeIn(fadeTime);
    }

    public IEnumerator FadeIn(float time = _DefaultFadeTime)
    {
        while (_CanvasGroup.alpha > 0)
        {
            _CanvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeOut(float time = _DefaultFadeTime)
    {
        while (_CanvasGroup.alpha < 1)
        {
            _CanvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeWait(float time = _DefaultWaitTime)
    {
        yield return new WaitForSeconds(time);
    }

}