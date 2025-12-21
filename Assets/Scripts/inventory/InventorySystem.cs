using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    AlchemyEnums.Ingredients pickedItem = AlchemyEnums.Ingredients.Null;
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
        Debug.Log(pickedItem.ToString());
        Debug.Log(clickedSlot.ToString());

        if (pickedItem == AlchemyEnums.Ingredients.Null && clickedSlot.itemInSlot != AlchemyEnums.Ingredients.Null)
        {
            pickedItem = clickedSlot.itemInSlot;
            pickedItemImage = clickedSlot.itemImage;
            startingSlot = clickedSlot;

            Debug.Log("Picked item");

            return true;
        }

        return false;
    }

    public bool dropItem(ItemSlot clickedSlot)
    {

        if (pickedItem != AlchemyEnums.Ingredients.Null && clickedSlot.itemInSlot == AlchemyEnums.Ingredients.Null && !clickedSlot.isOutput)
        {
            clickedSlot.itemInSlot = pickedItem;
            clickedSlot.itemImage = pickedItemImage;
            clickedSlot.button.GetComponent<Image>().sprite = pickedItemImage;

            pickedItem = AlchemyEnums.Ingredients.Null;
            pickedItemImage = null;

            startingSlot.ClearSlot();
            startingSlot = null;

            return true;
        }

        return false;
    }
}
