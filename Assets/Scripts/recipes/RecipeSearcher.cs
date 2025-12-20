using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RecipeSearcher : MonoBehaviour
{
    public List<RecipeSO> RecipeList = new List<RecipeSO>(); 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AlchemyEnums.Ingredients? ReturnRecipe(AlchemyEnums.Instruments InstrumentType, List<AlchemyEnums.Ingredients> Ingredients)
    {
        Debug.Log("XD");
        // Mamy dwie listy: sk³adniki, na których teraz pracujemy i sk³adniki potrzebne do recepty
        // Listy te mog¹ zawieraæ te same sk³adniki, ale w ró¿nej kolejnoœci, przez co sprawdzenie czy s¹ sobie równe nie bêdzie dzia³aæ.
        // Dlatego kopiujê je do InputIngredients i RecipeIngredients, potem sortujê i porównujê.
        List<AlchemyEnums.Ingredients> InputIngredients;
        List<AlchemyEnums.Ingredients> RecipeIngredients;

        // Przeszukujemy listê recept
        foreach (var recipe in RecipeList)
        {
            // Jeœli recepta wykorzystuje t¹ sam¹ aparaturê, z której korzystamy
            if (recipe.Instrument == InstrumentType)
            {
                // Patrzymy czy sk³adniki siê zgadzaj¹
                InputIngredients = new List<AlchemyEnums.Ingredients>(Ingredients);
                RecipeIngredients = new List<AlchemyEnums.Ingredients>(recipe.Ingredients);

                InputIngredients.Sort();
                RecipeIngredients.Sort();

                if(InputIngredients.SequenceEqual(RecipeIngredients))
                {
                    Debug.Log("XDDD");
                    if (QuestManager.Instance != null)
                    {
                        QuestManager.Instance.CheckQuestCompletion(recipe);
                    }
                    Debug.Log(recipe.Potion);
                    return recipe.Potion;
                }
            }
        }

        return null;
    }
}
