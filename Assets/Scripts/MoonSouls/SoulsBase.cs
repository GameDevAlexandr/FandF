using System.Collections.Generic;
using UnityEngine;

public class SoulsBase : MonoBehaviour
{
    public static Dictionary<int, List<SoulItem>> Base { get { InitBase(); return _base; } }
    public static Dictionary<int,  SoulItem> IndexBase { get { InitBase(); return _indexBase; } }
    
    private static Dictionary<int, SoulItem> _indexBase;
    private static Dictionary<int, List<SoulItem>> _base;

    public static void InitBase()
    {
        if (_base == null)
        {
            SoulItem[] items = Resources.LoadAll<SoulItem>("Souls");

            _base = new Dictionary<int, List<SoulItem>>();
            _indexBase = new Dictionary<int, SoulItem>();
            for (int i = 0; i < items.Length; i++)
            {
                _indexBase.Add(items[i].index, items[i]);
                
                if (!_base.ContainsKey(items[i].rare))
                {
                    _base.Add(items[i].rare, new List<SoulItem>());
                }
                _base[items[i].rare].Add(items[i]);
            }
        }
    }

}
