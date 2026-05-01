using UnityEngine;

[System.Serializable]
public class DecisionCardData
{
    public string cardID;
    public string cardName;
    public CategoryType category;
    public string cardType;
    public int cost;
    public int savingsChange;
    public int weight;
    public int minDay;
    public int maxDay;
    public bool isActive;
    public string description;
    public string feedbackText;

    public DecisionCardData(
        string cardID,
        string cardName,
        CategoryType category,
        string cardType,
        int cost,
        int savingsChange,
        int weight,
        int minDay,
        int maxDay,
        bool isActive,
        string description,
        string feedbackText)
    {
        this.cardID = cardID;
        this.cardName = cardName;
        this.category = category;
        this.cardType = cardType;
        this.cost = cost;
        this.savingsChange = savingsChange;
        this.weight = weight;
        this.minDay = minDay;
        this.maxDay = maxDay;
        this.isActive = isActive;
        this.description = description;
        this.feedbackText = feedbackText;
    }
}