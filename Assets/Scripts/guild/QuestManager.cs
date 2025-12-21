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
    [SerializeField] private TextMeshProUGUI currentQuestNameText;

    [SerializeField] private OwnedRecipeList RecipeList;

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
        UpdateQuestHUD();
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

        // Trzeba zagwarantowaæ, ¿e przynajmniej czêœæ zadañ bêdzie mo¿liwa do wykonania, zmienna percent pokazuje jaka to iloœæ
        // Mo¿na by próbowaæ generowaæ te¿ procent zadañ, które mog¹ byæ wykonane po zakupie recept, ale komplikuje to sprawê
        // Maybe TODO in future
        float percent = 0.5f;
        int sureCards = (int)Mathf.Min(cardsToDraw * percent, RecipeList.RecipeList.Count);
        int restCards = cardsToDraw - sureCards;

        Debug.Log(sureCards.ToString());

        //Stare zwyk³e randomowe generowanie
        /*for (int i = 0; i < cardsToDraw; i++)
        {
            int randomIndex = Random.Range(0, deck.Count);
            GuildQuestSO drawnQuest = deck[randomIndex];
            deck.RemoveAt(randomIndex);

            GameObject cardObj = Instantiate(questCardPrefab, cardsContainer);
            cardObj.GetComponent<QuestCardUI>().Setup(drawnQuest, this);
        }*/

        //Generowanie zadañ gwarantowanych
        for (int i = 0; i < sureCards; i++)
        {
            int randomIndex;
            GuildQuestSO drawnQuest;

            while (true)
            {
                randomIndex = Random.Range(0, deck.Count);
                drawnQuest = deck[randomIndex];

                if (RecipeList.IsRecipeOwned(drawnQuest.requiredPotion)) break;
            }

            deck.RemoveAt(randomIndex);

            GameObject cardObj = Instantiate(questCardPrefab, cardsContainer);
            cardObj.GetComponent<QuestCardUI>().Setup(drawnQuest, this);
        }

        //Generowanie reszty zadañ
        for (int i = 0; i < restCards; i++)
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
        UpdateQuestHUD();
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
            }

            currentActiveQuest = null;
            GenerateCards();
            UpdateQuestHUD();
            if (activeTimerText != null) activeTimerText.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("nie ta mikstura");
        }
    }
    private void UpdateQuestHUD()
    {
        if (currentQuestNameText == null) return;

        if (currentActiveQuest != null)
        {
            currentQuestNameText.text = $"Current Order: {currentActiveQuest.requiredPotion.recipeName}";
        }
        else
        {
            currentQuestNameText.text = "Visit the Guild for new quests";
        }
    }
}