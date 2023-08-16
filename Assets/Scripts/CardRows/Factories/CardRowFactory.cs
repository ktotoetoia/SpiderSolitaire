using System.Collections.Generic;
using UnityEngine;

public class CardRowFactory : ICardRowFactory
{
    private RandomCardFactory cardFactory;
    private GameObject cardPlace;
    private float rowsDistance;
    private Transform parentTransform;

    public CardRowFactory(RandomCardFactory cardFactory, GameObject cardPlace, float rowsDistance)
    {
        this.cardPlace = cardPlace;
        this.cardFactory = cardFactory;
        this.rowsDistance = rowsDistance;
        parentTransform = Object.FindObjectOfType<CardParent>().transform;
    }

    public ICardRow Create(Vector2 position, int cardCount)
    {
        CardRow cardRow = new CardRow(GetRandomCards(cardCount), position, rowsDistance);
        GameObject cardObject = Object.Instantiate(cardPlace, position, cardPlace.transform.rotation, parentTransform);

        cardRow.CardPlace = cardObject.GetComponent<IHasCardRow>();

        return cardRow;
    }

    private List<ICard> GetRandomCards(int count)
    {
        List<ICard> result = new List<ICard>();

        for (int i = 0; i < count; i++)
        {
            result.Add(cardFactory.Create());
        }

        return result;
    }
}