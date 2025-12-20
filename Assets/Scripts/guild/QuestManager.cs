using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Transform cardsContainer;
    [SerializeField] private GameObject questCardPrefab;
    [SerializeField] private List<GuildQuestSO> allQuests;
    private void OnEnable()
    {
        GenerateCards();
    }
    public void GenerateCards()
    {
        foreach (Transform child in cardsContainer)
        {
            Destroy(child.gameObject);
        }

        List<GuildQuestSO> deck = new List<GuildQuestSO>(allQuests);

        int cardsToDraw = Mathf.Min(12, deck.Count);

        for (int i = 0; i < cardsToDraw; i++)
        {
            int randomIndex = Random.Range(0, deck.Count);
            GuildQuestSO drawnQuest = deck[randomIndex];
            deck.RemoveAt(randomIndex);
            GameObject cardObj = Instantiate(questCardPrefab, cardsContainer);
            cardObj.GetComponent<QuestCardUI>().Setup(drawnQuest, this);
        }
    }

    public void AcceptQuest(GuildQuestSO quest)
    {
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.AddGold(quest.goldReward);
        }

        Debug.Log(quest.goldReward);
    }
}