using UnityEngine;

public class BudgetSetupController : MonoBehaviour
{
    [SerializeField] private BudgetManager budgetManager;
    [SerializeField] private GameObject setupScreen;
    [SerializeField] private GameObject gameplayScreen;
    [SerializeField] private GameplayManager gameplayManager;

    public void ConfirmBudget()
    {
        setupScreen.SetActive(false);
        gameplayScreen.SetActive(true);

        gameplayManager.BeginRun();
    }

    public int ConvertPercentToMoney(float percent)
    {
        return Mathf.RoundToInt(budgetManager.startingBalance * percent);
    }
}