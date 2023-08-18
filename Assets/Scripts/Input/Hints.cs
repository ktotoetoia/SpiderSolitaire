using UnityEngine;

public class Hints : MonoBehaviour
{
    [SerializeField] private CardsDealInput dealInput;
    private CardShaker cardShaker;
    private CardMoves cardMoves;

    private void Start()
    {
        cardShaker = GetComponent<CardShaker>();
        cardMoves = GetComponent<CardMoves>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowHint();
        }
    }

    public void ShowHint()
    {
        if (!TryShowPossibleMove())
        {
            TryShowDealMove();
        }
    }

    private bool TryShowPossibleMove()
    {
        CardMoveInfo cardMove = cardMoves.GetBestCardMove();

        if (cardMove != null)
        {
            ICard card = cardMove.Card;

            cardShaker.ShakeCard(card);
        }

        return cardMove != null;
    }

    private bool TryShowDealMove()
    {
        if (dealInput.DealLast > 0)
        {
            cardShaker.ShakeCard(dealInput.LastCard);
        }

        return dealInput.DealLast > 0;
    }
}