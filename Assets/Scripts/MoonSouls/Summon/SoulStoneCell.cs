using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GeneralData;
using static EnumsData;

public class SoulStoneCell : InventoryItem
{
    [SerializeField] private Button _mainButon;
    [SerializeField] private SummonManager _manager;
    [SerializeField] private Image _selectFrame;
    private void Awake()
    {
        Init();
        _mainButon.onClick.AddListener(Select);
    }
    public override void Init()
    {
        EventManager.ChangeCurrency.AddListener((CurrencyType t, int i) => SetInteractable());
        base.Init();

    }
    private void SetInteractable()
    {
        _mainButon.interactable = currency[(int)item.type] > 0;
    }

    private void Select()
    {
        _manager.SetData(item.type);
        _selectFrame.enabled = true;
    }

    public void Deselect()
    {
        _selectFrame.enabled = false;
    }
}
