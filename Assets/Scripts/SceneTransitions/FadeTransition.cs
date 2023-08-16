using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour, ISceneTransition
{
    [SerializeField] private float transitionTime;
    private Image image;
    private IEnumerator fade;

    private void Start()
    {
        image = GetComponentInChildren<Image>();

        FadeIn();
    }

    public void Activate(Action action)
    {
        FadeOut(action);
    }

    private void FadeIn()
    {
        StopCoroutine();

        fade = Fade((a) => image.color = new Color(0, 0, 0, 1 - a), null, 0);

        StartCoroutine(fade);
    }

    private void FadeOut(Action action)
    {
        StopCoroutine();

        fade = Fade((a) => image.color = new Color(0, 0, 0, Mathf.Max(a, image.color.a)), action, image.color.a);

        StartCoroutine(fade);
    }

    private void StopCoroutine()
    {
        if (fade != null)
        {
            StopCoroutine(fade);
        }
    }

    private IEnumerator Fade(Action<float> timeAction, Action onFinishedAction, float normalizedTime = 0)
    {
        float currentTime = 0;

        while (normalizedTime < 1)
        {
            currentTime += Time.deltaTime;
            normalizedTime = currentTime / transitionTime;
            timeAction(normalizedTime);

            yield return null;
        }

        onFinishedAction?.Invoke();
    }
}