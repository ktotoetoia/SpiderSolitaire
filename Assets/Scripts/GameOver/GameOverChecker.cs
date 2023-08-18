using System;
using UnityEngine;
using Zenject;

[DefaultExecutionOrder(1)]
public class GameOverChecker : MonoBehaviour
{
    public event Action OnGameOver;

    [Inject] private CardRowsContainer cardRowsContainer;
    private bool wasInvoked;

    private void Start()
    {
        foreach (ICardRow row in cardRowsContainer.CardRows)
        {
            row.OnMatch += (x) => CheckGameOver();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameOver();
        }
    }

    private void CheckGameOver()
    {
        if (cardRowsContainer.CardRows.TrueForAll(x => x.LastCard == null) && !wasInvoked)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        wasInvoked = true;

        OnGameOver?.Invoke();
    }
}