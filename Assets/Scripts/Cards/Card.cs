using UnityEngine;

public class Card : MonoBehaviour, ICard
{
    private bool isHided = true;

    [field: SerializeField] public CardValue Value { get; protected set; }
    [field: SerializeField] public CardSuit Suit { get; protected set; }
    public CardTransform CardTransform { get; private set; }
    public CardRenderer CardRenderer { get; private set; }
    public ICardRow CardRow { get; set; }
    public bool WasShowed { get; set; }
    public bool IsAvailableToSelect { get { return !isHided && CardRow != null; } }

    private void Awake()
    {
        CardRenderer = GetComponent<CardRenderer>();
        CardTransform = GetComponent<CardTransform>();
    }

    public void Initialize(ICardSettings cardSettings)
    {
        Value = cardSettings.Value;
        Suit = cardSettings.Suit;

        CardRenderer.Initialize(cardSettings.Sprite);
    }

    public void ChangeVisibility(bool visible)
    {
        if (visible)
        {
            Show();

            return;
        }

        Hide();
    }

    public void Hide()
    {
        if (WasShowed)
        {
            isHided = true;

            CardRenderer.Hide();
        }
    }

    public void Show()
    {
        WasShowed = true;
        isHided = false;

        CardRenderer.Open();
    }

    public bool CanDestroy()
    {
        return !CardTransform.IsMoving;
    }
}