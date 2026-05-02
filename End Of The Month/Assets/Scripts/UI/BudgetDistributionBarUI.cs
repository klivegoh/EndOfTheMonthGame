using UnityEngine;

public class BudgetDistributionBarUI : MonoBehaviour
{
    [SerializeField] private BudgetManager budgetManager;
    [SerializeField] private BudgetDistributionSegmentUI[] segments;

    public void UpdateBar()
    {
        foreach (BudgetDistributionSegmentUI segment in segments)
        {
            BudgetCategory data = budgetManager.GetCategory(segment.category);
            segment.UpdateSegment(data, budgetManager.startingBalance);
        }
    }
}