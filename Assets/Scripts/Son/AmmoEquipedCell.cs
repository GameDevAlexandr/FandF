using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;

[RequireComponent(typeof(Button))]
public class AmmoEquipedCell : MonoBehaviour
{
    public AmmoType type; 
    [SerializeField] private Image _icon;
    [SerializeField] private Image _back;
    [SerializeField] private Image _frame;
    [SerializeField] private Sprite _empty;
    [SerializeField] private EquipCellManager _manager;
    [SerializeField] private ForgeItemInfo _infoPanel;
    [SerializeField] private NotificationItem _notification;

    private Button _button;
    private bool _isEquip;
    private ForgeItem _item;
    private int _index =-1;
    public void Init()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        ForgeItem item = _manager.GetSonData(type);
        if (item != null)
        {
            SetData(item);
            _manager.SetSonData(type, _item, _index);
        }
        if (_notification)
        {
            EventManager.ChangrForgeitem.AddListener(AddForgeItemEvent);
            SetNotification(CheckNotifiacation());
        }
        Debug.Log("initialisaton " + type);
    }
    public void OnClick()
    {
        _infoPanel.gameObject.SetActive(_isEquip);
        Debug.Log("is qeuiped Cell " + _isEquip);
        if (_isEquip)
        {
            _infoPanel.SetData(_item, this, false);
        }
        _manager.GetAmmoInventory(this);
    }

    public void Equip(ForgeItem item)
    {
        if (_index != -1)
        {
            Remove();
        }
        SetData(item);
        _manager.SetSonData( type,_item,_index);
        EventManager.AddForgeItem(item.type, item.level, -1);
        SetNotification(false);
    }

    public bool CheckEquiped(ForgeItem item)
    {
        if (!_isEquip)
        {
            return false;
        }
        return _item == item;
    }
    public void Remove()
    {
        SetData(null);
        _manager.SetSonData(type, _item, _index);
        EventManager.AddForgeItem(_item.type, _item.level, 1);
    }

    public void SetData(ForgeItem item)
    { 
        bool isEquip = item != null; 
        _item = isEquip?item: _item;
        _back.gameObject.SetActive(item != null);         
        
        if (isEquip)
        {
            _icon.sprite = _item.icon;
            _frame.sprite = RarityBase.frames[item.level];
            _back.sprite = RarityBase.backs[item.material];
        }
        _isEquip = isEquip;
        _index = isEquip? _item.level:-1;
    }
    private bool CheckNotifiacation()
    {
        for (int i = 0; i < forgeItems.Length; i++)
        {
            for (int j = 0; j < forgeItems[i].items.Length; j++)
            {
                if (forgeItems[i].items[j]>0 && ForgeItemBase.Amunition[type].Contains((ForgeItemType)i))
                {
                    return true;
                }
            }            
        }
        return false;
    }
    private void SetNotification(bool isSet)
    {
        if (_index > -1)
        {
            isSet = false;
        }
        if (_notification)
        {
            _notification.SetNotification(isSet);
        }
    }
    private void AddForgeItemEvent(ForgeItemType fType, int level, int count)
    {
        if (ForgeItemBase.Amunition[type].Contains(fType))
        {
            SetNotification(forgeItems[(int)fType].items[level] > 0);
        }
    }
}
