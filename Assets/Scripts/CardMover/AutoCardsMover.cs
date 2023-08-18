using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AutoCardsMover : MonoBehaviour
{
    [Inject] private CardRowsContainer cardRowsContainer;
    private CardShaker cardShaker;
    private CardMoves cardMoves;

    private void Start()
    {
        cardShaker = GetComponent<CardShaker>();
        cardMoves = GetComponent<CardMoves>();
    }

    public void AutoMoveCards(IEnumerable<ICard> cards)
    {
        ICard card = cards.First();
        ICardRow row = cardMoves.GetBestCardMove(card)?.RowToAdd;

        if (row != null)
        {
            MoveCards(cards, row);

            return;
        }

        StopCards(card.CardRow.Cards.GetAfter(card));
        cardShaker.ShakeCard(card);
    }

    private void MoveCards(IEnumerable<ICard> cards, ICardRow row)
    {
        ICard card = cards.First();

        card.CardRow.MoveCardsTo(card, row);
        cardRowsContainer.SetMaxOrder(cards);

        card.CardTransform.OnMovePerformed -= UpdateSortingOrder;
        card.CardTransform.OnMovePerformed += UpdateSortingOrder;
    }

    private void UpdateSortingOrder(ICard card)
    {
        cardRowsContainer.UpdateCardsFromCardRow(card.CardRow);
    }

    private void StopCards(IEnumerable<ICard> cards)
    {
        foreach (ICard card in cards)
        {
            card.CardTransform.ResetPosition();
        }
    }
}