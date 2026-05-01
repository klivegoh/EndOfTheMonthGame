using System.Collections.Generic;
using UnityEngine;

public class BudgetManager : MonoBehaviour
{
    public int startingBalance = 100;
    public int currentBalance;

    public List<BudgetCategory> categories = new List<BudgetCategory>();

    private void Awake()
    {
        currentBalance = startingBalance;
    }

    public void SetCategoryBudget(CategoryType category, int amount)
    {
        BudgetCategory budget = GetCategory(category);

        if (budget != null)
        {
            budget.allocatedAmount = amount;
        }
    }

    public void AddExpense(CategoryType category, int amount)
    {
        currentBalance -= amount;

        if (category == CategoryType.Income)
        {
            return;
        }

        BudgetCategory budget = GetCategory(category);

        if (budget != null)
        {
            budget.spentAmount += Mathf.Max(0, amount);
        }
    }

    public BudgetCategory GetCategory(CategoryType category)
    {
        return categories.Find(c => c.category == category);
    }

    public int GetTotalSpent()
    {
        int total = 0;

        foreach (BudgetCategory category in categories)
        {
            total += category.spentAmount;
        }

        return total;
    }
}