public interface ICardFactory
{
    ICard Create(ICardInfo cardInfo);
    ICard Create(CardValue value, CardSuit suit);
}