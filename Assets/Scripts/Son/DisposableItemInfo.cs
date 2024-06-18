using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

public class DisposableItemInfo : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _frame;
    [SerializeField] private Image _back;
    [SerializeField] private Text _name;
    [SerializeField] private Text _description;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Text _buttonText;
    [SerializeField] private UseDisposableManager _useManager;

    private PotionItem _item;
    private PotionItem _equipItem;
    private bool _equped;
    private DisposableEqipCell _cell;
    public void SetData(PotionItem item)
    {
        _equped = _cell.IsFull(item);
        _equipButton.onClick.RemoveAllListeners();
        _equipButton.onClick.AddListener(() => Equip(_equped));
        gameObject.SetActive(true);
        _item = item;
        _icon.sprite = _item.icon;
        _back.sprite = _item.icon;
        _frame.sprite = RarityBase.frames[_item.level];
        _name.text = _item.itemName;
        _description.text = item.description;
        _equipButton.gameObject.SetActive(true);
       // bool isEmpty = potionItems[(int)item.type].items[item.level]>0
        _buttonText.text = _equped ? "REMOVE" : "EQIP";
    }

    public void SetCell(PotionItem item, DisposableEqipCell cell)
    {
        if(_cell != null)
        {
            _cell.SetNormalSize();
        }
        _cell = cell;
        _item = item;
        if (_cell.isEquped) 
        { 
            SetData(item); 
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    private void Equip(bool isEquip)
    {
        if (!isEquip)
        {
            _cell.Equip(_item);
            _equipItem = _item;
        }
        else
        {
            _equipItem = null;
            _cell.Remove();
        }
        gameObject.SetActive(false);
        _useManager.SetNewPocketCells();
    }
}
