using UnityEngine;
using UnityEngine.UI;
using static EnumsData;

public class MinerBagCell : MonoBehaviour
{
    public bool isFull { get; private set; }
    [SerializeField] private Image _ore;
    [SerializeField] private Text _count;
    [SerializeField] private int _capacity;

    private int _curCapa;
    private CurrencyType _curType;
    public void SetOre(CurrencyType type)
    {
        if(!CheckEmpty(type))
        {
            return;
        }
        _ore.enabled = true;
        _ore.sprite = CurrencyBase.Base[type].icon;
        _curCapa++;
        _count.text = _curCapa + "/" + _capacity;
        _curType = type;
        if (_curCapa >= _capacity)
        {
            isFull = true;
        }
    }
    public bool CheckEmpty(CurrencyType type)
    {
        if (!gameObject.activeSelf)
        {
            return false;
        }
        if (isFull || (_curCapa > 0 && type != _curType))
        {
            return false;
        }
        return true;
    }
    public void EmptyCell()
    {
        isFull = false;
        _ore.enabled = false;
        _curCapa = 0;
        _count.text = _curCapa + "/" + _capacity;
    }
    public void SetCapacity(int capacity)
    {
        _capacity = capacity;
        _count.text = _curCapa + "/" + _capacity;
    }
}
