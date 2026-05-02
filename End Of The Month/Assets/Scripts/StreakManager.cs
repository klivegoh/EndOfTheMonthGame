using UnityEngine;

public class StreakManager : MonoBehaviour
{
    [SerializeField] private GameDataManager gameDataManager;
    [SerializeField] private BudgetManager budgetManager;

    public void CheckStreaks(DecisionCardData playedCard = null)
    {
        foreach (StreakData streak in gameDataManager.AllStreaks)
        {
            bool conditionMet = IsConditionMet(streak.streakType, playedCard);

            if (conditionMet)
            {
                streak.currentCount++;

                if (streak.currentCount > streak.bestCount)
                {
                    streak.bestCount = streak.currentCount;
                }
            }
            else
            {
                streak.currentCount = 0;
            }
        }
    }

    private bool IsConditionMet(string streakType, DecisionCardData playedCard)
    {
        switch (streakType)
        {
            

            case "ConsecutiveSavingsActions":
                return playedCard != null &&
                       (playedCard.category == CategoryType.Savings || playedCard.savingsChange > 0);

            case "AllCategoriesWithin80Percent":
                return NoCategoryExceededPercent(0.8f);

            default:
                Debug.LogWarning("Unknown streak type: " + streakType);
                return false;
        }
    }

    private bool NoCategoryExceededPercent(float limit)
    {
        foreach (BudgetCategory category in budgetManager.categories)
        {
            if (category.category == CategoryType.Income)
            {
                continue;
            }

            if (category.allocatedAmount <= 0)
            {
                continue;
            }

            if (category.UsagePercent > limit)
            {
                return false;
            }
        }

        return true;
    }

    public string GetMainStreakDisplay()
    {
        if (gameDataManager.AllStreaks.Count == 0)
        {
            return "Streak: 0";
        }

        StreakData mainStreak = gameDataManager.AllStreaks[0];

        return mainStreak.streakName + ": " + mainStreak.currentCount + " day(s)";
    }

    public int GetBestStreak()
    {
        int best = 0;

        foreach (StreakData streak in gameDataManager.AllStreaks)
        {
            if (streak.bestCount > best)
            {
                best = streak.bestCount;
            }
        }

        return best;
    }
}
