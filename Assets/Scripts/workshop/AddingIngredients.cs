using UnityEngine;
public class AddingIngredients : MonoBehaviour {
    [SerializeField] SelectedIngredients selectedIngredients;
    [SerializeField] GameObject prefabSalt;
    [SerializeField] GameObject prefabMercury;
    [SerializeField] GameObject prefabSulphur;


    [SerializeField] GameObject itemSlot;
    [SerializeField] GameObject itemSlot1;
    [SerializeField] GameObject itemSlot3;

    public void putIngredients () {
        switch (selectedIngredients.selectIng) {
            case AlchemyEnums.Ingredients.Salt:
                Instantiate(prefabSalt, itemSlot.transform);
                Instantiate(prefabSalt, itemSlot1.transform);
                Instantiate(prefabSalt, itemSlot3.transform);
                Debug.Log("Put salt");
                break;
            case AlchemyEnums.Ingredients.Mercury:
                Instantiate(prefabMercury, itemSlot.transform);
                Debug.Log("Put mercury");
                break;
            case AlchemyEnums.Ingredients.Sulphur:
                Instantiate(prefabSulphur, itemSlot.transform);
                Debug.Log("Put sulphur");
                break;
        }
    }
}
