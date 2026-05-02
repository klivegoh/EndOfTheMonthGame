using System.Collections.Generic;
using UnityEngine;

public class BudgetManager : MonoBehaviour
{
    [SerializeField] private GameDataManager gameDataManager;

    public int startingBalance = 100;
    public int currentBalance;

    public List<BudgetCategory> categories = new List<BudgetCategory>();

    private void Awake()
    {
        currentBalance = startingBalance;
    }

    private void Start()
    {
        LoadBudgetDefaults();
    }

    private void LoadBudgetDefaults()
    {
        if (gameDataManager != null)
        {
            startingBalance = gameDataManager.GetIntSetting("StartingBalance", startingBalance);
        }

        currentBalance = startingBalance;

        EnsureCategoryExists(CategoryType.Food);
        EnsureCategoryExists(CategoryType.Transport);
        EnsureCategoryExists(CategoryType.Leisure);
        EnsureCategoryExists(CategoryType.Savings);

        if (gameDataManager == null)
        {
            return;
        }

        foreach (KeyValuePair<CategoryType, int> defaultBudget in gameDataManager.CategoryDefaults)
        {
            BudgetCategory category = GetCategory(defaultBudget.Key);

            if (category != null)
            {
                category.allocatedAmount = defaultBudget.Value;
                category.spentAmount = 0;
            }
        }
    }

    private void EnsureCategoryExists(CategoryType category)
    {
        if (GetCategory(category) != null)
        {
            return;
        }

        BudgetCategory newCategory = new BudgetCategory();
        newCategory.category = category;
        newCategory.allocatedAmount = 0;
        newCategory.spentAmount = 0;

        categories.Add(newCategory);
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
