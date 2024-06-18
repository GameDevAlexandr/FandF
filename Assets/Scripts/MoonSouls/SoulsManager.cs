using System.Collections.Generic;
using UnityEngine;
using static GeneralData;
using static EnumsData;

public class SoulsManager : MonoBehaviour
{
    [SerializeField] private SoulCell _cell;
    [SerializeField] private Transform _content;
    [SerializeField] private SoulInfo _info;
    [SerializeField] private SoulEquipCell[] _equipCells;
    [SerializeField] private SoulEquipCell[] _spareCells;

    private List<SoulCell> _cells = new List<SoulCell>();
    private void Awake()
    {
        EventManager.ChangeSouls.AddListener(AddItem);
        EventManager.EquipAmmo.AddListener(SetQuipCellData);
        if (souls.Count > 0)
        {
            for (int i = 0; i < souls.Count; i++)
            {
                AddCell(i);
            }
        }
        SetQuipCellData(AmmoType.amulet);
        for (int i = 0; i < sonData.figthSouls.Length; i++)
        {
            if (sonData.figthSouls[i] >= 0)
            {
                Equip(_equipCells[i], sonData.figthSouls[i]);
                _cells[sonData.figthSouls[i]].Equip(true);
            }
        }
        for (int i = 0; i < sonData.spareSouls.Length; i++)
        {
            if (sonData.spareSouls[i] >= 0)
            {
                Equip(_spareCells[i], sonData.spareSouls[i]);
                _cells[sonData.spareSouls[i]].Equip(true);
            }

        }
    }

    public void AddItem()
    {
        AddCell(souls.Count - 1);
    }
    private void AddCell(int dataIndex)
    {
        SoulCell cell = Instantiate(_cell, _content);
        cell.SetData(dataIndex, _info);
        _cells.Add(cell);
    }

    public void Equip(SoulEquipCell cell, int dataIndex)
    {
        if (cell.dataIndex != -1)
        {
            _cells[cell.dataIndex].Equip(false);
        }
        if (cell.isBasic)
        {
            for (int i = 0; i < _equipCells.Length; i++)
            {
                if(_equipCells[i] == cell)
                {                    
                    cell.SetData(dataIndex, _info);
                    cell.Equip(true);
                    sonData.figthSouls[i] = dataIndex;
                    EventManager.EquipSoul.Invoke(dataIndex, true);
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < _spareCells.Length; i++)
            {
                if (_spareCells[i] == cell)
                {
                    cell.SetData(dataIndex, _info);
                    cell.Equip(true);
                    sonData.spareSouls[i] = dataIndex;
                    EventManager.EquipSoul.Invoke(dataIndex, true);
                    break;
                }
            }
        }
        _cells[dataIndex].Equip(true);
    }
    public void Remove(bool isBasic, int dataIndex)
    {
        if (isBasic)
        {
            for (int i = 0; i < _equipCells.Length; i++)
            {
                if (_equipCells[i].isRady && _equipCells[i].dataIndex == dataIndex)
                {
                    _equipCells[i].Equip(false);
                    sonData.figthSouls[i] = -1;
                    EventManager.EquipSoul.Invoke(dataIndex, false);
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < _spareCells.Length; i++)
            {
                if (_spareCells[i].isRady && _spareCells[i].dataIndex == dataIndex)
                {
                    _spareCells[i].Equip(false);
                    sonData.spareSouls[i] = -1;
                    EventManager.EquipSoul.Invoke(dataIndex, false);
                    break;
                }
            }
        }
        _cells[dataIndex].Equip(false);
    }
    public void RemoveItem(SoulCell cell)
    {
        int dataIndex = cell.dataIndex;
        var sword = souls[cell.dataIndex].sword;
        var arm = souls[cell.dataIndex].armour;
        var aml = souls[cell.dataIndex].amulet;
       EventManager.AddForgeItem((ForgeItemType)sword.type, sword.index, 1);
       EventManager.AddForgeItem((ForgeItemType)arm.type, arm.index, 1);
       EventManager.AddForgeItem((ForgeItemType)aml.type, aml.index, 1);
        souls.RemoveAt(cell.dataIndex);
        Remove(true, dataIndex);
        Remove(false, dataIndex);
        _cells.Remove(cell);
        cell.Destroy();
        
        for (int i = 0; i < souls.Count; i++)
        {            
            _cells[i].SetData(i,_info);
        }        
        
        for (int i = 0; i < sonData.figthSouls.Length; i++)
        {
            if (sonData.figthSouls[i] > dataIndex)
            {
                sonData.figthSouls[i]--;
                _equipCells[i].SetData(sonData.figthSouls[i], _info);
            }
        }
        for (int i = 0; i < sonData.spareSouls.Length; i++)
        {
            if (sonData.spareSouls[i] > dataIndex)
            {
                sonData.spareSouls[i]--;
                _spareCells[i].SetData(sonData.spareSouls[i], _info);
            }
        }
    }

    private void SetQuipCellData(AmmoType aType)
    {
        if (aType == AmmoType.amulet)
        {
            for (int i = 0; i < _equipCells.Length; i++)
            {
                _equipCells[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < _spareCells.Length; i++)
            {
                _spareCells[i].gameObject.SetActive(false);
            }

            int eqIdx = sonData.eqipment[(int)AmmoType.amulet].index;
            if (eqIdx >= 0)
            {
                ForgeItemType type = (ForgeItemType) sonData.eqipment[(int)AmmoType.amulet].type;
                ForgeItem item = ForgeItemBase.Base[type][eqIdx];
                for (int i = 0; i < _equipCells.Length; i++)
                {
                    _equipCells[i].SetRady(i < item.fightPocket);
                }
                for (int i = 0; i < _spareCells.Length; i++)
                {
                    _spareCells[i].SetRady(i < item.spare);
                }
            }
            else
            {
                for (int i = 0; i < _equipCells.Length; i++)
                {
                    _equipCells[i].Equip(false);
                    sonData.figthSouls[i] = -1;
                }
                for (int i = 0; i < _spareCells.Length; i++)
                {
                    _spareCells[i].Equip(false);
                    sonData.spareSouls[i] = -1;
                }
                for (int i = 0; i < _cells.Count; i++)
                {
                    _cells[i].Equip(false);
                }
            }
        }
    }
    public void SelectCellForTutorial()
    {
        _cells[1].SelectForEnhance();
    }
}
