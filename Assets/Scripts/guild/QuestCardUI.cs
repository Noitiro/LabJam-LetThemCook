using UnityEngine;
using TMPro; 

public class QuestCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clientName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI reward;
    [SerializeField] private TextMeshProUGUI potionName;

    public void Setup(GuildQuestSO data)
    {
        if (clientName != null)
            clientName.text = data.clientName;

        if (description != null)
            description.text = data.requestDescription;

        if (reward != null)
            reward.text = data.goldReward.ToString();

        if (potionName != null && data.requiredPotion != null)
            potionName.text = data.requiredPotion.recipeName;
    }
}