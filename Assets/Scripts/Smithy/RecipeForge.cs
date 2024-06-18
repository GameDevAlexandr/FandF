using NaughtyAttributes;
using UnityEngine;
using static EnumsData;

[CreateAssetMenu(fileName = "NewForgeRecipe", menuName ="Smithy/ForgeRecipe", order = 13)]
public class RecipeForge : ScriptableObject
{
    [SerializeField] private ForgeItemType _result;
    [SerializeField] private int _hardness;
    [SerializeField] private Ingredient[] _ingredients;

    public ForgeItemType result { get { return _result; } }
    public int hardness { get { return _hardness; } }
    public Ingredient[] ingredients { get { return _ingredients; } }

    [System.Serializable]
    public struct Ingredient
    {
        public bool isCurrency;        
        [HideIf("isCurrency")][AllowNesting] public ForgeItemType fItem;
        [ShowIf("isCurrency")][AllowNesting] public CurrencyType item;
        public int count;
    }

}
