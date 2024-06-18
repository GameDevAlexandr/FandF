using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;

public class StoreCell : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _currencyIcon;
    [SerializeField] private Text _priceText;
    [SerializeField] private Text _countText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private StoreItem _item;
    [SerializeField] private int _count = 1;
    [SerializeField] private Button _infoButton;
    [SerializeField] private StoreItemInfo _info;

    private StoreItemType type => _item.type;
    private int _price;
    private int _level;
    private CurrencyType _currency;
    private int _storageCount;

    private void Start()
    {
        _buyButton.onClick.AddListener(BuyItem);
        EventManager.ChangeCurrency.AddListener((CurrencyType t, int c) => SetInteractable(t));
        _infoButton.onClick.AddListener(GetInfo);
        EventManager.ChangeSingleItem.AddListener(ChangeItemListner);
        Init();
    }

    private void ChangeItemListner(SingleItemType sType)
    {
        if( type == StoreItemType.single && sType == _item.singleItem.type)
        {
            Init();
        }
    }
    private void Init()
    {
        ChangeItem();
        _level = _item.level;
        _countText.text = _count > 1 ? "x" + _count : "";
        _nameText.text = _item.itemName;
        _descriptionText.text = _item.description;
        _currencyIcon.sprite = _item.crcIcon;
        _currency = _item.currency;
        _price = _item.price;
        _priceText.text = _price.ToString();
        _icon.sprite = _item.icon;             
        SetInteractable(_currency);
    }

    private void ChangeItem()
    {
        if (type== StoreItemType.single)
        {
            if (_item.level <= singleItems[(int)_item.singleItem.type])
            {
                if (_item.nextItem)
                {
                    _item = _item.nextItem;
                    ChangeItem();
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
    private void SetInteractable(CurrencyType type)
    {
        if(type == _currency)
        {
            _buyButton.interactable = currency[(int)type] >= _price;
        }
    }

    private void BuyItem()
    {
        EventManager.AddCurrency(_currency, -_price);

        switch (type)
        {
            case StoreItemType.currency: EventManager.AddCurrency(_item.item.type,_count);
                break;
            case StoreItemType.single: EventManager.AddSingleItem(_item.singleItem.type, _level);
                break;
            case StoreItemType.potion: EventManager.AddPotion(_item.potionItem.type, _level,_count);
                break;

        }
        GetInfo();
        Sounds.chooseSound.buyAtCoins.Play();
        GameAnalitic.BuyProduct(_item.itemName, _item.currency.ToString());
    }
    private void GetInfo()
    {
        _info.SetData(_item.description, GetStoreCount(), _item.icon);        
    }
    private int GetStoreCount()
    {
        switch (type)
        {
            case StoreItemType.currency:
                return currency[(int)_item.item.type];
            case StoreItemType.single:
                return -1;
            default:
                return potionItems[(int) _item.potionItem.type].items[_level];
        }
    }
}
