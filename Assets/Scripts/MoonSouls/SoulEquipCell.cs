using UnityEngine;
using UnityEngine.UI;

public class SoulEquipCell : SoulCell
{
    [HideInInspector] public bool isRady; 
    
    public bool isBasic;
    [SerializeField] private GameObject _content;
    [SerializeField] private SoulsManager _manager;
    [SerializeField] private SoulInfo _info;
    [SerializeField] private NotificationItem _notificaton;
    private int _changeIndex;
    private void Awake()
    {
        _info.changeRadyEvent.AddListener(SetChangeRady);
        _info.removeRadyEvent.AddListener(RemoveChangerady);
        _info.setEnhanceEvent.AddListener(EnhanceRady);
    }
    public override void Equip(bool isEquip)
    {
        level.gameObject.SetActive(isEquip);
        isEquiped = isEquip;
        icon.enabled = isEquip;
        _content.SetActive(isEquip);
        mainButton.interactable = isEquip;
        _notificaton.SetNotification(isRady&&!isEquiped);
    }
    public void SetRady(bool isRady)
    {
        this.isRady = isRady;
        gameObject.SetActive(isRady);
        _notificaton.SetNotification(isRady && !isEquiped);
    }
    private void SetChangeRady(int changeIndex)
    {
        _changeIndex = changeIndex;
        
        mainButton.onClick.RemoveAllListeners();
        mainButton.onClick.AddListener(ChangeStatus);
        mainButton.interactable = true;        
    }

    public void RemoveChangerady()
    {
        mainButton.onClick.RemoveAllListeners();
        mainButton.onClick.AddListener(SetInfo);
        mainButton.interactable = isEquiped;
        _changeIndex = -1;
    }
    public void ChangeStatus()
    {
        if (isEquiped == false)
        {
            this.dataIndex = -1;
        }
        if (dataIndex >= 0 && dataIndex == _changeIndex)
        {
            _manager.Remove(isBasic, dataIndex);            
        }
        if (_changeIndex >= 0)
        {
            _manager.Equip(this, _changeIndex);
            _info.RemoveEquipBackground();
        }
    }
    public override void EnhanceRady(int dataIndex, bool rady)
    {
       // mainButton.interactable = rady;
    }
    public override void SetEnhanceSelectable(bool isSelectable)
    {
        return;
    }
}
