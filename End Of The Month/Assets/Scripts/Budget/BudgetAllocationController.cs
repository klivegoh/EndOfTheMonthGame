using UnityEngine;

public class BudgetAllocationController : MonoBehaviour
{
    [SerializeField] private BudgetManager budgetManager;
    [SerializeField] private BudgetSliderUI[] sliders;
    [SerializeField] private BudgetDistributionBarUI distributionBarUI;
    [SerializeField] private BudgetSummaryUI budgetSummaryUI;

    private const int TotalLimit = 100;

    private void Start()
    {
        InitDefaults();
        ApplyBudgets();
        UpdateAllVisuals();
    }

    private void InitDefaults()
    {
        foreach (BudgetSliderUI slider in sliders)
        {
            int defaultPercent = GetDefaultPercent(slider.Category);
            slider.Init(this, defaultPercent);
        }
    }

    private int GetDefaultPercent(CategoryType category)
    {
        switch (category)
        {
            case CategoryType.Food:
                return 30;

            case CategoryType.Transport:
                return 20;

            case CategoryType.Leisure:
                return 20;

            case CategoryType.Savings:
                return 30;

            default:
                return 0;
        }
    }

    public void OnSliderChanged(BudgetSliderUI changedSlider)
    {
        int totalWithout = GetTotalPercentExcept(changedSlider);
        int maxAllowed = TotalLimit - totalWithout;

        int clampedValue = Mathf.Clamp(changedSlider.Percent, 0, maxAllowed);

        if (clampedValue != changedSlider.Percent)
        {
            changedSlider.SetPercent(clampedValue, false);
        }

        ApplyBudgets();
        UpdateAllVisuals();
    }

    private void ApplyBudgets()
    {
        foreach (BudgetSliderUI slider in sliders)
        {
            int money = Mathf.RoundToInt(budgetManager.startingBalance * (slider.Percent / 100f));
            budgetManager.SetCategoryBudget(slider.Category, money);
        }
    }

    

    private void UpdateAllVisuals()
    {
        foreach (BudgetSliderUI slider in sliders)
        {
            slider.UpdateText(slider.Percent);
            slider.UpdateMoney(budgetManager.startingBalance);
        }

        if (distributionBarUI != null)
        {
            distributionBarUI.UpdateBar();
        }

        if (budgetSummaryUI != null)
        {
            budgetSummaryUI.UpdateSummary();
        }
    }

    private int GetTotalPercent()
    {
        int total = 0;

        foreach (BudgetSliderUI slider in sliders)
        {
            total += slider.Percent;
        }

        return total;
    }

    private int GetTotalPercentExcept(BudgetSliderUI excludedSlider)
    {
        int total = 0;

        foreach (BudgetSliderUI slider in sliders)
        {
            if (slider == excludedSlider) continue;

            total += slider.Percent;
        }

        return total;
    }
}