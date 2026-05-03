using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private BudgetManager budgetManager;

    [Header("Main UI")]
    [SerializeField] private TMP_Text balanceText;
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private Image balanceRadialFill;

    [Header("Category UI")]
    [SerializeField] private CategoryBarUI[] categoryBars;

    [Header("End Screen")]
    [SerializeField] private GameObject endScreen;
    [Header("End Screen Texts")]
    [SerializeField] private TMP_Text balanceTextEnd;         // Final Balance
    [SerializeField] private TMP_Text totalSpentText;         // Total Spent
    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text transportText;
    [SerializeField] private TMP_Text leisureText;
    [SerializeField] private TMP_Text savingsText;
    [SerializeField] private TMP_Text reflectionHeaderText;   // "Reflection"

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

        // Update radial fill
        float percentRemaining = (float)budgetManager.currentBalance / budgetManager.startingBalance;
        balanceRadialFill.fillAmount = Mathf.Clamp01(percentRemaining);

        if (percentRemaining > 0.5f)
            balanceRadialFill.color = new Color(0.5450981f, 0.7647059f, 0.2901961f);
        else
            balanceRadialFill.color = new Color(0.7647059f, 0.2901961f, 0.3254902f);

        foreach (CategoryBarUI bar in categoryBars)
        {
            BudgetCategory category = budgetManager.GetCategory(bar.Category);
            bar.UpdateBar(category);
        }
    }

    public void UpdateDayText(int currentDay, int maxDays)
    {
        string[] daysOfWeek =
        {
        "Monday", "Tuesday", "Wednesday",
        "Thursday", "Friday", "Saturday", "Sunday"
    };

        // Convert day number to index (loop if beyond 7)
        int index = (currentDay - 1) % daysOfWeek.Length;

        string dayName = daysOfWeek[index];

        dayText.text = "Day " + currentDay + " / " + maxDays + " - " + dayName;
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

        

        // Numbers
        balanceTextEnd.text = "$" + budgetManager.currentBalance;
        totalSpentText.text = "$" + budgetManager.GetTotalSpent();

        // Breakdown
        foodText.text = "$" + GetSpentText(food);
        transportText.text = "$" + GetSpentText(transport);
        leisureText.text = "$" + GetSpentText(leisure);
        savingsText.text = "$" + GetSpentText(savings);

        reflectionHeaderText.text = GetLearningMessage();
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
            return "You ran out of money before the week ended. Small daily choices added up.";
        }

        if (HasAnyOverBudgetCategory())
        {
            return "You finished the week, but some categories went over budget. Your next goal is to plan limits more carefully.";
        }

        return "You stayed within your plan. Budgeting is about making trade-offs before money runs out.";
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

    public void ResetUI()
    {
        if (endScreen != null)
        {
            endScreen.SetActive(false);
        }

        if (rewardPopup != null)
        {
            rewardPopup.SetActive(false);
        }

        ShowFeedback("");
        ShowEventPrompt("");
        UpdateStreakText("");
        UpdateAllUI();
    }
}
