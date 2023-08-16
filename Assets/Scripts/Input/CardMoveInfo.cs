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
        if (!PreviousCard?.WasShowed ?? false)
        {
            MoveValue += 2;
        }
        else if(PreviousCard?.Value!= Card.Value + 1)
        {
            MoveValue += 1;
        }

        if(cardToSetOn?.Suit == Card?.Suit)
        {
            MoveValue += 1;
        }
    }
}