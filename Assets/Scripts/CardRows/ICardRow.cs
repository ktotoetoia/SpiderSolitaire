using System;
using System.Collections.Generic;

public delegate void CardRowDelegate(IEnumerable<ICard> cards);

public interface ICardRow : ICardRowEvents
{
    RowList<ICard> Cards { get; }
    ICard LastCard { get; }

    void MoveCardsTo(ICard card, ICardRow cardRow);
    void AddCards(IEnumerable<ICard> cards);
    bool CanAddCard(ICard card);
    void AddCard(ICard card);
    IEnumerable<ICard> GetLastSequence();
}

public interface ICardRowEvents
{
    event CardRowDelegate OnMatch;
    event CardRowDelegate OnUpdate;
    event Action OnMove;
}