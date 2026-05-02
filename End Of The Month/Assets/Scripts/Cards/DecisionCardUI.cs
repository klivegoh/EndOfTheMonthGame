using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecisionCardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button button;

    private DailyChoiceData choiceData;
    private GameplayManager gameplayManager;

    public void Setup(DailyChoiceData data, GameplayManager manager)
    {
        choiceData = data;
        gameplayManager = manager;

        nameText.text = data.choiceName;
        descriptionText.text = data.description;

        if (data.cost < 0)
        {
            costText.text = "+$" + Mathf.Abs(data.cost);
        }
        else if (data.cost == 0)
        {
            costText.text = "$0";
        }
        else
        {
            costText.text = "-$" + data.cost;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        gameplayManager.PlayChoice(choiceData);
    }
}
