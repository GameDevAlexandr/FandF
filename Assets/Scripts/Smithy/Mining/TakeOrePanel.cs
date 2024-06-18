using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;

public class TakeOrePanel : MonoBehaviour
{
    [SerializeField] private Image _oreIcon;
    [SerializeField] private Button _tekeOreButton;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _countText;
    [SerializeField] private Text _radyText;
    [SerializeField] private MinerBag _bag;

    private int _currencyIdx;
    private CurrencyType _type;
    private int _oreCount;
    
    private void Start()
    {
        _tekeOreButton.onClick.AddListener(PutToBag);
        _bag.emptyEvent.AddListener(() => SetData(_type,_oreCount));
    }
    public void SetData(CurrencyType type, int count)
    {
        bool isFree = _bag.CheckEmptyCell(type);
        _countText.text = "x" + count;
        _oreCount = count;
        _type = type;
        _oreIcon.sprite = CurrencyBase.Base[type].icon;
        _tekeOreButton.interactable = isFree;
        _currencyIdx = (int)type;
        _radyText.text = isFree ? "GET" : "BAG IS FULL";
        _nameText.text = CurrencyBase.Base[type].itemName;
    }

    public int AutoPick()
    {
        while (_bag.CheckEmptyCell(_type) && _oreCount > 0)
        {
            PutToBag();
        }
        return _oreCount;
    }
    private void PutToBag()
    {
        _bag.SetOre(_type);
        EventManager.AddCurrency(_type, 1);
        _oreCount--;
        if (_oreCount <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            SetData(_type, _oreCount);
        }
    }
    
}
