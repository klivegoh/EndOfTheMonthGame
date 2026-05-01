using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [Header("Resource Paths")]
    [SerializeField] private string cardsCSVPath = "Data/Cards";

    public List<DecisionCardData> AllCards { get; private set; } = new List<DecisionCardData>();

    private void Awake()
    {
        LoadAllData();
    }

    private void LoadAllData()
    {
        LoadCards();
    }

    private void LoadCards()
    {
        TextAsset cardsCSV = Resources.Load<TextAsset>(cardsCSVPath);

        if (cardsCSV == null)
        {
            Debug.LogError("Cards CSV could not be loaded from Resources path: " + cardsCSVPath);
            return;
        }

        List<string[]> rows = CSVReader.ReadCSV(cardsCSV.text);

        AllCards.Clear();

        // Skip header row.
        for (int i = 1; i < rows.Count; i++)
        {
            string[] row = rows[i];

            // Expected header:
            // index,CardID,CardName,CategoryID,CardType,Cost,SavingsChange,Weight,MinDay,MaxDay,...
            if (row.Length < 14)
            {
                Debug.LogWarning("Skipping invalid card row at index: " + i + " | Columns found: " + row.Length);
                continue;
            }

            string cardID = row[0];
            string cardName = row[1];
            CategoryType category = ParseCategory(row[2]);
            string cardType = row[3];

            int cost = ParseInt(row[4]);
            int savingsChange = ParseInt(row[5]);
            int weight = ParseInt(row[6]);
            int minDay = ParseInt(row[7]);
            int maxDay = ParseInt(row[8]);

            bool isActive = ParseBool(row[11]);

            string shortText = row[12];
            string feedbackText = row[13];

            if (!isActive)
            {
                continue;
            }

            DecisionCardData card = new DecisionCardData(
                cardID,
                cardName,
                category,
                cardType,
                cost,
                savingsChange,
                weight,
                minDay,
                maxDay,
                isActive,
                shortText,
                feedbackText
            );

            AllCards.Add(card);
        }

        Debug.Log("Loaded cards from CSV: " + AllCards.Count);
    }

    private CategoryType ParseCategory(string value)
    {
        switch (value.Trim().ToUpper())
        {
            case "FOOD":
                return CategoryType.Food;

            case "TRANSPORT":
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

    private int ParseInt(string value)
    {
        int result;
        int.TryParse(value, out result);
        return result;
    }

    private bool ParseBool(string value)
    {
        return value.Trim().ToLower() == "true";
    }
}