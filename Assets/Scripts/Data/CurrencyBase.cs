using System.Collections.Generic;
using UnityEngine;

public class CurrencyBase : MonoBehaviour
{
    public static Dictionary<EnumsData.CurrencyType, CurrencyItem> Base { get { Initialize(); return _base; } } 
    private static Dictionary<EnumsData.CurrencyType, CurrencyItem> _base;

    private static void Initialize()
    {
        if (_base == null)
        {
            _base = new Dictionary<EnumsData.CurrencyType, CurrencyItem>();
            CurrencyItem[] crc = Resources.LoadAll<CurrencyItem>("Currency");
            for (int i = 0; i < crc.Length; i++)
            {
                _base.Add(crc[i].type, crc[i]);
            }
        } 
    }
}
