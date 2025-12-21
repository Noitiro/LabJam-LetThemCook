using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public InventorySystem Inventory;
    public Button button;
    public TextMeshProUGUI nameText;

    public AlchemyEnums.Ingredients itemInSlot = AlchemyEnums.Ingredients.Null;
    public Sprite itemImage = null;

    public bool isSource = false;
    public int source = 0;
    public bool isOutput = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isSource)
        {
            switch(source)
            {
                case 0:
                    itemInSlot = AlchemyEnums.Ingredients.Salt;
                    nameText.text = "Salt";
                    break;

                case 1:
                    itemInSlot = AlchemyEnums.Ingredients.Mercury;
                    nameText.text = "Mercury";
                    break;

                case 2:
                    itemInSlot = AlchemyEnums.Ingredients.Sulphur;
                    nameText.text = "Sulphur";
                    break;
            }

            button.GetComponent<Image>().sprite = itemImage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickSlot()
    {
        if (itemInSlot == AlchemyEnums.Ingredients.Null)
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
        if (!isSource)
        {
            button.GetComponent<Image>().sprite = null;
            itemInSlot = AlchemyEnums.Ingredients.Null;
            itemImage = null;
        }
    }

    public void SetItem(AlchemyEnums.Ingredients item, Sprite image)
    {
        itemInSlot = item;
        itemImage = image;
        button.GetComponent<Image>().sprite = image;
    }

    public void setTestItem()
    {
        itemInSlot = AlchemyEnums.Ingredients.Salt;
    }
}
