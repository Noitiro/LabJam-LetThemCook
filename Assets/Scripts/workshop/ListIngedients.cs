using System.Collections.Generic;
using UnityEngine;

public class ListIngedients : MonoBehaviour {
    [SerializeField] private List<GameObject> allIngridientsPrefab;

    public GameObject test(AlchemyEnums.Ingredients? nameIngredients) {
        for (int i = 0; i < allIngridientsPrefab.Count; i++) {
            if(allIngridientsPrefab[i].name.Equals(nameIngredients.Value.ToString())) {
                return allIngridientsPrefab[i];
            }
        }
        return null;
    }
}
