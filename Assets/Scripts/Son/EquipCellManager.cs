using UnityEngine;
using static EnumsData;
using static GeneralData;
public class EquipCellManager : MonoBehaviour
{
    [SerializeField] private EquipAmmoCell[] _ammoCells;
    [SerializeField] private GameObject _InventPanel;
    [SerializeField] private GameObject _ammoInventory;
    [SerializeField] private GameObject _disposableInventory;
    [SerializeField] private AmmoEquipedCell[] _equpments;
    [SerializeField] private SonInfo _sonInfo;

    private AmmoEquipedCell _curCell;
    private bool _isOpen;
    private void Awake()
    {
        for (int i = 0; i < _equpments.Length; i++)
        {
            _equpments[i].Init();
        }
        EventManager.ChangrForgeitem.AddListener((ForgeItemType t, int l, int c) => SetChanges());       
    }
    public void GetAmmoInventory(AmmoEquipedCell cell)
    {
        _curCell = cell;
        var type = cell.type;        
        for (int i = 0; i < _ammoCells.Length; i++)
        {
            _ammoCells[i].isActive = false;
        }
        int cellsCounter = 0;
        foreach (var key1 in ForgeItemBase.Base)
        {            
            var fType = key1.Key;
            if (ForgeItemBase.Amunition[type].Contains(fType))
            {
                foreach (var key2 in ForgeItemBase.Base[fType])
                {
                    if (_ammoCells.Length > cellsCounter)
                    {
                        _ammoCells[cellsCounter].isActive = true;
                        _ammoCells[cellsCounter].SetData(fType, key2.Key,cell);
                    }
                        cellsCounter++;                    
                }
            }
        }
        _isOpen = true;
        _ammoInventory.SetActive(true);
        if (_disposableInventory)
        {
            _disposableInventory.SetActive(false);
            _InventPanel.gameObject.SetActive(true);
        }
    }

    public void ActivateDespInvent()
    {
        _InventPanel.gameObject.SetActive(true);
        _ammoInventory.SetActive(false);
        _disposableInventory.SetActive(true);
    }
    private void SetChanges()
    {
        if (_isOpen)
        {
            GetAmmoInventory(_curCell);
        }
    }
    public virtual void SetSonData(AmmoType type, ForgeItem item, int index)
    {
        fItemData fItem = new fItemData();
        fItem.type = (int)item.type;
        fItem.index = index;
        sonData.eqipment[(int)type] = fItem;
        _sonInfo.SetData();
        EventManager.EquipAmmo.Invoke(type);
    }
    public virtual ForgeItem GetSonData(AmmoType type)
    {
        fItemData fItem = sonData.eqipment[(int)type];
        return fItem.index != -1 ? ForgeItemBase.Base[(ForgeItemType)fItem.type][fItem.index] : null;
    }
}
