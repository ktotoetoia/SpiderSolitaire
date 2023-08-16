public class RandomCardFactory
{
    private CardInfoRandomizer cardInfoRandomizer;
    private ICardFactory cardFactory;

    public RandomCardFactory(CardInfoRandomizer cardInfoRandomizer, ICardFactory cardFactory)
    {
        this.cardInfoRandomizer = cardInfoRandomizer;
        this.cardFactory = cardFactory;
    }

    public ICard Create()
    {
        return cardFactory.Create(cardInfoRandomizer.GetRandom());
    }
}