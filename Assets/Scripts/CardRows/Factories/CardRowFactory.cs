using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardRowFactory : ICardRowFactory
{
    private RandomCardFactory cardFactory;
    private GameObject cardPlace;
    private Transform parentTransform;
    private float rowsDistance;

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
        return Enumerable
            .Range(0, count)
            .Select(x => cardFactory.Create())
            .ToList();
    }
}