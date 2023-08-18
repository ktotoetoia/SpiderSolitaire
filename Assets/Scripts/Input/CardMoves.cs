using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CardMoves : MonoBehaviour
{
    [Inject] private CardRowsContainer container;

    public CardMoveInfo GetBestCardMove()
    {
        IEnumerable<CardMoveInfo> moves = container.CardRows
            .SelectMany(row => GetCardMoves(row.GetLastSequence().FirstOrDefault()).Where(x => x?.MoveValue > 0));

        return GetBest(moves);
    }

    public CardMoveInfo GetBestCardMove(ICard card)
    {
        IEnumerable<CardMoveInfo> moves = GetCardMoves(card);

        return GetBest(moves);
    }

    private CardMoveInfo GetBest(IEnumerable<CardMoveInfo> moves)
    {
        int best = moves.Any() ? moves.Max(x => x.MoveValue) : -1;

        return moves
            .OrderByDescending(x => x?.RowToAddShowedCardsCount)
            .FirstOrDefault(x => x?.MoveValue == best);
    }

    public IEnumerable<CardMoveInfo> GetCardMoves(ICard card)
    {
        if (card == null)
        {
            return Enumerable.Empty<CardMoveInfo>();
        }

        return container.CardRows
            .Where(x => x.CanAddCard(card))
            .Select(row => new CardMoveInfo(card, card.CardRow.Cards.GetPrevious(card), row));
    }
}