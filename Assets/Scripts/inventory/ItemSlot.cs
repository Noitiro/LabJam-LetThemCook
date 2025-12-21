using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public InventorySystem Inventory;
    public Button button;

    public AlchemyEnums.Ingredients? itemInSlot = null;
    public Sprite itemImage = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickSlot()
    {
        if (itemInSlot == null)
        {
            Debug.Log("Dropping item");
            Inventory.dropItem(this);
        }
        else
        {
            Debug.Log("Picking up item");
            Inventory.pickItem(this);
        }
    }

    public void ClearSlot()
    {
        button.GetComponent<Image>().sprite = null;
        itemInSlot = null;
        itemImage = null;
    }

    public void setTestItem()
    {
        itemInSlot = AlchemyEnums.Ingredients.Salt;
    }
}
