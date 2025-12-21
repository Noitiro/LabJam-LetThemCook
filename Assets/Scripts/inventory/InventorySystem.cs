using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    AlchemyEnums.Ingredients? pickedItem = null;
    Sprite pickedItemImage = null;
    ItemSlot startingSlot = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Próbuj podnieœæ przedmiot. Jak masz jakiœ podniesiony ju¿ albo slot jest pusty to zwróæ false.
    public bool pickItem(ItemSlot clickedSlot)
    {
        if (pickedItem == null && clickedSlot.itemInSlot != null)
        {
            pickedItem = clickedSlot.itemInSlot;
            pickedItemImage = clickedSlot.itemImage;
            startingSlot = clickedSlot;
            return true;
        }

        return false;
    }

    public bool dropItem(ItemSlot clickedSlot)
    {
        if (pickedItem != null && clickedSlot.itemInSlot == null)
        {
            clickedSlot.itemInSlot = pickedItem;
            clickedSlot.itemImage = pickedItemImage;
            clickedSlot.button.GetComponent<Image>().sprite = pickedItemImage;

            pickedItem = null;
            pickedItemImage = null;

            startingSlot.ClearSlot();
            startingSlot = null;

            return true;
        }

        return false;
    }
}
