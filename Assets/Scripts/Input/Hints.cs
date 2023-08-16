using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hints : MonoBehaviour
{
    [SerializeField] private CardsDealInput dealInput;
    private CardShaker cardShaker;
    private CardMoves cardMoves;
    
    private void Start()
    {
        cardShaker = GetComponent<CardShaker>();
        cardMoves = GetComponent<CardMoves>();
    }

    public void ShowHint()
    {
        IEnumerable<CardMoveInfo> possibleMoves = cardMoves.GetAllCardsMoves()
            .Where(x => x.MoveValue > 0)
            .OrderByDescending(x => x.MoveValue);

        if(possibleMoves.Count() > 0)
        {
            ICard card = possibleMoves.First().Card;
            
            cardShaker.ShakeCard(card);

            return;
        }

        if (dealInput.DealLast > 0)
        {
            cardShaker.ShakeCard(dealInput.LastCard);
        }
    }
}