using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class AddingIngredients : MonoBehaviour {
    [SerializeField] SelectedIngredients selectedIngredients;
    [SerializeField] RecipeSearcher recipeSearcher;

    [SerializeField] GameObject prefabSalt;
    [SerializeField] GameObject prefabMercury;
    [SerializeField] GameObject prefabSulphur;

    [SerializeField] GameObject itemSlot;
    [SerializeField] GameObject itemSlot1;
    [SerializeField] GameObject itemSlot3;

    public List<AlchemyEnums.Ingredients> ingredientsList = new List<AlchemyEnums.Ingredients>();

    bool itemSlotEmpty = true;
    bool itemSlotEmpty1 = true;
    bool itemSlotEmpty2 = true;

    public void putIngredients () {
        switch (selectedIngredients.selectIng) {
            case AlchemyEnums.Ingredients.Salt:
                add(prefabSalt, AlchemyEnums.Ingredients.Salt);
                Debug.Log("Put salt");
                break;
            case AlchemyEnums.Ingredients.Mercury:
                add(prefabMercury, AlchemyEnums.Ingredients.Mercury);
                Debug.Log("Put mercury");
                break;
            case AlchemyEnums.Ingredients.Sulphur:
                add(prefabSulphur, AlchemyEnums.Ingredients.Sulphur);
                Debug.Log("Put sulphur");
                break;
        }
    }

    private void add(GameObject prefab, AlchemyEnums.Ingredients typ) {
        if (itemSlotEmpty) {
            Instantiate(prefab, itemSlot.transform);
            ingredientsList.Add(typ);
            selectedIngredients.selectIng = null;
            itemSlotEmpty = false;
        } else if(itemSlotEmpty1) {
            Instantiate(prefab, itemSlot1.transform);
            ingredientsList.Add(typ);
            selectedIngredients.selectIng = null;
            itemSlotEmpty1 = false;
        } else if (itemSlotEmpty2) {
            Instantiate(prefab, itemSlot3.transform);
            ingredientsList.Add(typ);
            selectedIngredients.selectIng = null;
            itemSlotEmpty2 = false;
        }
    }

    public void create() {
        recipeSearcher.ReturnRecipe(AlchemyEnums.Instruments.Alembic, ingredientsList);
    }

    public void clearIngredients() {
        //Destroy(itemSlot.GetComponentInChildren<GameObject>());
       // Destroy(itemSlot1.GetComponentInChildren<GameObject>());
      //  Destroy(itemSlot3.GetComponentInChildren<GameObject>());

        ingredientsList.Clear();
    }
}
