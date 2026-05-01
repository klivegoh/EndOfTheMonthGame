using UnityEngine;

[CreateAssetMenu(fileName = "DecisionCard", menuName = "Game/Decision Card")]
public class DecisionCardData : ScriptableObject
{
    public string cardName;
    public CategoryType category;
    public int cost;
    [TextArea] public string description;
    [TextArea] public string feedbackText;
}