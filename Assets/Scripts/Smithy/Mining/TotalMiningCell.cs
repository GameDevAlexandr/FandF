using UnityEngine;
using UnityEngine.UI;

public class TotalMiningCell : MonoBehaviour
{
    [SerializeField] private EnumsData.CurrencyType _type;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _count;

    private int _counter; 
    private void Awake()
    {
        _icon.sprite = CurrencyBase.Base[_type].icon;
    }
    public void AddItem(EnumsData.CurrencyType type)
    {
        if (type != _type)
        {
            return;
        }
            _counter++;
            _count.text = _counter.ToString();
            gameObject.SetActive(true);
        
    }
    public void Clear()
    {
        _counter = 0;
        _count.text = _counter.ToString();
    }
}
