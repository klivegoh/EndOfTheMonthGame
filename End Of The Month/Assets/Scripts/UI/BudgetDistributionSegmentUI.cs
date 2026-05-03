using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BudgetDistributionSegmentUI : MonoBehaviour
{
    public CategoryType category;
    public LayoutElement layoutElement;
    public TMP_Text labelText;
    public bool isUnallocated;

    public void UpdateSegment(BudgetCategory data, int startingBalance, int percentOverride = -1)
    {
        int displayPercent = 0;

        if (percentOverride >= 0)
        {
            displayPercent = percentOverride;
        }
        else if (data != null && startingBalance > 0)
        {
            float percent = (float)data.allocatedAmount / startingBalance;
            displayPercent = Mathf.RoundToInt(percent * 100f);
        }

        layoutElement.minWidth = 0;
        layoutElement.preferredWidth = 0;
        layoutElement.flexibleWidth = Mathf.Max(displayPercent, 0);

        if (labelText != null)
        {
            labelText.text = displayPercent > 0 ? displayPercent + "%" : "";
        }
    }
}