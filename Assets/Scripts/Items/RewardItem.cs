using NaughtyAttributes;
using UnityEngine;
using static EnumsData;

[CreateAssetMenu(fileName = "NewReward", menuName ="Rewards",order =17)]
public class RewardItem : ScriptableObject
{
    [HideIf("_noCurrency")] [SerializeField] CurrencyItem _currency;
    [HideIf("_noForge")] [SerializeField] ForgeItem _forgeItem;
    [HideIf("_noPotion")] [SerializeField] PotionItem _potion;
    [HideIf("_noSoul")] [SerializeField] SoulItem _soul;

    private bool _noCurrency => _forgeItem != null || _potion!=null || _soul != null;
    private bool _noForge => _currency != null || _potion!=null || _soul != null;
    private bool _noPotion => _currency != null || _forgeItem!=null || _soul != null;
    private bool _noSoul => _currency != null || _forgeItem != null || _potion != null;

    public CurrencyItem cItem => _currency;
    public ForgeItem fItem => _forgeItem;
    public PotionItem pItem => _potion;
    public SoulItem sItem => _soul;
    public StoreItemType itemType => DefineType();
    public Sprite icon => DefineIcon();

    private StoreItemType DefineType()
    {
        if (_currency != null)
        {
            return StoreItemType.currency;
        }
        else if(_forgeItem!=null)
        {
            return StoreItemType.forge;
        }
        else if(_soul!=null)
        {
            return StoreItemType.soul;
        }

        return StoreItemType.potion;
    }
    public void GetReward(int count)
    {
        switch (itemType)
        {
            case StoreItemType.currency: EventManager.AddCurrency(cItem.type, count);
                break;
            case StoreItemType.forge: EventManager.AddForgeItem(fItem.type, fItem.level, count);
                break;
            case StoreItemType.soul: EventManager.AddSoul(sItem.index, count);
                break;
            default: EventManager.AddPotion(pItem.type, pItem.level, count);
                break;
        }
    }
    private Sprite DefineIcon()
    {
        switch (itemType)
        {
            case StoreItemType.currency: return cItem.icon;
            case StoreItemType.forge: return fItem.icon;
            case StoreItemType.soul: return sItem.icon;
            default: return pItem.icon;
        }
    }
}
