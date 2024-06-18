using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GeneralData;
using static EnumsData;

public class DailyQuest : MonoBehaviour
{
    [SerializeField] private ApplovinAD _adManager;
    [SerializeField] private DailyQData[] _questData;
    [SerializeField] private Text _timeCounter;


    private bool _dataIsSet;
    private string _curAdId;
    [System.Serializable] 
    public struct DailyQData
    {
        public DailyQuestItem item;
        public QuestType type;
        public int count;
        public int gemReward;
        public bool ad;        
        [TextArea] public string description;        
    }

    

    private void Start()
    {
        if (questData.daily == null || questData.daily.Length != _questData.Length)
        {
            questData.daily = new GeneralData.DailyQuest[_questData.Length];
        }
        SetItemData();
        StartCoroutine(TimerUpdate());
        _adManager.completeEvent.AddListener(WatchAd);
        EventManager.ChangeSouls.AddListener(Summon);
        EventManager.WinCompany.AddListener(LevelComplete);
        EventManager.EnemyDeath.AddListener(AddKills);
        EventManager.ChangeCurrency.AddListener(AddCurrency);
    }

    public void ClearDailyQuest()
    {
        if (questData.daily == null)
        {
            return;
        }
        for (int i = 0; i < questData.daily.Length; i++)
        {
            questData.daily[i].complete = false;
            questData.daily[i].count = 0;
        }

        if (!_dataIsSet)
        {
            SetItemData();
        }

        for (int i = 0; i < _questData.Length; i++)
        {
            _questData[i].item.transform.SetSiblingIndex(i);
            _questData[i].item.SetData();
        }
    }

    IEnumerator TimerUpdate()
    {
        yield return new WaitForSeconds(1);
        _timeCounter.text =MyString.GetHMS((DateTime.Today.AddDays(1) - DateTime.Now).TotalSeconds);
        StartCoroutine(TimerUpdate());
    }
    private void SetItemData()
    {
        for (int i = 0; i < _questData.Length; i++)
        {
            DailyQData qd = _questData[i];
            qd.item.dailyQuestManager = this;
            _questData[i].item.SetData(qd.type, qd.description, qd.count, qd.gemReward, qd.ad, i);
        }
        _dataIsSet = true;
    }

    private void AddKills()
    {
        if (gameMode == GameMode.fight)
        {
            ChangeData(GetIndex(QuestType.kill));
        }
    }
    private void AddCurrency(CurrencyType type, int count)
    {
        if (gameMode != GameMode.mine)
        {
            return;
        }
        if(type== CurrencyType.coal)
        {
            ChangeData(GetIndex(QuestType.mineCoal));
        }
        else if(type == CurrencyType.greenStone || type == CurrencyType.blueStone || type == CurrencyType.redStone)
        {
            ChangeData(GetIndex(QuestType.mineStone));
        }
        else
        {
            ChangeData(GetIndex(QuestType.mineOre));
        }
    }
    private void LevelComplete()
    {
        ChangeData(GetIndex(QuestType.levelComplete));
    }

    private void WatchAd(string adId)
    {
        ChangeData(GetIndex(QuestType.watchAd));
    }

    private void Summon()
    {
        ChangeData(GetIndex(QuestType.summon));
    }
    public void QuestComplete()
    {
        ChangeData(GetIndex(QuestType.questComplete));
    }

    private void ChangeData(List<int> idx)
    {
        for (int i = 0; i < idx.Count; i++)
        {
            questData.daily[idx[i]].count++;
            _questData[idx[i]].item.SetData();
        }        
    }
    private List<int> GetIndex(QuestType type)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < _questData.Length; i++)
        {
            if(_questData[i].type == type)
            {
                result.Add(i);
            }
        }
        return result;
    }
}
