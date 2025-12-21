using TMPro;
using UnityEngine;

public class QuestCardUI : MonoBehaviour
{
    [Header("Elementy UI")]
    [SerializeField] private TextMeshProUGUI clientName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI reward;
    [SerializeField] private TextMeshProUGUI potionName;
    [SerializeField] private TextMeshProUGUI timeText;
    private GuildQuestSO questData;
    private QuestManager manager;

    public void Setup(GuildQuestSO data, QuestManager managerRef)
    {
        questData = data;
        manager = managerRef;

        if (clientName != null)
            clientName.text = data.clientName;

        if (description != null)
            description.text = data.requestDescription;

        if (reward != null)
            reward.text = data.goldReward.ToString();

        if (potionName != null && data.requiredPotion != null)
            potionName.text = data.requiredPotion.recipeName;
        if (timeText != null)
            timeText.text = data.timeLimit.ToString() + " s";
    }

    public void OnClick()
    {
        if (manager != null && questData != null)
        {
            manager.AcceptQuest(questData);
            Destroy(gameObject);
        }
    }
}