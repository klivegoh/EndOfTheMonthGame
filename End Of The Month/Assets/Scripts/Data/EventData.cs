[System.Serializable]
public class EventData
{
    public string eventID;
    public string eventName;
    public CategoryType category;
    public string cardType;
    public int minDay;
    public int maxDay;
    public string eventText;
    public int weight;

    public EventData(
        string eventID,
        string eventName,
        CategoryType category,
        string cardType,
        int minDay,
        int maxDay,
        string eventText,
        int weight)
    {
        this.eventID = eventID;
        this.eventName = eventName;
        this.category = category;
        this.cardType = cardType;
        this.minDay = minDay;
        this.maxDay = maxDay;
        this.eventText = eventText;
        this.weight = weight;
    }
}
