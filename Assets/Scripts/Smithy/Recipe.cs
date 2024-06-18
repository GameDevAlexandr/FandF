using UnityEngine;
using static EnumsData;

[CreateAssetMenu(fileName = "NewRecipe", menuName ="Smithy/Recipe", order = 12)]
public class Recipe : ScriptableObject
{
    [SerializeField] private CurrencyType _result;
    [SerializeField] private int _hardness;
    [SerializeField] private Ingredient[] _ingredients;

    public CurrencyType result { get { return _result; } }
    public int hardness { get { return _hardness; } }
    public Ingredient[] ingredients { get { return _ingredients; } }

    [System.Serializable]
    public struct Ingredient
    {
        public CurrencyType item;
        public int count;
    }

}
