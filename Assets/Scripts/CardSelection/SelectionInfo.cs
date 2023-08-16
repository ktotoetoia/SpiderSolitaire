using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionInfo
{
    public List<ICard> SelectedCards { get; private set; }
    public List<Vector3> OnSelectPositions { get; private set; } = new List<Vector3>();
    public ICard SelectedCard { get; private set; }
    public Vector3 InputPosition { get; set; }

    public SelectionInfo(GameObject cardObject, Vector3 selectedPosition)
    {
        InputPosition = selectedPosition;
        SelectedCard = cardObject.GetComponent<ICard>();
        SelectedCards = SelectedCard.CardRow.Cards.GetAfter(SelectedCard).ToList();

        AddCardsPositions();
    }

    private void AddCardsPositions()
    {
        foreach (ICard card in SelectedCards)
        {
            OnSelectPositions.Add(card.CardTransform.transform.position);
        }
    }
}