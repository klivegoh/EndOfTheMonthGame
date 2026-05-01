using System.Collections.Generic;
using UnityEngine;

public class CardDeckManager : MonoBehaviour
{
    [SerializeField] private GameDataManager gameDataManager;

    public List<DecisionCardData> DrawCards(int amount, int currentDay)
    {
        List<DecisionCardData> availableCards = new List<DecisionCardData>();

        foreach (DecisionCardData card in gameDataManager.AllCards)
        {
            if (!card.isActive)
            {
                continue;
            }

            if (currentDay < card.minDay || currentDay > card.maxDay)
            {
                continue;
            }

            availableCards.Add(card);
        }

        List<DecisionCardData> drawnCards = new List<DecisionCardData>();

        for (int i = 0; i < amount; i++)
        {
            if (availableCards.Count == 0)
            {
                break;
            }

            int randomIndex = Random.Range(0, availableCards.Count);
            DecisionCardData selectedCard = availableCards[randomIndex];

            drawnCards.Add(selectedCard);
            availableCards.RemoveAt(randomIndex);
        }

        return drawnCards;
    }
}