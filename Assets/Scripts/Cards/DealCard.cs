using UnityEngine;

public class DealCard : MonoBehaviour, IHasCardTransform
{
    public CardTransform CardTransform { get; set; }

    private void Awake()
    {
        CardTransform = GetComponent<CardTransform>();
    }
}