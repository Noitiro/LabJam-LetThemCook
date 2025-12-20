using UnityEngine;

public class SelectedIngredients : MonoBehaviour
{
    public AlchemyEnums.Ingredients? selectIng = null;

    public void selectedIngredients (string nameIngredients) {
        switch (nameIngredients) {
            case "salt":
                selectIng = AlchemyEnums.Ingredients.Salt;
                Debug.Log("Select salt");
                break;
            case "mercury":
                selectIng = AlchemyEnums.Ingredients.Mercury;
                Debug.Log("Select mercury");
                break;
            case "sulphur":
                selectIng = AlchemyEnums.Ingredients.Sulphur;
                Debug.Log("Select sulphur");
                break;
        }
    }
}
