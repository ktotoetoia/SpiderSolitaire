using UnityEngine;

public class CardMover : MonoBehaviour, ICardMover
{
    private ICardSelection cardSelection;

    private void Start()
    {
        cardSelection = GetComponent<ICardSelection>();
    }

    public void MoveSelected(Vector3 position)
    {
        if (cardSelection.IsSelected)
        {
            SelectionInfo selectionInfo = cardSelection.SelectionInfo;

            for (int i = 0; i < selectionInfo.OnSelectPositions.Count; i++)
            {
                Vector2 movePosition = position + (selectionInfo.OnSelectPositions[i] - selectionInfo.InputPosition);

                selectionInfo.SelectedCards[i].CardTransform.MainMove(movePosition, 0);
            }
        }
    }
}