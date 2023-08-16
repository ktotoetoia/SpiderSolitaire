using System;
using System.Collections;
using UnityEngine;

public class CardTransform : MonoBehaviour
{
    public event Action<ICard> OnMovePerformed;

    private IEnumerator mainMoveCoroutine;
    private IEnumerator moveCoroutine;
    private ICard card;
    private Vector2 position;

    public bool IsMoving { get; set; }
    public float DefaultMoveTime { get; set; }

    private void Start()
    {
        TryGetComponent(out card);
    }

    public void SetPositionImmidiately(Vector2 position)
    {
        this.position = position;

        Move(position);
    }

    public void SetPosition(Vector2 position)
    {
        this.position = position;
        
        MainMove(position, DefaultMoveTime);
    }

    public void Move(Vector2 position)
    {
        transform.position = position;
    }

    public void ResetPosition()
    {
        Move(position);
    }

    public void MainMove(Vector2 position, float time)
    {
        StopAllCoroutines();

        mainMoveCoroutine = MainMovea(position, time);

        StartCoroutine(mainMoveCoroutine);
    }

    public void TempMove(Vector2 position, float time)
    {
        if (!IsMoving)
        {
            moveCoroutine = Move(position, time);
            StartCoroutine(moveCoroutine);
        }
    }

    private IEnumerator MainMovea(Vector2 position, float time)
    {
        IsMoving = true;
        
        yield return Move(position, time);

        IsMoving = false;

        OnMovePerformed?.Invoke(card);
    }

    private IEnumerator Move(Vector2 position, float time)
    {
        float currentTime = 0;
        float normalizedTime = 0;
        Vector3 startPosition = transform.position;

        while (normalizedTime < 1)
        {
            currentTime += Time.deltaTime;
            normalizedTime = currentTime / time;

            transform.position = Vector2.Lerp(startPosition, position, normalizedTime);

            yield return null;
        }

        ResetPosition();
    }
}