using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static GeneralData;

public class FightPointInfo : MonoBehaviour
{
    [HideInInspector] public UnityEvent startCompanyEven = new UnityEvent();

    [SerializeField] private Text _difficulty;
    [SerializeField] private Button _goToPoint;
    [SerializeField] private Button _startCompany;
    [SerializeField] private Button _run;
    [SerializeField] private Text _timeTo;
    [SerializeField] private DateTimeManager _timeManager;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private GameModeManager _gmManager;
    [SerializeField] private GameObject _comeBackHome;
    [SerializeField] private NotificationItem _notification;
    [SerializeField] private RewardCell[] _rwCells;
    [SerializeField] private EnemyInfoItem[] _enCells;
    

    private int _time;
    private int _index;
    private CompanyItem _item;
    private void Awake()
    {
        _goToPoint.onClick.AddListener(GoToPoint);
        _startCompany.onClick.AddListener(Fight);
        _timeManager.everySecondEvent.AddListener(Tic);
    }
    public void SetData(CompanyItem item, int index, int timeTo)
    {
        _comeBackHome.SetActive(index == 0);        
        _item = item;
        _difficulty.text = item.difficulty.ToString();
        _time = timeTo;
        _index = index;
        SetButtons();
        List<EnemyItem> eIt = new List<EnemyItem>();
        for (int i = 0; i < item.wawes.Length; i++)
        {
            for (int j = 0; j < item.wawes[i].enemyes.Length; j++)
            {
                if (!eIt.Contains(item.wawes[i].enemyes[j]))
                {
                    eIt.Add(item.wawes[i].enemyes[j]);
                }
            }
        }
        for (int i = 0; i < _enCells.Length; i++)
        {
            _enCells[i].gameObject.SetActive(i< eIt.Count);
            if (i < eIt.Count)
            {
                _enCells[i].SetData(eIt[i]);
            }
        }
        for (int i = 0; i <_rwCells.Length; i++)
        {
            _rwCells[i].gameObject.SetActive(i < item.reward.Length);
                
            if (i < item.reward.Length)
            {
                var rw = item.reward[i];
                Sprite icon;
                if (rw.type == EnumsData.StoreItemType.currency)
                {                   
                    icon = CurrencyBase.Base[rw.cType].icon;
                }
                else if (rw.type == EnumsData.StoreItemType.forge)
                {                  
                    icon = ForgeItemBase.Base[rw.fTipe][rw.level].icon;
                }
                else
                {                    
                    icon = PotionItemsBase.Base[rw.pType][rw.level].icon;
                }

                _rwCells[i].Clear();
                _rwCells[i].SetData(icon, item.reward[i].count);
            }
        }
        Tic();
        if (!inCompany && _index ==0)
        {
            _gmManager.SmithyMode();
            return;
        }
        gameObject.SetActive(true);
    }
    private void GoToPoint()
    {
        sonData.secondsToCompany = sonData.currentChapter+1<=sonData.chapterComplete? _time-120:_time;
        sonData.companyIndex = _index;
        sonData.companyIsComplete = false;
        if (_index == 0)
        {
            TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.startCraft);
            EventManager.LostCompany.Invoke();
            GetComponent<BlackBack>().Close();
            sonData.secondsToCompany = 0;
        }
        else
        {
            startCompanyEven.Invoke();
        }
        Tic();
    }

    private void Fight()
    {
        _gmManager.FightMode();
        _spawner.company = _item;
        _spawner.Spawn();
    }

    private void Tic()
    {
        _timeTo.gameObject.SetActive((sonData.secondsToCompany > 0|| _index != sonData.companyIndex)&&_index!=0);
        _timeTo.text = _index != sonData.companyIndex ? MyString.GetMS(_time) : MyString.GetMS(sonData.secondsToCompany);
        SetButtons();
    }
    private void SetButtons()
    {
        if (sonData.secondsToCompany == 0)
        {
            _goToPoint.gameObject.SetActive(_index != sonData.companyIndex);
            _startCompany.gameObject.SetActive(_index == sonData.companyIndex);
        }
        else
        {
            _goToPoint.gameObject.SetActive(false);
            _startCompany.gameObject.SetActive(false);
            _run.gameObject.SetActive(_index == sonData.companyIndex);
        }
        _notification.SetNotification(sonData.secondsToCompany == 0 && !sonData.companyIsComplete && sonData.companyIndex != 0);
        if(_index == 0)
        {
            _goToPoint.gameObject.SetActive(true);
        }
    }
    private void OnEnable()
    {
        Tic();
    }
}
