using TMPro;
using UnityEngine;

public class ShopCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;

    private ShopUpgradeSO upgradeData;
    private ShopManager manager;

    public void Setup(ShopUpgradeSO data, ShopManager managerRef)
    {
        upgradeData = data;
        manager = managerRef;

        if (nameText) nameText.text = data.upgradeName;
        if (descriptionText) descriptionText.text = data.description;
        if (costText) costText.text = data.cost.ToString() + " $";
    }

    public void OnClick()
    {
        if (manager != null)
        {
            manager.TryBuyUpgrade(upgradeData);
        }
    }
}