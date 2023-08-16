public class CardInfo : ICardInfo
{
    public CardValue Value { get; set; }
    public CardSuit Suit { get; set; }

    public CardInfo(CardValue value, CardSuit suit)
    {
        Value = value;
        Suit = suit;
    }
}