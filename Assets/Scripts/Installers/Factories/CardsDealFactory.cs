using Zenject;

public class CardsDealFactory : IFactory<CardsDeal>
{
    private RandomCardFactory randomCardFactory;
    private CardRowsContainer cardRowsContainer;

    public CardsDealFactory(RandomCardFactory randomCardFactory, CardRowsContainer cardRowsContainer)
    {
        this.randomCardFactory = randomCardFactory;
        this.cardRowsContainer = cardRowsContainer;
    }

    public CardsDeal Create()
    {
        return new CardsDeal(randomCardFactory, cardRowsContainer.CardRows);
    }
}