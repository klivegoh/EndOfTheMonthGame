[System.Serializable]
public class RewardData
{
    public string rewardID;
    public string rewardName;
    public string triggerCondition;
    public string rewardType;
    public int rewardValue;
    public string displayMessage;

    public bool isUnlocked;

    public RewardData(
        string rewardID,
        string rewardName,
        string triggerCondition,
        string rewardType,
        int rewardValue,
        string displayMessage)
    {
        this.rewardID = rewardID;
        this.rewardName = rewardName;
        this.triggerCondition = triggerCondition;
        this.rewardType = rewardType;
        this.rewardValue = rewardValue;
        this.displayMessage = displayMessage;

        isUnlocked = false;
    }
}