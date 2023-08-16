using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Matcher : IMatcher
{
    private MonoBehaviour coroutineRunner = Unity.VisualScripting.CoroutineRunner.instance;
    private float disapearTime = 0.06f;

    public MatchInfo Match(IEnumerable<ICard> cards)
    {
        MatchInfo matchInfo = new MatchInfo();

        coroutineRunner.StartCoroutine(MatchCoroutine(cards, matchInfo));
        return matchInfo;
    }

    private IEnumerator MatchCoroutine(IEnumerable<ICard> cards, MatchInfo matchInfo)
    {
        yield return new WaitUntil(() => cards.All(x => x.CanDestroy()));

        yield return MoveCoroutine(cards.Reverse());

        yield return new WaitUntil(() => cards.All(x => x.CanDestroy()));

        DestroyCards(cards);

        matchInfo.Invoke();
    }

    private IEnumerator MoveCoroutine(IEnumerable<ICard> cards)
    {
        List<ICard> movedCards = new List<ICard>();

        foreach (ICard card in cards)
        {
            if (movedCards.Count > 0)
            {
                foreach (ICard prevCard in movedCards)
                {
                    prevCard.CardTransform.Move(card.CardTransform.transform.position);
                }

                yield return new WaitForSeconds(disapearTime);
            }

            movedCards.Add(card);
        }
    }

    private void DestroyCards(IEnumerable<ICard> cards)
    {
        foreach (ICard card in cards)
        {
            Object.Destroy(card.CardTransform.gameObject);
        }
    }
}