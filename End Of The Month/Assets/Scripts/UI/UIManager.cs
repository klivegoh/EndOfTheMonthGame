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

    public void ShowEndSummary()
    {
        endScreen.SetActive(true);

        summaryText.text =
            "Week Complete\n" +
            "Balance Left: $" + budgetManager.currentBalance + "\n" +
            "Total Spent: $" + budgetManager.GetTotalSpent();
    }
}