using System.Collections.Generic;
using UnityEngine;
using static EnumsData;

public class PotionItemsBase : MonoBehaviour
{
    public static Dictionary<PotionType, Dictionary<int, PotionItem>> Base { get { Initialize(); return _base; } }

    private static Dictionary<PotionType, Dictionary<int, PotionItem>> _base;

    private static void Initialize()
    {
        if (_base == null)
        {
            _base = new Dictionary<PotionType, Dictionary<int, PotionItem>>();
            PotionItem[] crc = Resources.LoadAll<PotionItem>("PotionItems");
            for (int i = 0; i < crc.Length; i++)
            {
                Dictionary<int, PotionItem> newItem = new Dictionary<int, PotionItem>();
                newItem.Add(crc[i].level, crc[i]);
                if (!_base.ContainsKey(crc[i].type))
                {
                    _base.Add(crc[i].type, newItem);
                }
                else
                {
                    _base[crc[i].type].Add(crc[i].level, crc[i]);
                }
            }
        }
    }
}
