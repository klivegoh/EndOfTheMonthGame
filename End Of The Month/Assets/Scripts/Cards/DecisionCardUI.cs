using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecisionCardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button button;

    private DecisionCardData cardData;
    private GameplayManager gameplayManager;

    public void Setup(DecisionCardData data, GameplayManager manager)
    {
        cardData = data;
        gameplayManager = manager;

        nameText.text = data.cardName;
        costText.text = "-$" + data.cost;
        descriptionText.text = data.description;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        gameplayManager.PlayCard(cardData);
    }
}