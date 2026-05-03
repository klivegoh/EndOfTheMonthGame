using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameDataManager gameDataManager;
    [SerializeField] private BudgetManager budgetManager;
    [SerializeField] private UIManager uiManager;

    [Header("Progression Managers")]
    [SerializeField] private StreakManager streakManager;
    [SerializeField] private RewardManager rewardManager;

    [Header("Choice UI")]
    [SerializeField] private List<DecisionCardUI> cardSlots = new List<DecisionCardUI>();

    [Header("Game Settings")]
    [SerializeField] private int maxDays = 7;

    private int currentDay = 1;
    private bool hasStarted = false;
    private DailyEventPlanData currentPlan;

    public void BeginRun()
    {
        if (hasStarted)
        {
            return;
        }

        if (!HasRequiredReferences())
        {
            return;
        }

        hasStarted = true;
        currentDay = 1;

        uiManager.UpdateAllUI();
        StartDay();
    }

    public void StartDay()
    {
        if (!HasRequiredReferences())
        {
            return;
        }

        uiManager.HideRewardPopup();
        uiManager.UpdateDayText(currentDay, maxDays);

        currentPlan = gameDataManager.GetPlanForDay(currentDay);

        if (currentPlan == null)
        {
            Debug.LogError("No DailyEventPlan found for day " + currentDay + ". Check that DailyEventPlan.csv is inside Assets/Resources/Data and has a row for this day.");
            uiManager.ShowEventPrompt("No daily plan found for Day " + currentDay + ".");
            uiManager.ShowFeedback("Check DailyEventPlan.csv has a row for this day.");
            HideAllCards();
            return;
        }

        if (currentPlan.choices == null || currentPlan.choices.Count < 3)
        {
            Debug.LogError("DailyEventPlan for day " + currentDay + " does not have 3 choices.");
            uiManager.ShowEventPrompt(currentPlan.scenarioText);
            uiManager.ShowFeedback("This day needs 3 choices in DailyEventPlan.csv.");
            HideAllCards();
            return;
        }

        uiManager.ShowEventPrompt(currentPlan.scenarioText);
        //uiManager.ShowFeedback(currentPlan.scenarioName);

        for (int i = 0; i < cardSlots.Count; i++)
        {
            if (cardSlots[i] == null)
            {
                Debug.LogWarning("Card slot " + i + " is missing in GameplayManager.");
                continue;
            }

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
        if (choice == null)
        {
            Debug.LogError("PlayChoice was called with null choice data.");
            return;
        }

        if (!HasRequiredReferences())
        {
            return;
        }

        int costAmount = Mathf.Abs(choice.cost);

        budgetManager.AddExpense(choice.category, costAmount);

        

        if (streakManager != null)
        {
            streakManager.CheckStreaks();
            uiManager.UpdateStreakText(streakManager.GetMainStreakDisplay());
        }

        if (rewardManager != null)
        {
            List<RewardData> unlockedRewards = rewardManager.CheckRewards();

            if (unlockedRewards.Count > 0)
            {
                uiManager.ShowRewardPopup(unlockedRewards[0].displayMessage);
            }
        }

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
            if (cardSlot != null)
            {
                cardSlot.gameObject.SetActive(false);
            }
        }
    }

    private bool HasRequiredReferences()
    {
        bool isValid = true;

        if (gameDataManager == null)
        {
            Debug.LogError("GameplayManager is missing GameDataManager reference in the Inspector.");
            isValid = false;
        }

        if (budgetManager == null)
        {
            Debug.LogError("GameplayManager is missing BudgetManager reference in the Inspector.");
            isValid = false;
        }

        if (uiManager == null)
        {
            Debug.LogError("GameplayManager is missing UIManager reference in the Inspector.");
            isValid = false;
        }

        if (cardSlots == null || cardSlots.Count == 0)
        {
            Debug.LogError("GameplayManager has no card slots assigned in the Inspector.");
            isValid = false;
        }

        return isValid;
    }


    public void RestartRun()
    {
        if (!HasRequiredReferences())
        {
            return;
        }

        hasStarted = false;
        currentDay = 1;
        currentPlan = null;

        budgetManager.ResetBudget();
        uiManager.ResetUI();

        BeginRun();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
