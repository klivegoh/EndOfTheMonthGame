using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [Header("Resource Paths")]
    [SerializeField] private string dailyEventPlanCSVPath = "Data/DailyEventPlan";
    [SerializeField] private string settingsCSVPath = "Data/Settings";
    [SerializeField] private string categoriesCSVPath = "Data/Categories";
    [SerializeField] private string streaksCSVPath = "Data/Streaks";
    [SerializeField] private string rewardsCSVPath = "Data/Rewards";

    public List<DailyEventPlanData> AllDailyPlans { get; private set; } = new List<DailyEventPlanData>();
    public List<StreakData> AllStreaks { get; private set; } = new List<StreakData>();
    public List<RewardData> AllRewards { get; private set; } = new List<RewardData>();

    // Category default budget amounts loaded from Categories.csv.
    // Example: FOOD -> 35, TRANSPORT -> 25, etc.
    public Dictionary<CategoryType, int> CategoryDefaults { get; private set; } = new Dictionary<CategoryType, int>();

    private Dictionary<string, string> settings = new Dictionary<string, string>();

    private void Awake()
    {
        LoadAllData();
    }

    private void LoadAllData()
    {
        LoadSettings();
        LoadCategories();
        LoadDailyEventPlan();
        LoadStreaks();
        LoadRewards();
    }

    private void LoadSettings()
    {
        TextAsset settingsCSV = Resources.Load<TextAsset>(settingsCSVPath);

        if (settingsCSV == null)
        {
            Debug.LogWarning("Settings CSV could not be loaded from Resources path: " + settingsCSVPath);
            return;
        }

        List<string[]> rows = CSVReader.ReadCSV(settingsCSV.text);
        settings.Clear();

        // Expected header: Key, Value, Type
        for (int i = 1; i < rows.Count; i++)
        {
            string[] row = rows[i];

            if (row.Length < 2)
            {
                Debug.LogWarning("Skipping invalid settings row at index: " + i + " | Columns found: " + row.Length);
                continue;
            }

            string key = Clean(row[0]);
            string value = Clean(row[1]);

            if (!string.IsNullOrEmpty(key))
            {
                settings[key] = value;
            }
        }

        Debug.Log("Loaded settings from CSV: " + settings.Count);
    }

    private void LoadCategories()
    {
        TextAsset categoriesCSV = Resources.Load<TextAsset>(categoriesCSVPath);

        if (categoriesCSV == null)
        {
            Debug.LogWarning("Categories CSV could not be loaded from Resources path: " + categoriesCSVPath);
            return;
        }

        List<string[]> rows = CSVReader.ReadCSV(categoriesCSV.text);
        CategoryDefaults.Clear();

        // Expected header: CategoryID, DisplayName, DefaultBudgetAmount, IsEssential, ColorHex
        for (int i = 1; i < rows.Count; i++)
        {
            string[] row = rows[i];

            if (row.Length < 3)
            {
                Debug.LogWarning("Skipping invalid category row at index: " + i + " | Columns found: " + row.Length);
                continue;
            }

            CategoryType category = ParseCategory(row[0]);
            int defaultBudgetAmount = ParseInt(row[2]);

            CategoryDefaults[category] = defaultBudgetAmount;
        }

        Debug.Log("Loaded category defaults from CSV: " + CategoryDefaults.Count);
    }

    private void LoadDailyEventPlan()
    {
        TextAsset dailyCSV = Resources.Load<TextAsset>(dailyEventPlanCSVPath);

        if (dailyCSV == null)
        {
            Debug.LogError("DailyEventPlan CSV could not be loaded from Resources path: " + dailyEventPlanCSVPath);
            return;
        }

        List<string[]> rows = CSVReader.ReadCSV(dailyCSV.text);
        AllDailyPlans.Clear();

        // Expected header:
        // Day, DayName, ScenarioName, ScenarioText,
        // Choice1Name, Choice1Category, Choice1Cost, Choice1Feedback,
        // Choice2Name, Choice2Category, Choice2Cost, Choice2Feedback,
        // Choice3Name, Choice3Category, Choice3Cost, Choice3Feedback,
        // DesignPurpose
        for (int i = 1; i < rows.Count; i++)
        {
            string[] row = rows[i];

            if (row.Length < 17)
            {
                Debug.LogWarning("Skipping invalid daily plan row at index: " + i + " | Columns found: " + row.Length);
                continue;
            }

            List<DailyChoiceData> choices = new List<DailyChoiceData>
            {
                new DailyChoiceData(row[4], ParseCategory(row[5]), ParseInt(row[6]), row[7]),
                new DailyChoiceData(row[8], ParseCategory(row[9]), ParseInt(row[10]), row[11]),
                new DailyChoiceData(row[12], ParseCategory(row[13]), ParseInt(row[14]), row[15])
            };

            DailyEventPlanData plan = new DailyEventPlanData(
                ParseInt(row[0]),
                row[1],
                row[2],
                row[3],
                choices,
                row[16]
            );

            AllDailyPlans.Add(plan);
        }

        Debug.Log("Loaded daily event plans from CSV: " + AllDailyPlans.Count);
    }

    private void LoadStreaks()
    {
        TextAsset streaksCSV = Resources.Load<TextAsset>(streaksCSVPath);

        if (streaksCSV == null)
        {
            Debug.LogWarning("Streaks CSV could not be loaded from Resources path: " + streaksCSVPath);
            return;
        }

        List<string[]> rows = CSVReader.ReadCSV(streaksCSV.text);
        AllStreaks.Clear();

        // Expected header: StreakID, StreakName, StreakType, RewardPerDay, UnlockAfterDays, BonusMessage
        for (int i = 1; i < rows.Count; i++)
        {
            string[] row = rows[i];

            if (row.Length < 6)
            {
                Debug.LogWarning("Skipping invalid streak row at index: " + i + " | Columns found: " + row.Length);
                continue;
            }

            StreakData streakData = new StreakData(
                row[0],
                row[1],
                row[2],
                ParseInt(row[3]),
                ParseInt(row[4]),
                row[5]
            );

            AllStreaks.Add(streakData);
        }

        Debug.Log("Loaded streaks from CSV: " + AllStreaks.Count);
    }

    private void LoadRewards()
    {
        TextAsset rewardsCSV = Resources.Load<TextAsset>(rewardsCSVPath);

        if (rewardsCSV == null)
        {
            Debug.LogWarning("Rewards CSV could not be loaded from Resources path: " + rewardsCSVPath);
            return;
        }

        List<string[]> rows = CSVReader.ReadCSV(rewardsCSV.text);
        AllRewards.Clear();

        // Expected header: RewardID, RewardName, TriggerCondition, RewardType, RewardValue, DisplayMessage
        for (int i = 1; i < rows.Count; i++)
        {
            string[] row = rows[i];

            if (row.Length < 6)
            {
                Debug.LogWarning("Skipping invalid reward row at index: " + i + " | Columns found: " + row.Length);
                continue;
            }

            RewardData rewardData = new RewardData(
                row[0],
                row[1],
                row[2],
                row[3],
                ParseInt(row[4]),
                row[5]
            );

            AllRewards.Add(rewardData);
        }

        Debug.Log("Loaded rewards from CSV: " + AllRewards.Count);
    }

    public DailyEventPlanData GetPlanForDay(int day)
    {
        return AllDailyPlans.Find(plan => plan.day == day);
    }

    public int GetIntSetting(string key, int fallbackValue = 0)
    {
        if (!settings.ContainsKey(key))
        {
            return fallbackValue;
        }

        return ParseInt(settings[key], fallbackValue);
    }

    public int GetCategoryDefault(CategoryType category, int fallbackValue = 0)
    {
        if (CategoryDefaults.ContainsKey(category))
        {
            return CategoryDefaults[category];
        }

        return fallbackValue;
    }

    private CategoryType ParseCategory(string value)
    {
        string cleanedValue = Clean(value).ToUpper();

        switch (cleanedValue)
        {
            case "FOOD":
                return CategoryType.Food;

            case "TRANSPORT":
            case "TRAN":
                return CategoryType.Transport;

            case "LEISURE":
                return CategoryType.Leisure;

            case "SAVINGS":
                return CategoryType.Savings;

            case "INCOME":
                return CategoryType.Income;

            default:
                Debug.LogWarning("Unknown category: " + value + ". Defaulting to Food.");
                return CategoryType.Food;
        }
    }

    private int ParseInt(string value, int fallbackValue = 0)
    {
        int result;

        if (int.TryParse(Clean(value), out result))
        {
            return result;
        }

        return fallbackValue;
    }

    private string Clean(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return "";
        }

        return value.Replace("\uFEFF", "").Trim();
    }
}
