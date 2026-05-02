using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private GameDataManager gameDataManager;
    [SerializeField] private BudgetManager budgetManager;

    private bool hasReachedLowBalance = false;

    public List<RewardData> CheckRewards()
    {
        List<RewardData> newlyUnlockedRewards = new List<RewardData>();

        UpdateLowBalanceTracking();

        foreach (RewardData reward in gameDataManager.AllRewards)
        {
            if (reward.isUnlocked)
            {
                continue;
            }

            bool conditionMet = IsConditionMet(reward.triggerCondition, reward.rewardValue);

            if (conditionMet)
            {
                reward.isUnlocked = true;
                newlyUnlockedRewards.Add(reward);

                Debug.Log("Reward unlocked: " + reward.rewardName);
            }
        }

        return newlyUnlockedRewards;
    }

    private void UpdateLowBalanceTracking()
    {
        int lowBalanceThreshold = Mathf.RoundToInt(budgetManager.startingBalance * 0.25f);

        if (budgetManager.currentBalance <= lowBalanceThreshold)
        {
            hasReachedLowBalance = true;
        }
    }

    private bool IsConditionMet(string triggerCondition, int rewardValue)
    {
        switch (triggerCondition)
        {
            case "SavingsReachedHalfGoal":
                return HasReachedSavingsAmount(rewardValue);

            case "BalanceAboveAmount":
                return budgetManager.currentBalance >= rewardValue;

            

            case "RecoveredFromLowBalance":
                return hasReachedLowBalance && budgetManager.currentBalance > budgetManager.startingBalance * 0.25f;

            case "UsedAllFourCategories":
                return HasUsedCoreCategories();

            default:
                Debug.LogWarning("Unknown reward trigger condition: " + triggerCondition);
                return false;
        }
    }

    private bool HasReachedSavingsAmount(int targetAmount)
    {
        BudgetCategory savings = budgetManager.GetCategory(CategoryType.Savings);

        if (savings == null)
        {
            return false;
        }

        return savings.spentAmount >= targetAmount;
    }

    private bool HasUsedCoreCategories()
    {
        return HasUsedCategory(CategoryType.Food) &&
               HasUsedCategory(CategoryType.Transport) &&
               HasUsedCategory(CategoryType.Leisure) &&
               HasUsedCategory(CategoryType.Savings);
    }

    private bool HasUsedCategory(CategoryType categoryType)
    {
        BudgetCategory category = budgetManager.GetCategory(categoryType);

        if (category == null)
        {
            return false;
        }

        return category.spentAmount > 0;
    }

    public int GetUnlockedRewardCount()
    {
        int count = 0;

        foreach (RewardData reward in gameDataManager.AllRewards)
        {
            if (reward.isUnlocked)
            {
                count++;
            }
        }

        return count;
    }

    public string GetUnlockedRewardsText()
    {
        string result = "";

        foreach (RewardData reward in gameDataManager.AllRewards)
        {
            if (reward.isUnlocked)
            {
                result += "- " + reward.rewardName + "\n";
            }
        }

        if (string.IsNullOrEmpty(result))
        {
            result = "No rewards unlocked yet.";
        }

        return result;
    }
}
