using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardInput : MonoBehaviour
{
    [SerializeField] private float maxMovingDistance = 1;
    private AutoCardsMover autoCardsMover;
    private ICardMover cardMover;
    private ICardSelection cardSelection;
    private CardShaker cardShaker;
    private Vector2 downPosition;
    private float movedDistance;

    private void Start()
    {
        autoCardsMover = GetComponent<AutoCardsMover>();
        cardMover = GetComponent<ICardMover>();
        cardSelection = GetComponent<ICardSelection>();
        cardShaker = GetComponent<CardShaker>();
    }

    private void Update()
    {
        CheckMouseButtonDown();
        CheckMouseButtonUp();
        CheckMouseButtonHold();
    }

    private void CheckMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cardSelection.SelectAtPosition(GetMousePosition());

            downPosition = GetMousePosition();
        }
    }

    private void CheckMouseButtonHold()
    {
        if (Input.GetMouseButton(0))
        {
            cardMover.MoveSelected(GetMousePosition());

            movedDistance = Mathf.Max(movedDistance, Vector2.Distance(downPosition, GetMousePosition()));
        }
    }

    private void CheckMouseButtonUp()
    {
        if (cardSelection.IsSelected && Input.GetMouseButtonUp(0))
        {
            if (movedDistance < maxMovingDistance)
            {
                IEnumerable<ICard> cards = cardSelection.SelectionInfo.SelectedCards;

                cardSelection.Deselect();
                autoCardsMover.AutoMoveCards(cards);
            }
            else
            {
                MoveCards();
            }

            movedDistance = 0;
        }
    }

    private void MoveCards()
    {
        SelectionInfo selectionInfo = cardSelection.SelectionInfo;
        ICard card = cardSelection.Deselect();
        List<GameObject> cards = cardSelection.GetCardsAtPosition(GetMousePosition());
        
        cards.RemoveAll(x => selectionInfo.SelectedCards.Contains(x.GetComponent<ICard>()));

        ICardRow rowToAdd = cards.FirstOrDefault()?.GetComponent<IHasCardRow>().CardRow;

        if (card != null && cards.Count > 0 && rowToAdd.CanAddCard(card))
        {
            card.CardRow.MoveCardsTo(card, rowToAdd);

            return;
        }
        
        selectionInfo.SelectedCards.ForEach(x => x.CardTransform.ResetPosition());
        cardShaker.ShakeCard(card);
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}