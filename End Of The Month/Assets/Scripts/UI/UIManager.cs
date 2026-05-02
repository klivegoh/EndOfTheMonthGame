using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private BudgetManager budgetManager;

    [Header("Main UI")]
    [SerializeField] private TMP_Text balanceText;
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private TMP_Text feedbackText;

    [Header("Category UI")]
    [SerializeField] private CategoryBarUI[] categoryBars;

    [Header("End Screen")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TMP_Text summaryText;

    [Header("Scenario UI")]
    [SerializeField] private TMP_Text eventPromptText;

    [Header("Streak UI")]
    [SerializeField] private TMP_Text streakText;

    [Header("Reward Popup")]
    [SerializeField] private GameObject rewardPopup;
    [SerializeField] private TMP_Text rewardPopupText;

    public void UpdateAllUI()
    {
        balanceText.text = "$" + budgetManager.currentBalance;

        foreach (CategoryBarUI bar in categoryBars)
        {
            BudgetCategory category = budgetManager.GetCategory(bar.Category);
            bar.UpdateBar(category);
        }
    }

    public void UpdateDayText(int currentDay, int maxDays)
    {
        dayText.text = "Day " + currentDay + " / " + maxDays;
    }

    public void ShowFeedback(string message)
    {
        feedbackText.text = message;
    }

    public void ShowEventPrompt(string prompt)
    {
        eventPromptText.text = prompt;
    }

    public void ShowEndSummary()
    {
        endScreen.SetActive(true);

        BudgetCategory food = budgetManager.GetCategory(CategoryType.Food);
        BudgetCategory transport = budgetManager.GetCategory(CategoryType.Transport);
        BudgetCategory leisure = budgetManager.GetCategory(CategoryType.Leisure);
        BudgetCategory savings = budgetManager.GetCategory(CategoryType.Savings);

        summaryText.text =
            "Week Complete\n\n" +
            "Final Balance: $" + budgetManager.currentBalance + "\n" +
            "Total Spent: $" + budgetManager.GetTotalSpent() + "\n\n" +
            "Category Breakdown\n" +
            "Food: $" + GetSpentText(food) + "\n" +
            "Transport: $" + GetSpentText(transport) + "\n" +
            "Leisure: $" + GetSpentText(leisure) + "\n" +
            "Savings Goal: $" + GetSpentText(savings) + "\n\n" +
            GetLearningMessage();
    }

    private string GetSpentText(BudgetCategory category)
    {
        if (category == null)
        {
            return "0";
        }

        return category.spentAmount + " / $" + category.allocatedAmount;
    }

    private string GetLearningMessage()
    {
        if (budgetManager.currentBalance <= 0)
        {
            return "Reflection: You ran out of money before the week ended. Small daily choices added up.";
        }

        if (HasAnyOverBudgetCategory())
        {
            return "Reflection: You finished the week, but some categories went over budget. Your next goal is to plan limits more carefully.";
        }

        return "Reflection: You stayed within your plan. Budgeting is about making trade-offs before money runs out.";
    }

    private bool HasAnyOverBudgetCategory()
    {
        foreach (BudgetCategory category in budgetManager.categories)
        {
            if (category.category == CategoryType.Income)
            {
                continue;
            }

            if (category.IsOverBudget)
            {
                return true;
            }
        }

        return false;
    }

    public void UpdateStreakText(string message)
    {
        if (streakText != null)
        {
            streakText.text = message;
        }
    }

    public void ShowRewardPopup(string message)
    {
        if (rewardPopup == null || rewardPopupText == null)
        {
            return;
        }

        rewardPopup.SetActive(true);
        rewardPopupText.text = message;
    }

    public void HideRewardPopup()
    {
        if (rewardPopup != null)
        {
            rewardPopup.SetActive(false);
        }
    }
}
