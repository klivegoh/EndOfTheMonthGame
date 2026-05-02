using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BudgetDistributionSegmentUI : MonoBehaviour
{
    public CategoryType category;
    public LayoutElement layoutElement;
    public TMP_Text labelText;

    public void UpdateSegment(BudgetCategory data, int startingBalance)
    {
        if (data == null || startingBalance <= 0) return;

        float percent = (float)data.allocatedAmount / startingBalance;
        int displayPercent = Mathf.RoundToInt(percent * 100f);

        layoutElement.minWidth = 0;
        layoutElement.preferredWidth = 0;
        layoutElement.flexibleWidth = Mathf.Max(displayPercent, 0);

        if (labelText != null)
        {
            labelText.text = displayPercent + "%";
        }
    }
}