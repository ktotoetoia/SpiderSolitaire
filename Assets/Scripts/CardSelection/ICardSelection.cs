using System.Collections.Generic;
using UnityEngine;

public interface ICardSelection
{
    SelectionInfo SelectionInfo { get; }
    bool IsSelected { get; }
    List<GameObject> GetCardsAtPosition(Vector2 position);
    ICard SelectAtPosition(Vector3 position);
    ICard Deselect();
}