using System.Collections.Generic;
using UnityEngine;
using static EnumsData;

public class SingleItemsBase : MonoBehaviour
{
    public static Dictionary<SingleItemType, Dictionary<int, SingleItem>> Base { get { Initialise(); return _base; } }

    private static Dictionary<SingleItemType, Dictionary<int, SingleItem>> _base;

    private static void Initialise()
    {
        if (_base == null)
        {
            _base = new Dictionary<SingleItemType, Dictionary<int, SingleItem>>();
            var items = Resources.LoadAll<SingleItem>("SingleItems");
            for (int i = 0; i < items.Length; i++)
            {
                if (!_base.ContainsKey(items[i].type))
                {
                    _base.Add(items[i].type, new Dictionary<int, SingleItem>());
                }
                _base[items[i].type].Add(items[i].level, items[i]);
            }
        }
    }
}
