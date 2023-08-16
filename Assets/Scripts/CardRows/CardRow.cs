using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardRow : ICardRow
{
    public event Action OnMove;
    public event CardRowDelegate OnMatch;
    public event CardRowDelegate OnUpdate;

    private IMatcher matcher = new Matcher();
    private IHasCardRow cardPlace;
    private float offset;

    public RowList<ICard> Cards { get; private set; } = new RowList<ICard>();
    public ICard LastCard { get { return Cards.Last; } }
    public IHasCardRow CardPlace
    {
        get { return cardPlace; }
        set { cardPlace = value; cardPlace.CardRow = this; }
    }
    public Vector2 Position { get; set; }

    public CardRow(List<ICard> cards, Vector2 position, float offset)
    {
        Position = position;
        this.offset = offset;

        InstantiateCards(cards);
        LastCard.ChangeVisibility(true);
    }

    private void InstantiateCards(IEnumerable<ICard> cards)
    {
        foreach (ICard card in cards)
        {
            card.CardTransform.SetPositionImmidiately(GetNewCardPosition());
            AddHidedCard(card);
        }

        Update();
    }

    public bool CanAddCard(ICard card)
    {
        return LastCard == null || 
            LastCard.Value != CardValue.Ace && 
            LastCard.Value - 1 == card.Value;
    }

    public void AddCard(ICard card)
    {
        card.CardTransform.SetPosition(GetNewCardPosition());
        AddHidedCard(card);
        Update();
    }

    private void AddHidedCard(ICard card)
    {
        Cards.AddLast(card);
        card.CardRow = this;
    }

    private Vector2 GetNewCardPosition()
    {
        return Position + new Vector2(0, Cards.Sum(c => c.WasShowed ? offset : offset / 2));
    }

    public void AddCards(IEnumerable<ICard> cards)
    {
        foreach (ICard card in cards)
        {
            card.CardTransform.SetPosition(GetNewCardPosition());
            AddHidedCard(card);
        }

        Update();
    }

    public void MoveCardsTo(ICard card, ICardRow cardRow)
    {
        if (cardRow.CanAddCard(card))
        {
            cardRow.AddCards(Cards.RemoveAfter(card));
            Update();
            OnMove?.Invoke();
        }
    }

    private void Update()
    {
        LastCard?.ChangeVisibility(true);

        IEnumerable<ICard> cards = GetLastSequence();

        Cards.ForEach(card => card.ChangeVisibility(cards.Contains(card)));

        CheckForMatch();
        OnUpdate?.Invoke(Cards);
    }

    private void CheckForMatch()
    {
        IEnumerable<ICard> matchCards = GetLastSequence();

        if (matchCards.Count() == 13)
        {
            CardSuit cardSuit = matchCards.First().Suit;

            if (matchCards.All(x => x.Suit == cardSuit))
            {
                RemoveCards(matchCards);
                Match(matchCards);
            }
        }
    }

    private void RemoveCards(IEnumerable<ICard> cards)
    {
        Cards.RemoveAfter(cards.First());
    }

    private void Match(IEnumerable<ICard> cards)
    {
        MatchInfo matchInfo = matcher.Match(cards);
        matchInfo.OnMatchFinished += Update;
        matchInfo.OnMatchFinished += () => OnMatch?.Invoke(cards);
    }

    public IEnumerable<ICard> GetLastSequence()
    {
        return Cards.LastSequence((current, next) => 
        current.Value == next.Value + 1 
        && current.WasShowed 
        && next.WasShowed 
        && current.Suit == next.Suit);
    }
}