using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [SerializeField] private Transform cardsContainer;
    [SerializeField] private GameObject questCardPrefab;
    [SerializeField] private List<GuildQuestSO> allQuests;
    [SerializeField] private TextMeshProUGUI activeTimerText;
    [SerializeField] private TextMeshProUGUI currentQuestNameText;

    [SerializeField] private GameObject actionButton;
    [SerializeField] private TextMeshProUGUI actionButtonText; 

    [SerializeField] private OwnedRecipeList RecipeList;

    public GuildQuestSO currentActiveQuest;
    private float timeRemaining;

    private bool isReadyToFinish = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        if (activeTimerText != null) activeTimerText.gameObject.SetActive(false);
        if (actionButton != null) actionButton.gameObject.SetActive(false);
        UpdateQuestHUD();
    }

    private void OnEnable()
    {
        GenerateCards();
    }

    private void Update()
    {
        if (currentActiveQuest != null && !isReadyToFinish)
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

    public void Teges()
    {
        if (currentActiveQuest == null) return;

        if (isReadyToFinish)
        {
            FinishQuestSuccess();

        }
        else
        {
            Debug.Log("Gracz anulowa³ zadanie.");
            FailQuest();
        }
    }

    public void AcceptQuest(GuildQuestSO quest)
    {
        if (currentActiveQuest != null) return;

        currentActiveQuest = quest;
        timeRemaining = quest.timeLimit;

        isReadyToFinish = false;

        if (actionButtonText != null) actionButtonText.text = "Cancel";

        UpdateQuestHUD();
        Debug.Log($"Przyjêto: {quest.requiredPotion.recipeName}");

        if (activeTimerText != null) activeTimerText.gameObject.SetActive(true);
        if (actionButton != null) actionButton.SetActive(true);

        foreach (Transform child in cardsContainer) Destroy(child.gameObject);

    }

    public void CheckQuestCompletion(RecipeSO craftedPotion)
    {
        if (currentActiveQuest == null) return;

        if (isReadyToFinish) return;

        if (currentActiveQuest.requiredPotion == craftedPotion)
        {
            isReadyToFinish = true;
            if (activeTimerText != null) activeTimerText.gameObject.SetActive(false);

            if (actionButtonText != null) actionButtonText.text = "Finish";
            UpdateQuestHUD();
        }
        else
        {
            Debug.Log("Nie ts kurwa");
        }
    }

    private void FinishQuestSuccess()
    {
        Debug.Log("ZADANIE UKOÑCZONE");

        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.AddGold(currentActiveQuest.goldReward);
        }

        ResetQuestState();
    }

    private void FailQuest()
    {
        Debug.Log("Zadanie nieudane lub anulowane.");
        ResetQuestState();
    }

    private void ResetQuestState()
    {
        currentActiveQuest = null;
        isReadyToFinish = false;
        timeRemaining = 0;

        if (activeTimerText != null) activeTimerText.gameObject.SetActive(false);
        if (actionButton != null) actionButton.SetActive(false);

        GenerateCards();
        UpdateQuestHUD();
    }

    private void UpdateQuestHUD()
    {
        if (currentQuestNameText == null) return;

        if (currentActiveQuest != null)
        {
            if (isReadyToFinish)
                currentQuestNameText.text = $"Ready to deliver: {currentActiveQuest.requiredPotion.recipeName}";
            else
                currentQuestNameText.text = $"Current Order: {currentActiveQuest.requiredPotion.recipeName}";
        }
        else
        {
            currentQuestNameText.text = "Visit the Guild for new quests";
        }
    }

    public void GenerateCards()
    {
        foreach (Transform child in cardsContainer) Destroy(child.gameObject);
        if (currentActiveQuest != null) return;

        List<GuildQuestSO> deck = new List<GuildQuestSO>(allQuests);
        int cardsToDraw = Mathf.Min(8, deck.Count);

        float percent = 0.5f;
        int sureCards = (int)Mathf.Min(cardsToDraw * percent, RecipeList.RecipeList.Count);
        int restCards = cardsToDraw - sureCards;

        for (int i = 0; i < sureCards; i++)
        {
            int randomIndex;
            GuildQuestSO drawnQuest;

            if (deck.Count == 0) break;

            int safeCounter = 0;
            while (true)
            {
                randomIndex = Random.Range(0, deck.Count);
                drawnQuest = deck[randomIndex];

                if (RecipeList.IsRecipeOwned(drawnQuest.requiredPotion)) break;

                safeCounter++;
                if (safeCounter > 50) break;
            }

            deck.RemoveAt(randomIndex);
            GameObject cardObj = Instantiate(questCardPrefab, cardsContainer);
            cardObj.GetComponent<QuestCardUI>().Setup(drawnQuest, this);
        }
        for (int i = 0; i < restCards; i++)
        {
            if (deck.Count == 0) break;

            int randomIndex = Random.Range(0, deck.Count);
            GuildQuestSO drawnQuest = deck[randomIndex];
            deck.RemoveAt(randomIndex);

            GameObject cardObj = Instantiate(questCardPrefab, cardsContainer);
            cardObj.GetComponent<QuestCardUI>().Setup(drawnQuest, this);
        }
    }
}