using UnityEngine;
using static GeneralData;

public class EquipSoulAmmoManager : EquipCellManager
{
    [SerializeField] private SoulInfo _info;
    private int  _dataIndex =>_info.dataIndex;
    public override void SetSonData(EnumsData.AmmoType type, ForgeItem item, int index)
    {
        SoulsData data = souls[_dataIndex];
        if(type == EnumsData.AmmoType.sword)
        {
            data.sword.type = (int)item.type;
            data.sword.index = index;
        }
        else if(type == EnumsData.AmmoType.armour)
        {
            data.armour.type = (int)item.type;
            data.armour.index = index;
        }
        else
        {
            data.amulet.type = (int)item.type;
            data.amulet.index = index;
        }
        souls[_dataIndex] = data;
        _info.ChangeAmmo();
    }
    public override ForgeItem GetSonData(EnumsData.AmmoType type)
    {
        if (souls.Count == 0)
        {
            return null;
        }
        var dat = souls[_dataIndex];
        fItemData fI;
        if(type == EnumsData.AmmoType.sword)
        {
            fI = dat.sword;
        }
        else if(type == EnumsData.AmmoType.armour)
        {
            fI = dat.armour;
        }
        else
        {
            fI = dat.amulet;
        }
        return fI.index > -1 ? ForgeItemBase.Base[(EnumsData.ForgeItemType)fI.type][fI.index] : null;
    }
}
