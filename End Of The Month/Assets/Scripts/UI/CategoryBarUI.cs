using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CategoryBarUI : MonoBehaviour
{
    [SerializeField] private CategoryType category;
    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text labelText;
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private TMP_Text warningText;

    public CategoryType Category => category;

    public void UpdateBar(BudgetCategory data)
    {
        if (data == null) return;

        float usage = data.UsagePercent;

        fillImage.fillAmount = Mathf.Clamp01(usage);

        int percent = Mathf.RoundToInt(usage * 100);

        labelText.text = data.category.ToString();
        valueText.text = percent + "%  $" + data.spentAmount + " / $" + data.allocatedAmount;

        bool overBudget = data.IsOverBudget;
        warningText.gameObject.SetActive(overBudget);
        warningText.text = overBudget ? "OVER" : "";
    }
}