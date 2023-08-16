using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardInfoRandomizer
{
    private List<ICardInfo> cardInfos = new List<ICardInfo>();

    public CardInfoRandomizer(int count, List<CardSuit> suits)
    {
        int suitsCount = suits.Count();

        for (int i = 0; i < count; i++)
        {
            Fill(suits.ElementAt(i % suitsCount));
        }
    }

    private void Fill(CardSuit suit)
    {
        for (int i = 1; i <= 13; i++)
        {
            cardInfos.Add(new CardInfo((CardValue)i, suit));
        }
    }

    public ICardInfo GetRandom()
    {
        ICardInfo cardInfo = cardInfos[Random.Range(0, cardInfos.Count)];

        cardInfos.Remove(cardInfo);

        return cardInfo;
    }
}