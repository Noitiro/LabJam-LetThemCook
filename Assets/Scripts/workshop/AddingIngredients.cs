using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AddingIngredients : MonoBehaviour {
    [Header("Time Cook")]
    [SerializeField] float time;

    [SerializeField] SelectedIngredients selectedIngredients;
    [SerializeField] RecipeSearcher recipeSearcher;
    [SerializeField] ListIngedients listIngedients;

    [SerializeField] AlchemyEnums.Instruments instrumentName;
    [SerializeField] AlchemyEnums.Ingredients? finalItem;

    [SerializeField] GameObject prefabSalt;
    [SerializeField] GameObject prefabMercury;
    [SerializeField] GameObject prefabSulphur;

    [SerializeField] GameObject itemSlot;
    [SerializeField] GameObject itemSlot1;
    [SerializeField] GameObject itemSlot3;

    [SerializeField] GameObject finalSlot;

    public List<AlchemyEnums.Ingredients> ingredientsList = new List<AlchemyEnums.Ingredients>();

    bool itemSlotEmpty = true;
    bool itemSlotEmpty1 = true;
    bool itemSlotEmpty2 = true;

    bool itemFinalSlotEmpty = true;

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

    private IEnumerator LetThemCook(float time) {
        yield return new WaitForSeconds(time);
        
        if (itemFinalSlotEmpty) {
            finalItem = recipeSearcher.ReturnRecipe(instrumentName, ingredientsList);
            Instantiate(listIngedients.test(finalItem), finalSlot.transform);
            itemFinalSlotEmpty = false;
            clearIngredients();
        }
    }

    public void selectItem() {
        if (itemFinalSlotEmpty == false) {
            selectedIngredients.selectIng = finalItem;
        }
    }

    public void create() {
        StartCoroutine(LetThemCook(time));
    }

    public void finishTask() {
        if (itemFinalSlotEmpty == false) {
            Destroy(finalSlot.transform.GetChild(0).gameObject);
            itemFinalSlotEmpty = true;
        }
    }

    public void clearIngredients() {
        if (itemSlotEmpty == false) {
            Destroy(itemSlot.transform.GetChild(0).gameObject);
            itemSlotEmpty = true;
        }
        if (itemSlotEmpty1 == false) {
            Destroy(itemSlot1.transform.GetChild(0).gameObject);
            itemSlotEmpty1 = true;
        }
        if (itemSlotEmpty2 == false) {
            Destroy(itemSlot3.transform.GetChild(0).gameObject);
            itemSlotEmpty2 = true;
        }

        ingredientsList.Clear();
    }

    public void ReduceCookingTime(float amount) {
        time -= amount;
        if (time < 0.5f) time = 0.5f;
    }
}
