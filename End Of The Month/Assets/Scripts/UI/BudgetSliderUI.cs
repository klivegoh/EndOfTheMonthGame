using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BudgetSliderUI : MonoBehaviour
{
    [SerializeField] private CategoryType category;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text percentText;
    [SerializeField] private TMP_Text moneyText;

    private BudgetAllocationController controller;
    private bool isUpdating;

    public CategoryType Category => category;
    public int Percent => Mathf.RoundToInt(slider.value);

    public void Init(BudgetAllocationController controllerRef, int defaultPercent)
    {
        controller = controllerRef;

        slider.wholeNumbers = true;
        slider.minValue = 0;
        slider.maxValue = 100;

        SetPercent(defaultPercent, false);

        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnSliderChanged(float value)
    {
        if (isUpdating) return;

        controller.OnSliderChanged(this);
    }

    public void SetPercent(int percent, bool notify)
    {
        isUpdating = true;

        slider.value = percent;
        UpdateText(percent);

        isUpdating = false;

        if (notify && controller != null)
        {
            controller.OnSliderChanged(this);
        }
    }

    

    public void UpdateText(int percent)
    {
        percentText.text = percent + "%";
    }

    public void UpdateMoney(int startingBalance)
    {
        int money = Mathf.RoundToInt(startingBalance * (Percent / 100f));
        moneyText.text = "$" + money;
    }
}