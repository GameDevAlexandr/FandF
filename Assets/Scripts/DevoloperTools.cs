using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GeneralData;
using static EnumsData;
using UnityEngine.UI;
using NaughtyAttributes;

public class DevoloperTools : MonoBehaviour
{
    [SerializeField] private InputField _input;
    [SerializeField] private Dropdown _chooseCurrency;
    [SerializeField] private DateTimeManager _timeManager;
    [SerializeField] private List<CurrencyType> _currensyList = new List<CurrencyType>();
    private void Awake()
    {
        List<Sprite> cSprite = new List<Sprite>();
        foreach(var item in CurrencyBase.Base)
        {
            cSprite.Add(item.Value.icon);
            _currensyList.Add(item.Value.type);
        }
        _chooseCurrency.AddOptions(cSprite);
        _chooseCurrency.value = 7;
    }

    public void AddCurrency()
    {
        int idx = _chooseCurrency.value;
        EventManager.AddCurrency(_currensyList[idx], int.Parse(_input.text));
    }

    public void AddDay()
    {
        dateValue.dayInGame++;
        _timeManager.dayInGameEvent.Invoke();
    }
}
