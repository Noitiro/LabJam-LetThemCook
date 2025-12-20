using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [SerializeField] private Transform cardsContainer;
    [SerializeField] private GameObject shopCardPrefab;

    [SerializeField] private List<ShopUpgradeSO> allUpgrades; 

    private void Awake() { Instance = this; }

    private void OnEnable()
    {
        GenerateShopCards();
    }

    public void GenerateShopCards()
    {
        foreach (Transform child in cardsContainer) Destroy(child.gameObject);

        List<ShopUpgradeSO> deck = new List<ShopUpgradeSO>(allUpgrades);
        int cardsToDraw = Mathf.Min(12, deck.Count);

        for (int i = 0; i < cardsToDraw; i++)
        {
            int randomIndex = Random.Range(0, deck.Count);
            ShopUpgradeSO drawnUpgrade = deck[randomIndex];
            deck.RemoveAt(randomIndex);
            GameObject cardObj = Instantiate(shopCardPrefab, cardsContainer);
            cardObj.GetComponent<ShopCardUI>().Setup(drawnUpgrade, this);
        }
    }


    public void TryBuyUpgrade(ShopUpgradeSO upgrade)
    {
        if (MoneyManager.Instance.TrySpendGold(upgrade.cost))
        {
            Debug.Log($"Kupiono: {upgrade.upgradeName}");
            ApplyUpgradeStats(upgrade);
            allUpgrades.Remove(upgrade);
            GenerateShopCards();
        }
        else
        {
            Debug.Log("Nie dla psa!");
        }
    }

    private void ApplyUpgradeStats(ShopUpgradeSO upgrade)
    {
    }
}
