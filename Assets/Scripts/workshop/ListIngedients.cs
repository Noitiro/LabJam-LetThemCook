using System.Collections.Generic;
using UnityEngine;

public class ListIngedients : MonoBehaviour {
    [SerializeField] private List<GameObject> allIngridientsPrefab;

    public GameObject test(AlchemyEnums.Ingredients? nameIngredients) {
        switch (nameIngredients) {
            case AlchemyEnums.Ingredients.SublimatedSulphur:
                return allIngridientsPrefab[0];
            default:
                return allIngridientsPrefab[1];
        }
    }
}
