using UnityEngine;
using UnityEngine.UI;

public class SonBagCell : MonoBehaviour
{
    [HideInInspector]public EnumsData.CurrencyType type;
    public bool isfull { get; private set; }

    [SerializeField] private Image _icon;
    [SerializeField] private Text _countText;

    int _count;
    public void SetData(CurrencyItem item, int count)
    {
        _icon.enabled = true;
        isfull = true;
        type = item.type;
        _icon.sprite = item.icon;
        _count += count;
        _countText.text = _count.ToString(); 
    }
    public void SetEmpty()
    {
        isfull = false;
        _icon.enabled = false;
        _countText.text = "";
        _count = 0;
    }
}
