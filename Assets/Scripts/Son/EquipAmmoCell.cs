
using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;

[RequireComponent(typeof(Button))]
public class EquipAmmoCell : MonoBehaviour
{
    [HideInInspector] public bool isActive;

    [SerializeField] private Image _icon;
    [SerializeField] private Image _back;
    [SerializeField] private Image _frame;
    [SerializeField] private Text _count;
    [SerializeField] private ForgeItemInfo _info;

    private int _index;
    private ForgeItemType _forgeItem;
    private ForgeItem _item;
    private AmmoEquipedCell _cell;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(GetInfo);
    }
    public void SetData(ForgeItemType type, int index, AmmoEquipedCell cell)
    {
        _cell = cell;
        _forgeItem = type;
        _item = ForgeItemBase.Base[_forgeItem][index];
        _icon.sprite =_item.icon;
        _frame.sprite = RarityBase.frames[_item.level];
        _back.sprite = RarityBase.backs[_item.material];
        _index = index;
        ChangeCount(_forgeItem, index);
    }

    private void ChangeCount(ForgeItemType type, int index)
    {
        if(isActive && type==_forgeItem && index == _index)
        {
            int count = forgeItems[(int)type].items[index];
            gameObject.SetActive(count > 0);
            _count.text = forgeItems[(int)type].items[index].ToString();
        }
    }
    private void GetInfo()
    {
        _info.gameObject.SetActive(true);
        _info.SetData(_item,_cell, !_cell.CheckEquiped(_item));
    }
}
