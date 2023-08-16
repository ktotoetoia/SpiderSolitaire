using UnityEngine;

[CreateAssetMenu()]
public class CardSettings : ScriptableObject, ICardSettings
{
    [field: SerializeField] public CardValue Value { get; set; }
    [field: SerializeField] public CardSuit Suit { get; set; }
    [field: SerializeField] public Sprite Sprite { get; set; }
}