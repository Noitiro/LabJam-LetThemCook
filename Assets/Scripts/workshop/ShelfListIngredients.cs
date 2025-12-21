using System.Collections.Generic;
using UnityEngine;

public class ShelfListIngredients : MonoBehaviour
{
    public List<AlchemyEnums.Ingredients?> shelfIngredientsList = new List<AlchemyEnums.Ingredients?>();
    public List<AlchemyEnums.Ingredients> shelfList = new List<AlchemyEnums.Ingredients>();

    [SerializeField] ListIngedients listIngedients;

    [SerializeField] GameObject shelfSlot;
    [SerializeField] GameObject shelfSlot2;
    [SerializeField] GameObject shelfSlot3;


    public void showItem() {
        if (shelfIngredientsList != null) { 
            Instantiate(listIngedients.test(shelfIngredientsList[0]), shelfSlot.transform);
            shelfList.Add((AlchemyEnums.Ingredients)shelfIngredientsList[0]);
        }
    }
}
