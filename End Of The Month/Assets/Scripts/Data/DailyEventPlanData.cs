using System.Collections.Generic;

[System.Serializable]
public class DailyEventPlanData
{
    public int day;
    public string dayName;
    public string scenarioName;
    public string scenarioText;
    public List<DailyChoiceData> choices = new List<DailyChoiceData>();
    public string designPurpose;

    public DailyEventPlanData(
        int day,
        string dayName,
        string scenarioName,
        string scenarioText,
        List<DailyChoiceData> choices,
        string designPurpose)
    {
        this.day = day;
        this.dayName = dayName;
        this.scenarioName = scenarioName;
        this.scenarioText = scenarioText;
        this.choices = choices;
        this.designPurpose = designPurpose;
    }
}
