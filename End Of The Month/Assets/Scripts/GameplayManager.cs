using UnityEngine;
using System.Collections.Generic;

public class GameplayManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameDataManager gameDataManager;
    [SerializeField] private BudgetManager budgetManager;
    [SerializeField] private UIManager uiManager;

    [Header("Choice UI")]
    [SerializeField] private List<DecisionCardUI> cardSlots;

    [Header("Game Settings")]
    [SerializeField] private int maxDays = 7;

    private int currentDay = 1;
    private bool hasStarted = false;
    private DailyEventPlanData currentPlan;

    public void BeginRun()
    {
        if (hasStarted) return;

        hasStarted = true;
        currentDay = 1;

        uiManager.UpdateAllUI();
        StartDay();
    }

    private void StartDay()
    {
        uiManager.UpdateDayText(currentDay, maxDays);

        currentPlan = gameDataManager.GetPlanForDay(currentDay);

        if (currentPlan == null)
        {
            uiManager.ShowEventPrompt("No daily plan found for Day " + currentDay + ".");
            uiManager.ShowFeedback("Check DailyEventPlan.csv has a row for this day.");
            HideAllCards();
            return;
        }

        uiManager.ShowEventPrompt(currentPlan.scenarioText);
        uiManager.ShowFeedback(currentPlan.designPurpose);

        for (int i = 0; i < cardSlots.Count; i++)
        {
            if (i < currentPlan.choices.Count)
            {
                cardSlots[i].gameObject.SetActive(true);
                cardSlots[i].Setup(currentPlan.choices[i], this);
            }
            else
            {
                cardSlots[i].gameObject.SetActive(false);
            }
        }
    }

    public void PlayChoice(DailyChoiceData choice)
    {
        budgetManager.AddExpense(choice.category, choice.cost);

        uiManager.UpdateAllUI();
        uiManager.ShowFeedback(choice.feedbackText);

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
        HideAllCards();
        uiManager.ShowEndSummary();
    }

    private void HideAllCards()
    {
        foreach (DecisionCardUI cardSlot in cardSlots)
        {
            cardSlot.gameObject.SetActive(false);
        }
    }
}
