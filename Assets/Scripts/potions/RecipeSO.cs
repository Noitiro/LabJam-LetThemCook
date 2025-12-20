using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Alchemy/Recipe Card")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public Sprite icon;
    // Tu mo¿na color mikkstury niue ikone w sumie

    public List<AlchemyEnums.Ingredients> ingredients;

    public List<AlchemyEnums.Instruments> Instruments;

    public float cookTime = 2.0f;
}
