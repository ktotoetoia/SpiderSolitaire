using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CardMoves : MonoBehaviour
{
    [Inject] CardRowsContainer container;

    public List<CardMoveInfo> GetAllCardsMoves()
    {
        return container.CardRows
            .Where(x => x.LastCard != null)
            .SelectMany(row => GetCardMoves(row.GetLastSequence().FirstOrDefault()))
            .ToList();
    }

    public CardMoveInfo GetBestCardMove(ICard card)
    {
        IEnumerable<CardMoveInfo> moves = GetCardMoves(card);

        if(moves.Any())
        {
            int best = moves.Max(x => x.MoveValue);

            return moves
                .OrderByDescending(x => x.RowToAddShowedCardsCount)
                .FirstOrDefault(x => x.MoveValue == best);
        }

        return null;
    }

    public List<CardMoveInfo> GetCardMoves(ICard card)
    {
        return container.CardRows
            .Where(x => x.CanAddCard(card))
            .Select(row => new CardMoveInfo(card, card.CardRow.Cards.GetPrevious(card), row))
            .ToList();
    }
}