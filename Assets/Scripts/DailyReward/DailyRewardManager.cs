using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

public class DailyRewardManager : MonoBehaviour
{
    [SerializeField] private DailyRewardItem[] _items;
    [SerializeField] private GameObject _deilyRewardButton;
    [SerializeField] private DateTimeManager _timeMnager;
    [SerializeField] private Text _timerText;

    public void Init()
    {
        if(receivedDailyReward == null)
        {
            receivedDailyReward = new bool[_items.Length];
            dateValue.dayInGame = 0;
        }
        else if(receivedDailyReward.Length<_items.Length)
        {
            System.Array.Resize(ref receivedDailyReward, _items.Length);
        }
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].Init(i);
        }
        _timeMnager.dayInGameEvent.AddListener(UpdateDate);
        _timeMnager.everySecondEvent.AddListener(SetTimer);
    }

    private void UpdateDate()
    {
        _items[dateValue.dayInGame].SetActive(true);
    }
    private void SetTimer()
    {
        _timerText.text = MyString.GetHMS((System.DateTime.Today.AddDays(1) - System.DateTime.Now).TotalSeconds);
    }
}
