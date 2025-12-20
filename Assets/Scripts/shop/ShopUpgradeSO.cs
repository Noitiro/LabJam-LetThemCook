using UnityEngine;

public enum UpgradeType
{
    cos,
    tam
}
[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade Card")]
public class ShopUpgradeSO : ScriptableObject
{
    public string upgradeName;      
    [TextArea] public string description;
    public int cost; 

    public UpgradeType type;
    public float value;
}
