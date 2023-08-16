using UnityEngine;

public class CardPlace : MonoBehaviour, IHasCardRow
{
    public ICardRow CardRow { get; set; }
}