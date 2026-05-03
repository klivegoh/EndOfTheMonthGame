using UnityEngine;

public class BudgetDistributionBarUI : MonoBehaviour
{
    [SerializeField] private BudgetManager budgetManager;
    [SerializeField] private BudgetDistributionSegmentUI[] segments;

    public void UpdateBar()
    {
        int totalPercent = 0;

        // First pass: normal categories
        foreach (var segment in segments)
        {
            if (segment.isUnallocated) continue;

            BudgetCategory data = budgetManager.GetCategory(segment.category);

            int percent = 0;

            if (data != null && budgetManager.startingBalance > 0)
            {
                percent = Mathf.RoundToInt(
                    (float)data.allocatedAmount / budgetManager.startingBalance * 100f
                );
            }

            totalPercent += percent;
            segment.UpdateSegment(data, budgetManager.startingBalance);
        }

        // Second pass: unallocated
        int remaining = Mathf.Max(0, 100 - totalPercent);

        foreach (var segment in segments)
        {
            if (!segment.isUnallocated) continue;

            segment.UpdateSegment(null, budgetManager.startingBalance, remaining);
        }
    }
}