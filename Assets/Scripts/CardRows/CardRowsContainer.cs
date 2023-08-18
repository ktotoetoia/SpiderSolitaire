using System;
using System.Collections.Generic;
using System.Linq;

public class CardRowsContainer
{
    private const int maxLayer = 32767;

    public event Action OnMovePerformed;

    private delegate void CardIndexDelegate(ICard card, int index);

    public List<ICardRow> CardRows { get; private set; }

    public CardRowsContainer(List<ICardRow> cardRows)
    {
        CardRows = cardRows;

        UpdateRows();

        foreach (ICardRow row in cardRows)
        {
            row.OnUpdate += UpdateCardsSortingOrder;
            row.OnMove += () => OnMovePerformed?.Invoke();
        }
    }

    public void SetMaxOrder(IEnumerable<ICard> cards)
    {
        int cardsCount = cards.Count();

        ForEachCardIndex(cards, (card, index) => card.CardRenderer.SetSortingOrder(maxLayer - (cardsCount - index)));
    }

    private void UpdateRows()
    {
        foreach (ICardRow row in CardRows)
        {
            UpdateCardsSortingOrder(row.Cards);
        }
    }

    public void UpdateCardsFromCardRow(ICardRow cardRow)
    {
        if (cardRow != null)
        {
            UpdateCardsSortingOrder(cardRow.Cards);
        }
    }

    public void UpdateCardsSortingOrder(IEnumerable<ICard> cards)
    {
        ForEachCardIndex(cards, (card, order) => card.CardRenderer.SetSortingOrder(order));
    }

    private void ForEachCardIndex(IEnumerable<ICard> cards, CardIndexDelegate cardIndexDelegate)
    {
        int index = 0;

        foreach (ICard card in cards)
        {
            cardIndexDelegate(card, index);
            index++;
        }
    }
}