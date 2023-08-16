using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CardSelection : MonoBehaviour, ICardSelection
{
    [Inject] private CardRowsContainer cardRowsContainer;

    public SelectionInfo SelectionInfo { get; private set; }
    public bool IsSelected { get { return SelectionInfo != null; } }

    public ICard SelectAtPosition(Vector3 position)
    {
        GameObject cardObject = GetCardsAtPosition(position).FirstOrDefault();

        if (cardObject != null && cardObject.TryGetComponent(out ICard card) && card.IsAvailableToSelect)
        {
            Select(cardObject, position);
        }

        return SelectionInfo?.SelectedCard;
    }

    private void Select(GameObject obj, Vector2 selectedPosition)
    {
        SelectionInfo = new SelectionInfo(obj, selectedPosition);

        cardRowsContainer.SetMaxOrder(SelectionInfo.SelectedCards);
    }

    public List<GameObject> GetCardsAtPosition(Vector2 position)
    {
        return Physics2D
            .OverlapPointAll(position)
            .Where(x => x.TryGetComponent(out IHasCardRow card))
            .OrderByDescending(x => x.GetComponent<Renderer>().sortingOrder)
            .Select(x => x.gameObject)
            .ToList();
    }

    public ICard Deselect()
    {
        cardRowsContainer.UpdateCardsFromCardRow(SelectionInfo.SelectedCard.CardRow);

        ICard deselected = SelectionInfo?.SelectedCard;

        SelectionInfo = null;

        return deselected;
    }
}