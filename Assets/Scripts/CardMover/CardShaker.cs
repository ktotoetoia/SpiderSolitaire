using System.Collections;
using UnityEngine;

public class CardShaker : MonoBehaviour
{
    [SerializeField] private Vector2 shakePower = new Vector2(0.3f, 0);
    [SerializeField] private float shakeTime = 0.4f;
    [SerializeField] private float shakeSmoothing = 0.1f;

    public void ShakeCard(IHasCardTransform card)
    {
        StartCoroutine(ShakeCardCoroutine(card.CardTransform));
    }

    private IEnumerator ShakeCardCoroutine(CardTransform cardTransform)
    {
        Vector2 cardPosition = cardTransform.transform.position;
        float time = Time.deltaTime;
        int direction = 1;

        yield return new WaitForEndOfFrame();

        while (GetNormalizedTime(time) < 1 && !cardTransform.IsMoving && cardTransform)
        {
            Vector2 shakePosition = cardPosition + (direction * (1 - GetNormalizedTime(time)) * shakePower);

            cardTransform?.TempMove(shakePosition, shakeSmoothing);

            yield return new WaitForSeconds(shakeSmoothing);

            direction *= -1;
            time += shakeSmoothing;
        }
    }

    private float GetNormalizedTime(float time)
    {
        return time / shakeTime;
    }
}