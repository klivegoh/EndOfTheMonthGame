[System.Serializable]
public class StreakData
{
    public string streakID;
    public string streakName;
    public string streakType;
    public int rewardPerDay;
    public int unlockAfterDays;
    public string bonusMessage;

    public int currentCount;
    public int bestCount;

    public StreakData(
        string streakID,
        string streakName,
        string streakType,
        int rewardPerDay,
        int unlockAfterDays,
        string bonusMessage)
    {
        this.streakID = streakID;
        this.streakName = streakName;
        this.streakType = streakType;
        this.rewardPerDay = rewardPerDay;
        this.unlockAfterDays = unlockAfterDays;
        this.bonusMessage = bonusMessage;

        currentCount = 0;
        bestCount = 0;
    }
}