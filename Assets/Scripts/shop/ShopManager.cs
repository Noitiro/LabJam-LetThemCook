using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [SerializeField] private Transform cardsContainer;
    [SerializeField] private GameObject shopCardPrefab;
    [SerializeField] private OwnedRecipeList ownedRecipeList;
    [SerializeField] private List<ShopUpgradeSO> allUpgrades;

    [SerializeField] private GameObject athanorObject;
    [SerializeField] private GameObject alembicObject;
    [SerializeField] private GameObject mortarObject;
    private void Awake() { Instance = this; }

    private void OnEnable()
    {
        GenerateShopCards();
    }

    public void GenerateShopCards()
    {
        foreach (Transform child in cardsContainer) Destroy(child.gameObject);

        List<ShopUpgradeSO> priorityUpgrades = new List<ShopUpgradeSO>();
        List<ShopUpgradeSO> randomUpgradesPool = new List<ShopUpgradeSO>();

        foreach (var upgrade in allUpgrades)
        {
            if (upgrade.type == UpgradeType.MachineUnlock)
            {
                priorityUpgrades.Add(upgrade);
            }
            else
            {
                randomUpgradesPool.Add(upgrade);
            }
        }

        foreach (var priorityItem in priorityUpgrades)
        {
            GameObject cardObj = Instantiate(shopCardPrefab, cardsContainer);
            cardObj.GetComponent<ShopCardUI>().Setup(priorityItem, this);
        }
        int maxSlots = 12;
        int slotsRemaining = maxSlots - priorityUpgrades.Count;
        int cardsToDraw = Mathf.Min(slotsRemaining, randomUpgradesPool.Count);

        for (int i = 0; i < cardsToDraw; i++)
        {
            int randomIndex = Random.Range(0, randomUpgradesPool.Count);
            ShopUpgradeSO drawnUpgrade = randomUpgradesPool[randomIndex];
            randomUpgradesPool.RemoveAt(randomIndex);
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
        switch (upgrade.type)
        {
            case UpgradeType.RecipeUnlock:
                if (upgrade.recipeReward != null && ownedRecipeList != null)
                {
                    ownedRecipeList.AddRecipe(upgrade.recipeReward);
                }
                break;

            case UpgradeType.MachineUnlock:
                UnlockMachine(upgrade.machineToUnlock);
                break;

            case UpgradeType.CookingSpeed:
                ApplySpeedBoost(upgrade.value);
                break;
        }
    }
    private void UnlockMachine(AlchemyEnums.Instruments machineType)
    {
        switch (machineType)
        {
            case AlchemyEnums.Instruments.Alembic:
                if (alembicObject != null) alembicObject.SetActive(true);
                Debug.Log("Odblokowano Alembic!");
                break;

            case AlchemyEnums.Instruments.Mortar:
                if (mortarObject != null) mortarObject.SetActive(true);
                Debug.Log("Odblokowano Mortar!");
                break;

            case AlchemyEnums.Instruments.Athanor:
                if (athanorObject != null) athanorObject.SetActive(true);
                Debug.Log("Odblokowano Athanor!");
                break;
        }
    }
    private void ApplySpeedBoost(float amount)
    {
        if (athanorObject != null)
            athanorObject.GetComponent<AddingIngredients>().ReduceCookingTime(amount);

        if (alembicObject != null)
            alembicObject.GetComponent<AddingIngredients>().ReduceCookingTime(amount);

        if (mortarObject != null)
            mortarObject.GetComponent<AddingIngredients>().ReduceCookingTime(amount);
    }
}
