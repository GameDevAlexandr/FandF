using System.Collections.Generic;
using UnityEngine;
using static EnumsData;

public class ForgeItemBase : MonoBehaviour
{
    public static Dictionary<ForgeItemType, Dictionary<int, ForgeItem>> Base { get { Initialize(); return _base; } }

    public static Dictionary<AmmoType, List<ForgeItemType>> Amunition { get { InitializeAmmo(); return _amunition; } }

    private static Dictionary<ForgeItemType, Dictionary<int, ForgeItem>> _base;
    private static Dictionary<AmmoType, List<ForgeItemType>> _amunition;

    private static void Initialize()
    {
        if (_base == null)
        {
            _base = new Dictionary<ForgeItemType, Dictionary<int, ForgeItem>>();
            ForgeItem[] crc = Resources.LoadAll<ForgeItem>("ForgeItems");
            for (int i = 0; i < crc.Length; i++)
            {
                Dictionary<int, ForgeItem> newItem = new Dictionary<int, ForgeItem>();
                newItem.Add(crc[i].level, crc[i]);
                if (!_base.ContainsKey(crc[i].type))               
                {                    
                    _base.Add(crc[i].type, newItem);
                }
                else
                {
                    _base[crc[i].type].Add(crc[i].level,crc[i]);
                }
            }
        }
    }
    private static void InitializeAmmo()
    {
        if (_amunition == null)
        {
            _amunition = new Dictionary<AmmoType, List<ForgeItemType>>();
            foreach(var itm in Base)
            {
                var fType = itm.Key;
                foreach (var key in Base[fType])
                {
                    var aType = Base[fType][key.Key].ammoType;
                    if (!_amunition.ContainsKey(aType))
                    {
                        _amunition.Add(aType, new List<ForgeItemType>());
                    }
                    _amunition[aType].Add(fType);
                }
            }
        }
    }
}
