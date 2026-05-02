[System.Serializable]
public class DailyChoiceData
{
    public string choiceName;
    public CategoryType category;
    public int cost;
    public string description;
    public string feedbackText;

    public DailyChoiceData(
        string choiceName,
        CategoryType category,
        int cost,
        string description,
        string feedbackText)
    {
        this.choiceName = choiceName;
        this.category = category;
        this.cost = cost;
        this.description = description;
        this.feedbackText = feedbackText;
    }
}
