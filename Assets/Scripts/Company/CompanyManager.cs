using System.Collections.Generic;
using UnityEngine;
using static GeneralData;
using static CalculationData;
using UnityEngine.UI;

public class CompanyManager : MonoBehaviour
{
    public Dictionary<int, FightPoint> pointBase { get { return _pointBase; } }
    private Dictionary<int, FightPoint> _pointBase;

    [SerializeField] private DateTimeManager _timeManager;
    [SerializeField] private MapSonInfo _mapSonInfo;
    [SerializeField] private FightPoint[] _points;
    [SerializeField] private FightPointInfo _pointInfo;
    [SerializeField] private Button _infoButton;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _heroRect;
    [SerializeField] private GameObject[] _chapters;
    [SerializeField] private Button _nextChapter;
    [SerializeField] private Button _beforeChapter;
    [SerializeField] private Text _remainingTimeText;

    private RectTransform _contentRect;
    private int _currentChapter;

    private void Awake()
    {
        EventManager.WinCompany.AddListener(CompletePoint);
        EventManager.LostCompany.AddListener(Lost);
        _nextChapter.onClick.AddListener(() => ChapterButtonClick(1));
        _beforeChapter.onClick.AddListener(() => ChapterButtonClick(-1));
        _nextChapter.interactable = !inCompany;
        _beforeChapter.interactable = !inCompany;
        _infoButton.onClick.AddListener(OpenPointInfo);
        if (sonData.companyIsComplete)
        {
            CompletePoint();
        }
        _nextChapter.gameObject.SetActive(sonData.chapterUnlock > 0);
        _contentRect = _scrollRect.content;
        _pointInfo.startCompanyEven.AddListener(DeactiveChangeChapterButton);
        //EventManager.AddForgeItem(EnumsData.ForgeItemType.mithrilSword, 4, 5);
        //EventManager.AddForgeItem(EnumsData.ForgeItemType.mitrilArmour, 4, 5);
        //EventManager.AddForgeItem(EnumsData.ForgeItemType.uncommonAmulet, 2, 5);
        for (int i = 0; i < _chapters.Length; i++)
        {
            _chapters[i].SetActive(false);
        }
        _chapters[sonData.currentChapter].SetActive(true);
    }
    public void Init()
    {
        if (_pointBase == null)
        {
            _pointBase = new Dictionary<int, FightPoint>();
            for (int i = 0; i < _points.Length; i++)
            {
                _pointBase.Add(_points[i].companyIndex, _points[i]);
            }
        }
        if(eventCompanyPoint== null)
        {
            EventManager.AddForgeItem(EnumsData.ForgeItemType.copperSword, 0, 1);
            EventManager.AddForgeItem(EnumsData.ForgeItemType.cooperArmour, 0, 1);
            EventManager.AddCurrency(EnumsData.CurrencyType.coin, 10);
            EventManager.AddCurrency(EnumsData.CurrencyType.leather, 2);
            eventCompanyPoint = new List<int>();
        }
        _timeManager.dayInGameEvent.AddListener(InitCompanyEvens);
        _timeManager.everySecondEvent.AddListener(() => { _remainingTimeText.text = 
            MyString.GetHMS((System.DateTime.Today.AddDays(1) - System.DateTime.Now).TotalSeconds);});
    }
    private void CompletePoint()
    {
        sonData.companyIsComplete = true;
        var nextPoints = pointBase[sonData.companyIndex].nextPoints;
        GameAnalitic.MissionCompleteEvent(sonData.companyIndex.ToString());
        if (pointBase[sonData.companyIndex].isNextChapterTrigger)
        {
            if(sonData.chapterUnlock <= _currentChapter)
            {
                Tutorial.TutorialHandler.tutorEvent.Invoke(Tutorial.TutorialHandler.IterationName.newCompany);
                sonData.chapterUnlock = _currentChapter + 1;
                _nextChapter.gameObject.SetActive(true);
            }
        }
        for (int i = 0; i < nextPoints.Length; i++)
        {
            if(nextPoints[i].item.isEvent && !eventCompanyPoint.Contains(nextPoints[i].item.index))
            {
                continue;
            }
            nextPoints[i].gameObject.SetActive(true);
        }
        pointBase[sonData.companyIndex].gameObject.SetActive(false);
        _mapSonInfo.SetData();
    }

    private void Lost()
    {        
        sonData.companyIsComplete = false;
        foreach(var point in pointBase)
        {
            point.Value.gameObject.SetActive(false);
        }
        pointBase[101].gameObject.SetActive(true);
        pointBase[201].gameObject.SetActive(true);
        //pointBase[0].gameObject.SetActive(true);
        sonData.hp = GetSonHealth();
        _mapSonInfo.SetData();
        _nextChapter.interactable = true;
        _beforeChapter.interactable = true;
    }
    private void InitCompanyEvens()
    {
        eventCompanyPoint.Clear();
        for (int i = 0; i < _points.Length; i++)
        {
            if (_points[i].item.isEvent)
            {
                eventCompanyPoint.Add(_points[i].item.index);
            }
        }
    }
    public void OpenPointInfo()
    {
        int idx =0;
        if (!sonData.companyIsComplete)
        {
            idx = FindIndex(sonData.companyIndex);
        }
        else if(sonData.companyIndex != 0)
        {
            for (int i = 0; i < _points.Length; i++)
            {
                if (_points[i].gameObject.activeSelf && !_points[i].item.isEvent)
                {
                    idx = i;
                    break;
                }
            }
        }
        if (sonData.companyIndex == 0)
        {
            idx = 1;
        }
        _points[idx].OnClick();
        ShiftContent();
    }
    private void ShiftContent()
    {
        float normalizedPosition = _heroRect.anchoredPosition.y / _contentRect.rect.size.y;
        _scrollRect.verticalNormalizedPosition = Mathf.Clamp01(normalizedPosition);
    }

    private int FindIndex(int companyIndex)
    {
        for (int i = 0; i < _points.Length; i++)
        {
            if(_points[i].companyIndex == companyIndex)
            {
                return i;
            }
        }
        return 0;
    }
    private void ChapterButtonClick(int count)
    {
        _chapters[_currentChapter].SetActive(false);
        _currentChapter+=count;
        _chapters[_currentChapter].SetActive(true);
        _scrollRect.content = _chapters[_currentChapter].GetComponent<RectTransform>();
        _nextChapter.gameObject.SetActive(_currentChapter < sonData.chapterUnlock && _currentChapter<_chapters.Length-1);
        _beforeChapter.gameObject.SetActive(_currentChapter > 0);
        sonData.currentChapter = _currentChapter;
    }

    private void DeactiveChangeChapterButton()
    {
        _nextChapter.interactable = false;
        _beforeChapter.interactable = false;
    }
}
