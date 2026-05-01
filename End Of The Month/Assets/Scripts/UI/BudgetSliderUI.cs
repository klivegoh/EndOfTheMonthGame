using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BudgetSliderUI : MonoBehaviour
{
    [SerializeField] private CategoryType category;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text percentText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private BudgetManager budgetManager;

    private void Start()
    {
        slider.onValueChanged.AddListener(UpdateBudget);
        UpdateBudget(slider.value);
    }

    private void UpdateBudget(float value)
    {
        int percent = Mathf.RoundToInt(value * 100);
        int money = Mathf.RoundToInt(budgetManager.startingBalance * value);

        percentText.text = percent + "%";
        moneyText.text = "$" + money;

        budgetManager.SetCategoryBudget(category, money);
    }
}