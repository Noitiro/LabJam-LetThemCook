using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [SerializeField] private Transform cardsContainer;
    [SerializeField] private GameObject questCardPrefab;
    [SerializeField] private TextMeshProUGUI activeTimerText;
    [SerializeField] private List<GuildQuestSO> allQuests;

    public GuildQuestSO currentActiveQuest;
    private float timeRemaining;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }
    private void Start()
    {
        if (activeTimerText != null) activeTimerText.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        GenerateCards();
    }
    private void Update()
    {
        if (currentActiveQuest != null)
        {
            timeRemaining -= Time.deltaTime; 
            if (activeTimerText != null)
            {
                activeTimerText.text = timeRemaining.ToString("F1") + "s";
            }
            if (timeRemaining <= 0)
            {
                FailQuest();
            }
        }
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
        timeRemaining = quest.timeLimit;
        Debug.Log(quest.requiredPotion.recipeName);
        if (activeTimerText != null) activeTimerText.gameObject.SetActive(true);
        foreach (Transform child in cardsContainer)
        {
            Destroy(child.gameObject);
        }
    }
    private void FailQuest()
    {
        currentActiveQuest = null;
        if (activeTimerText != null) activeTimerText.gameObject.SetActive(false);
        GenerateCards();
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
                GenerateCards();

            }

            currentActiveQuest = null;
            if (activeTimerText != null) activeTimerText.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("nie ta mikstura");
        }
    }
}