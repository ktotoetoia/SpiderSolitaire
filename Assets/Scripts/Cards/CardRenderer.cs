using UnityEngine;

public class CardRenderer : MonoBehaviour
{
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

    public void Open()
    {
        spriteRenderer.sprite = openSprite;
        spriteRenderer.color = Color.white;
    }

    public void Hide()
    {
        spriteRenderer.color = Color.gray;
    }
}