using UnityEngine;
using TMPro;

public class BudgetSummaryUI : MonoBehaviour
{
    [SerializeField] private BudgetManager budgetManager;
    [SerializeField] private TMP_Text budgetText;

    public void UpdateSummary()
    {
        int totalAllocated = 0;

        foreach (BudgetCategory category in budgetManager.categories)
        {
            if (category.category == CategoryType.Income)
                continue;

            totalAllocated += category.allocatedAmount;
        }

        int total = budgetManager.startingBalance;

        budgetText.text = "$" + totalAllocated + " / $" + total;

        if (totalAllocated == total)
            budgetText.color = Color.green;
        else
            budgetText.color = Color.red;
    }
}