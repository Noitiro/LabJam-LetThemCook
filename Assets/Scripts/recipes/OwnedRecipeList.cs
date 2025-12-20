using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class OwnedRecipeList : MonoBehaviour
{
    public RecipeSearcher Searcher;
    public TextMeshProUGUI TextField;

    public RecipeSO BaseRecipe;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddRecipe(BaseRecipe);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddRecipe(RecipeSO NewRecipe)
    {
        TextMeshProUGUI Recipe = Instantiate(TextField, this.transform);

        string WriteRecipe = NewRecipe.recipeName + " = ";

        for (int i = 0; i < NewRecipe.Ingredients.Count; i++)
        {
            AlchemyEnums.Ingredients Ingredient = NewRecipe.Ingredients[i];

            if(Ingredient == AlchemyEnums.Ingredients.Salt ||
                Ingredient == AlchemyEnums.Ingredients.Mercury ||
                Ingredient == AlchemyEnums.Ingredients.Sulphur)
            {
                WriteRecipe += Ingredient.ToString();
            }
            else
            {
                RecipeSO GetRecipe = Searcher.ReturnIngredientRecipe(Ingredient);

                WriteRecipe += GetRecipe.recipeName;
            }

            if (i != NewRecipe.Ingredients.Count - 1)
            {
                WriteRecipe += " + ";
            }
        }

        WriteRecipe += " (" + NewRecipe.Instrument + ")";

        Recipe.text = WriteRecipe;
    }
}
