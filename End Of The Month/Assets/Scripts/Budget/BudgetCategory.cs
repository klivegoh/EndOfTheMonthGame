[System.Serializable]
public class BudgetCategory
{
    public CategoryType category;
    public int allocatedAmount;
    public int spentAmount;

    public float UsagePercent
    {
        get
        {
            if (allocatedAmount <= 0) return 0;
            return (float)spentAmount / allocatedAmount;
        }
    }

    public bool IsOverBudget => spentAmount > allocatedAmount;
}