public interface ICard : ICardInfo, IHasCardRow,IHasCardTransform
{
    CardRenderer CardRenderer { get; }
    bool IsAvailableToSelect { get; }
    bool WasShowed { get; set; }

    void Initialize(ICardSettings cardSettings);
    bool CanDestroy();
    void ChangeVisibility(bool visible);
}

public interface ICardInfo
{
    CardValue Value { get; }
    CardSuit Suit { get; }
}

public interface IHasCardRow
{
    ICardRow CardRow { get; set; }
}

public interface IHasCardTransform
{
    CardTransform CardTransform { get; }
}

public enum CardValue
{
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
}

public enum CardSuit
{
    Spade,
    Heart,
    Club,
    Diamond,
}