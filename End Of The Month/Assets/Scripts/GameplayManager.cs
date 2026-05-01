using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private BudgetManager budgetManager;
    [SerializeField] private CardDeckManager deckManager;
    [SerializeField] private UIManager uiManager;

    [Header("Card UI")]
    [SerializeField] private List<DecisionCardUI> cardSlots;

    [Header("Game Settings")]
    [SerializeField] private int maxDays = 7;

    private int currentDay = 1;

    private bool hasStarted = false;

    private void Start()
    {
        
    }

    public void BeginRun()
    {
        if (hasStarted) return;

        hasStarted = true;
        currentDay = 1;

        uiManager.UpdateAllUI();
        StartDay();
    }

    public void StartDay()
    {
        uiManager.UpdateDayText(currentDay, maxDays);

        List<DecisionCardData> drawnCards = deckManager.DrawCards(3, currentDay);

        for (int i = 0; i < cardSlots.Count; i++)
        {
            if (i < drawnCards.Count)
            {
                cardSlots[i].gameObject.SetActive(true);
                cardSlots[i].Setup(drawnCards[i], this);
            }
            else
            {
                cardSlots[i].gameObject.SetActive(false);
            }
        }
    }

    public void PlayCard(DecisionCardData card)
    {
        budgetManager.AddExpense(card.category, card.cost);

        uiManager.UpdateAllUI();
        uiManager.ShowFeedback(card.feedbackText);

        currentDay++;

        if (currentDay > maxDays)
        {
            EndRun();
        }
        else
        {
            StartDay();
        }
    }

    private void EndRun()
    {
        uiManager.ShowEndSummary();
    }
}