using Zenject;

public class CardRowsContainerFactory : IFactory<CardRowsContainer>
{
    private CardRowsCreator cardRowsCreator;

    public CardRowsContainerFactory(CardRowsCreator cardRowsCreator)
    {
        this.cardRowsCreator = cardRowsCreator;
    }

    public CardRowsContainer Create()
    {
        CardRowsContainer cardRowsContainer = new CardRowsContainer(cardRowsCreator.InstantiateRows());

        return cardRowsContainer;
    }
}
