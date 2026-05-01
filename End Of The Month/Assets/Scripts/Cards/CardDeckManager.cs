using System.Collections.Generic;
using UnityEngine;

public class CardDeckManager : MonoBehaviour
{
    [SerializeField] private List<DecisionCardData> allCards;

    public List<DecisionCardData> DrawCards(int amount)
    {
        List<DecisionCardData> availableCards = new List<DecisionCardData>(allCards);
        List<DecisionCardData> drawnCards = new List<DecisionCardData>();

        for (int i = 0; i < amount; i++)
        {
            if (availableCards.Count == 0) break;

            int randomIndex = Random.Range(0, availableCards.Count);
            drawnCards.Add(availableCards[randomIndex]);
            availableCards.RemoveAt(randomIndex);
        }

        return drawnCards;
    }
}