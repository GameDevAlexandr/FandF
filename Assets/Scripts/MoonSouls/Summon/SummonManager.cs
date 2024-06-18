using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;

public class SummonManager : MonoBehaviour
{
    [SerializeField] private Button _summonButton;
    [SerializeField] private Button _multiSummonButton;
    [SerializeField] private Image _stoneIcon;
    [SerializeField] private SummonSoul _summon;
    [SerializeField] private GameObject _summonPanel;
    [SerializeField] private SoulStoneCell[] _cells;

    private CurrencyType _stone;
    private void Awake()
    {
        _summonButton.onClick.AddListener(Summon);
        _multiSummonButton.onClick.AddListener(MultiSummon);
    }

    private void SetInteractable()
    {
            _summonButton.interactable = currency[(int)_stone] > 0;
            _multiSummonButton.interactable = currency[(int)_stone] > 9;
    }

    public void SetData(CurrencyType stone)
    {
        _stone = stone;
        _stoneIcon.sprite = CurrencyBase.Base[stone].icon;
        _summonPanel.SetActive(true);
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].Deselect();
        }
        SetInteractable();
    }
    private void Summon()
    {
        _stoneIcon.sprite = _summon.Summon(_stone).icon;        
        SetInteractable();
    }
    private void MultiSummon()
    {
        for (int i = 0; i < 10; i++)
        {
            Summon();
        }
    }
}
