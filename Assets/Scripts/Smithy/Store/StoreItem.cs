using NaughtyAttributes;
using UnityEngine;
using static EnumsData;

[CreateAssetMenu(fileName = "NewItem", menuName ="StoreItem", order = 15)]
public class StoreItem : ScriptableObject
{
    [SerializeField] private StoreItemType _type;
    [ShowIf("isCurrency")][SerializeField] private CurrencyItem _item;
    [ShowIf("isSingle")][SerializeField] private SingleItem _singleItem;
    [ShowIf("isPotion")][SerializeField] private PotionItem _potionItem;
    [ShowIf("isSingle")][SerializeField] StoreItem _nextItem;
    [SerializeField] private CurrencyType _currency;
    [SerializeField] private int _price;

    private bool isCurrency => _type == StoreItemType.currency;
    private bool isSingle => _type == StoreItemType.single;
    private bool isPotion => _type == StoreItemType.potion; 
    public StoreItemType type => _type;
    public Sprite icon => GetData(_type).icon;
    public string itemName => GetData(_type).itemName;
    public string description => GetData(_type).description;
    public Sprite crcIcon => CurrencyBase.Base[_currency].icon;
    public SingleItem singleItem => _singleItem;
    public CurrencyItem item => _item;
    public PotionItem potionItem => _potionItem;
    public StoreItem nextItem => _nextItem;
    public CurrencyType currency => _currency;
    public int price => _price;
    public int level => GetData(_type).level;

    private struct ItemData
    {
        public Sprite icon;
        public string itemName;
        public string description;
        public int level;
    }
    private ItemData GetData(StoreItemType type)
    {
        var dat = new ItemData();
        switch (type)
        {
            case StoreItemType.currency:
                dat.itemName = _item.itemName;
                dat.description = _item.description;
                dat.icon = _item.icon;
                dat.level = 0;
                return dat;
            case StoreItemType.potion:
                dat.itemName = _potionItem.itemName;
                dat.description = _potionItem.description;
                dat.icon = _potionItem.icon;
                dat.level = _potionItem.level;
                return dat;
            default:
                dat.itemName = _singleItem.itemName;
                dat.description = _singleItem.description;
                dat.icon = _singleItem.icon;
                dat.level = _singleItem.level;
                return dat;
        }
    }

}
