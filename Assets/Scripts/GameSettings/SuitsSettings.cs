using System.Collections.Generic;
using UnityEngine;
using UnityJSON;

public class SuitsSettings
{
    public List<CardSuit> Suits { get; set; } = new List<CardSuit>();
    [JSONNode(NodeOptions.DontSerialize)]
    public int SuitsCount { get { return Suits.Count; } }
    public float BestTime { get; set; } = 0;
    public float BestMoves { get; set; } = 0;

    public SuitsSettings() : this(1)
    {

    }

    public SuitsSettings(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Suits.Add((CardSuit)(i % 4));
        }
    }

    public void SetBest(float bestTime, float bestMoves)
    {
        if (BestMoves == 0)
        {
            BestMoves = bestMoves;
            BestTime = bestTime;
            return;
        }

        BestTime = Mathf.Min(BestTime, bestTime);
        BestMoves = Mathf.Min(BestMoves, bestMoves);
    }
}