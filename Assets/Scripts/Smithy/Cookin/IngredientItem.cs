using UnityEngine;
using UnityEngine.UI;
using static GeneralData;


public class IngredientItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _count;
    [SerializeField] private GameObject _notEnough;

    private int _cnt;
    private CurrencyItem _crc;
    private ForgeItem _frg;
    public void SetData(CurrencyItem item, int count)
    {
        _cnt = count;
        _crc = item;
        int storeCount = currency[(int)item.type];
        SetCountUI(storeCount, _cnt);
        _icon.sprite = item.icon;
    }
    public void SetData(ForgeItem item, int count)
    {
        _cnt = count;
        _frg = item;
        int storeCount = forgeItems[(int)item.type].items[item.level];
        SetCountUI(storeCount, _cnt);
        _icon.sprite = item.icon;
    }
    public void SetEnough(bool enough)
    {
        _notEnough.SetActive(!enough);
        if (_crc)
        {
            int storeCount = currency[(int)_crc.type];
            SetCountUI(storeCount, _cnt);
        }
        else
        {
            int storeCount = forgeItems[(int)_frg.type].items[4];
            SetCountUI(storeCount, _cnt);
        }
    }
    private void SetCountUI(int store, int count)
    {
        _count.text = store + "/" + count;
    }
}
