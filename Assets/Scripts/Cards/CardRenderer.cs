using UnityEngine;

public class CardRenderer : MonoBehaviour
{
    private const int maxLayer = 32767;

    [SerializeField] private Sprite closedSprite;
    private Sprite openSprite;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Sprite sprite)
    {
        openSprite = sprite;
    }

    public void SetSortingOrder(int value)
    {
        spriteRenderer.sortingOrder = value;
    }

    private void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void Open()
    {
        SetSprite(openSprite);
        spriteRenderer.color = Color.white;
    }

    public void Hide()
    {
        spriteRenderer.color = Color.gray;
    }

    public void SetMaxOrder(int count = 0)
    {
        SetSortingOrder(maxLayer - count);
    }
}