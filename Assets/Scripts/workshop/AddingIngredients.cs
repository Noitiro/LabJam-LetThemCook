using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class AddingIngredients : MonoBehaviour {
    [SerializeField] SelectedIngredients selectedIngredients;
    [SerializeField] GameObject prefabSalt;
    [SerializeField] GameObject prefabMercury;
    [SerializeField] GameObject prefabSulphur;

    [SerializeField] GameObject itemSlot;
    [SerializeField] GameObject itemSlot1;
    [SerializeField] GameObject itemSlot3;

    GameObject ing1;
    GameObject ing2;
    GameObject ing3;

    List<AlchemyEnums.Ingredients> ingredientsList = new List<AlchemyEnums.Ingredients>();

    bool itemSlotEmpty = true;
    bool itemSlotEmpty1 = true;
    bool itemSlotEmpty2 = true;

    public void putIngredients () {
        switch (selectedIngredients.selectIng) {
            case AlchemyEnums.Ingredients.Salt:
                add(prefabSalt, AlchemyEnums.Ingredients.Salt);
                break;
            case AlchemyEnums.Ingredients.Mercury:
                add(prefabMercury, AlchemyEnums.Ingredients.Mercury);
                break;
            case AlchemyEnums.Ingredients.Sulphur:
                add(prefabSulphur, AlchemyEnums.Ingredients.Sulphur);
                break;
        }
    }

    private void add(GameObject prefab, AlchemyEnums.Ingredients typ) {
        if (itemSlotEmpty) {
            ing1 = Instantiate(prefab, itemSlot.transform);
            ingredientsList.Add(typ);
            selectedIngredients.selectIng = null;
            Debug.Log("Put salt");
            itemSlotEmpty = false;
        } else if(itemSlotEmpty1) {
            ing2 = Instantiate(prefab, itemSlot1.transform);
            ingredientsList.Add(typ);
            selectedIngredients.selectIng = null;
            Debug.Log("Put mercury");
            itemSlotEmpty1 = false;
        } else if (itemSlotEmpty2) {
            ing3 = Instantiate(prefab, itemSlot3.transform);
            ingredientsList.Add(typ);
            selectedIngredients.selectIng = null;
            Debug.Log("Put sulphur");
            itemSlotEmpty2 = false;
        }
    }

    public void clearIngredients() {
        Destroy(ing1);
        Destroy(ing2);
        Destroy(ing3);
        ingredientsList.Clear();
    }
}
