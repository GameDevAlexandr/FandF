using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;
public class InventoryItem : MonoBehaviour
{
    public CurrencyItem item => CurrencyBase.Base[_item];

    [SerializeField] private CurrencyType _item;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _count;

    private void Awake()
    {
        Init();
    }
    
    public virtual void ChangeCount(CurrencyType item, int count)
    {
        _count.text = currency[(int)_item].ToString();
    }

    public virtual void Init()
    {
        _icon.sprite = item.icon;
        _count.text = currency[(int)_item].ToString();
        EventManager.ChangeCurrency.AddListener(ChangeCount);
        ChangeCount(_item, 0);
    }
}
