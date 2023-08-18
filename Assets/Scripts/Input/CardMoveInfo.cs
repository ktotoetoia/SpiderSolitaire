using System.Linq;

public class CardMoveInfo
{
    private ICard cardToSetOn;
    private ICard PreviousCard;

    public ICard Card { get; }
    public ICardRow RowToAdd { get; }
    public int MoveValue { get; set; }
    public int RowToAddShowedCardsCount { get; set; }
    public CardMoveInfo(ICard card, ICard previousCard, ICardRow rowToAdd)
    {
        Card = card;
        PreviousCard = previousCard;
        RowToAdd = rowToAdd;
        cardToSetOn = rowToAdd.LastCard;
        RowToAddShowedCardsCount = rowToAdd.GetLastSequence().Count();

        RateMove();
    }

    private void RateMove()
    {
        if (PreviousCard != null)
        {
            AddValueIf(!PreviousCard.WasShowed, 2);
            AddValueIf(PreviousCard.Value != Card.Value + 1);
        }

        AddValueIf(cardToSetOn?.Suit == Card?.Suit);
    }

    private void AddValueIf(bool condition, int value = 1)
    {
        if (condition)
        {
            MoveValue += value;
        }
    }
}