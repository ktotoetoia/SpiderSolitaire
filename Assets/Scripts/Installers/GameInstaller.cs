using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private List<CardSettings> cardSettings;
    [SerializeField] private GameObject placeCardPrefab;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private float cardRowsSpaceX = 0.3f;
    [SerializeField] private float cardsSpaceY = 0.6f;
    [SerializeField] private float cardMoveTime;

    private GameSettings gameSettings
    {
        get { return GameSettings.Instance; }
    }

    public override void InstallBindings()
    {
        BindCardFactory();
        BindCardInfoRandomizer();
        BindCardRowFactory();
        BindCardRowsCreator();
        BindCardRowsContainer();
        BindCardsDeal();
    }

    private void BindCardFactory()
    {
        Container.Bind<ICardFactory>()
                 .To<CardFactory>()
                 .AsSingle()
                 .WithArguments(cardSettings.OfType<ICardSettings>().ToList(), cardPrefab, cardMoveTime);
    }

    private void BindCardInfoRandomizer()
    {
        Container.Bind<CardInfoRandomizer>()
                 .AsSingle()
                 .WithArguments(gameSettings.Count, gameSettings.CurrentSuitSettings.Suits);
        Container.Bind<RandomCardFactory>().AsSingle();
    }

    private void BindCardRowFactory()
    {
        Container.Bind<ICardRowFactory>()
                 .To<CardRowFactory>()
                 .AsSingle()
                 .WithArguments(placeCardPrefab, cardsSpaceY);
    }

    private void BindCardRowsCreator()
    {
        float cardSize = cardPrefab.GetComponent<Renderer>().localBounds.size.x;

        Container.Bind<CardRowsCreator>()
                 .AsSingle()
                 .WithArguments(cardSize + cardRowsSpaceX);
    }

    private void BindCardRowsContainer()
    {
        Container.Bind<CardRowsContainer>()
                 .FromIFactory(b => b.To<CardRowsContainerFactory>().AsCached())
                 .AsSingle();
    }

    private void BindCardsDeal()
    {
        Container.Bind<CardsDeal>()
                 .FromIFactory(b => b.To<CardsDealFactory>().AsCached())
                 .AsSingle();
    }
}