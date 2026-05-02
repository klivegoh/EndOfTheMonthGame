[System.Serializable]
public class DailyChoiceData
{
    public string choiceName;
    public CategoryType category;
    public int cost;
    public string feedbackText;

    public DailyChoiceData(string choiceName, CategoryType category, int cost, string feedbackText)
    {
        this.choiceName = choiceName;
        this.category = category;
        this.cost = cost;
        this.feedbackText = feedbackText;
    }
}
