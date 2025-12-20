using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [SerializeField] private GameObject questBoardPanel;
    [SerializeField] private Transform cardsContainer;
    [SerializeField] private GameObject questCardPrefab;

    [SerializeField] private List<GuildQuestSO> allQuests;

    public GuildQuestSO currentActiveQuest;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

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
        if (currentActiveQuest != null)
        {
            Debug.Log("aktywne zadanie");
            return;
        }

        List<GuildQuestSO> deck = new List<GuildQuestSO>(allQuests);
        int cardsToDraw = Mathf.Min(8, deck.Count);

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
        if (currentActiveQuest != null) return;
        currentActiveQuest = quest;
        Debug.Log(quest.requiredPotion.recipeName);

        foreach (Transform child in cardsContainer)
        {
            Destroy(child.gameObject);
        }
        if (questBoardPanel != null)
            questBoardPanel.SetActive(false);
    }

    public void CheckQuestCompletion(RecipeSO craftedPotion)
    {
        if (currentActiveQuest == null) return;

        if (currentActiveQuest.requiredPotion == craftedPotion)
        {
            Debug.Log("ZADANIE UKOCZONE");
            if (MoneyManager.Instance != null)
            {
                MoneyManager.Instance.AddGold(currentActiveQuest.goldReward);
            }

            currentActiveQuest = null;
        }
        else
        {
            Debug.Log("nie ta mikstura");
        }
    }
}