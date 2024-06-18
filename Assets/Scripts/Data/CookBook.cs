using System.Collections.Generic;
using UnityEngine;
using static EnumsData;

public class CookBook : MonoBehaviour
{
    public static Dictionary<CurrencyType, Recipe> item { get { Initialize(); return _item; } }
    public static Dictionary<ForgeItemType, RecipeForge> forgeItem { get { Initialize(); return _forgeItem; } }

    private static Dictionary<CurrencyType, Recipe> _item;
    public static Dictionary<ForgeItemType, RecipeForge> _forgeItem;

    private static void Initialize()
    {
        if(_item == null)
        {
            _item = new Dictionary<CurrencyType, Recipe>();
            Recipe[] rs = Resources.LoadAll<Recipe>("CookBook");
            for (int i = 0; i < rs.Length; i++)
            {
                _item.Add(rs[i].result, rs[i]);
            }
        }
        if (_forgeItem == null)
        {
            _forgeItem = new Dictionary<ForgeItemType, RecipeForge>();
            RecipeForge[] rs = Resources.LoadAll<RecipeForge>("CookBook");
            for (int i = 0; i < rs.Length; i++)
            {
                _forgeItem.Add(rs[i].result, rs[i]);
            }
        }
    }
}
