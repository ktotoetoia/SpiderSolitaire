using System.Collections.Generic;
using UnityEngine;

public class CardFactory : ICardFactory
{
    private List<ICardSettings> cardSettings;
    private GameObject cardPrefab;
    private Transform parentObject;
    private float moveTime;

    public CardFactory(List<ICardSettings> cardSettings, GameObject cardPrefab, float moveTime)
    {
        this.cardSettings = cardSettings;
        this.cardPrefab = cardPrefab;
        this.moveTime = moveTime;

        parentObject = Object.FindObjectOfType<CardParent>().transform;
    }

    public ICard Create(ICardInfo cardInfo)
    {
        return Create(cardInfo.Value, cardInfo.Suit);
    }

    public ICard Create(CardValue value, CardSuit suit)
    {
        GameObject cardObject = Object.Instantiate(cardPrefab, parentObject);
        ICard card = cardObject.GetComponent<ICard>();

        card.Initialize(cardSettings.Find(x => x.Value == value && x.Suit == suit));

        card.CardTransform.DefaultMoveTime = moveTime;

        return card;
    }
}