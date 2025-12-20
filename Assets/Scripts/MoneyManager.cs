using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    public int currentGold = 0;
    [SerializeField] private TextMeshProUGUI goldDisplay;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        UpdateUI(); 
    }
    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateUI();
        Debug.Log($"Zarobiono {amount}. Razem: {currentGold}");
    }

    public bool TrySpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            UpdateUI();
            Debug.Log($"Wydano {amount}. Zosta³o: {currentGold}");
            return true;
        }
        else
        {
            Debug.Log("Nie Dla PSA! Brakuje: " + (amount - currentGold));
            return false; 
        }
    }

    private void UpdateUI()
    {
        if (goldDisplay != null)
        {
            goldDisplay.text = currentGold.ToString();
        }
    }
}