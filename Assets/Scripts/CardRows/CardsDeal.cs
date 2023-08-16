using System.Collections.Generic;
using UnityEngine;

public class CardsDeal
{
    private List<ICardRow> cardRows;
    private RandomCardFactory cardFactory;

    public CardsDeal(RandomCardFactory cardFactory, List<ICardRow> cardRows)
    {
        this.cardFactory = cardFactory;
        this.cardRows = cardRows;
    }

    public void Deal(Vector2 position)
    {
        foreach (ICardRow row in cardRows)
        {
            ICard card = cardFactory.Create();
            card.CardTransform.Move(position);
            row.AddCard(card);
            card.ChangeVisibility(true);
        }
    }
}