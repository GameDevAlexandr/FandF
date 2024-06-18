using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;
public class InventoryAmmo : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _count;
    [SerializeField] private ForgeItemType _type;
    [SerializeField] private int _level;
    private void Awake()
    {
        _icon.sprite = ForgeItemBase.Base[_type][_level].icon;
        ChangeData();
        EventManager.ChangrForgeitem.AddListener((ForgeItemType t, int ar1, int ar2) => ChangeData());
    }
    public void ChangeData()
    {        
        _count.text = forgeItems[(int)_type].items[_level].ToString();
    }
}
