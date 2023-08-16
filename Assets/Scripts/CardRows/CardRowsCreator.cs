using System.Collections.Generic;
using UnityEngine;

public class CardRowsCreator
{
    private ICardRowFactory cardRowFactory;
    private float cardRowsCount = 10;
    private float offset;

    public CardRowsCreator(ICardRowFactory cardRowFactory, float offset)
    {
        this.cardRowFactory = cardRowFactory;
        this.offset = offset;
    }

    public List<ICardRow> InstantiateRows()
    {
        List<ICardRow> cardRows = new List<ICardRow>();

        for (float i = 0; i < cardRowsCount; i++)
        {
            Vector2 position = new Vector2(offset * (i - cardRowsCount / 2), 0);

            cardRows.Add(cardRowFactory.Create(position, i < 4 ? 6 : 5));
        }

        return cardRows;
    }
}