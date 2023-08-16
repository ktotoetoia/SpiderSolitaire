using UnityEngine;

public interface ICardRowFactory
{
    public ICardRow Create(Vector2 position, int cardCount);
}