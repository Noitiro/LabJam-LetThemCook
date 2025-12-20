using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Alchemy/Recipe Card")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public Sprite icon;
    // Tu mo¿na color mikkstury niue ikone w sumie

    // Sk³adniki potrzebne do stworzenia substancji
    public List<AlchemyEnums.Ingredients> Ingredients;

    // Aparatura wykorzystana do stworzenia substancji
    public AlchemyEnums.Instruments Instrument;

    // Stworzona substancja
    public AlchemyEnums.Ingredients Potion;

    public float cookTime = 2.0f;
}
