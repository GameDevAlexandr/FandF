using UnityEngine;
using static GeneralData;
using static CalculationData;
using System.Collections.Generic;
using UnityEngine.UI;

public class PowerTestManager : EnemySpawner
{
    [SerializeField] private CompanyItem[] _items;
    [SerializeField] private Button _testButton;
    [SerializeField] private PowerTestGrade[] _grades;
    [SerializeField] private GameModeManager _modeManager;
    [SerializeField] private Text _powerText;
    [SerializeField] private Text _maxPower;
    [SerializeField] private GameObject _powerPanel;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private TotalPowerPanel _totalPanel;
    [SerializeField] private GameObject _rewardPanel;
    [SerializeField] private RewardCell[] _rewardCells;

    private List<Enemy> _enemies => enemies;
    private int _currentPower;
    private int _level;
    private float _hardnessIndex;
    private void Start()
    {
        SetGradesData();
        _level = GetPowerGrade(sonData.power);
        EventManager.LostCompany.AddListener(Lost);
        SetPowerProgress();
        SetContentPositon();       
    }
    public override void DestroyEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        _currentPower += (int)(enemy.item.hardness*_hardnessIndex);
        if (_enemies.Count == 0)
        {
            if (waveCount < wawes.Length)
            {
                Spawn();
                SetHardnessIndex();
            }
        }
        else
        {
            SetTarget(_enemies.Count > 1 ? _enemies[1] : _enemies[0]);
        }        
        
        if (_currentPower > sonData.power)
        {
            sonData.power = _currentPower;
            _grades[_level].SetData(GetPowerProgress(_level));
            var lv = GetPowerGrade(sonData.power);
            if (_level< lv)
            {
               
                _grades[_level].GetReward();
                if (lv - _level > 1)
                {
                    _grades[_level + 1].GetReward();
                }
                _rewardPanel.SetActive(true);
                for (int i = 0; i < _rewardCells.Length; i++)
                {
                    if (!_rewardCells[i].gameObject.activeSelf)
                    {                        
                        _rewardCells[i].SetData(_grades[_level].reward.icon, _grades[_level].rwCount);
                        break;
                    }
                }
                _level = lv; 
                if(_level == _grades.Length)
                {
                    sonData.power = 0;
                    sonData.powerTestLevel++;
                    SetGradesData();
                    EventManager.LostCompany.Invoke();
                }
            }
        }
        SetContentPositon();
        SetUI();
    }
    public void SpawnEnemy()
    {
        for (int i = 0; i < _rewardCells.Length; i++)
        {
            _rewardCells[i].Clear();
        }
        _rewardPanel.SetActive(false);
        _totalPanel.SetBefore();
        _modeManager.PowerTestMode();
        _powerPanel.SetActive(true);
        _currentPower = 0;
        company = _items[sonData.powerTestLevel];
        _hardnessIndex = 1;
        GameAnalitic.PowerTestState("Start");
        SetPowerProgress();
        Spawn();
        SetUI();
    }
    private void Lost()
    {
        if (gameMode == EnumsData.GameMode.powerTest)
        {
            _totalPanel.Open();
            _powerPanel.SetActive(false);
            _modeManager.MapMode();
            _modeManager.SmithyMode();
            GameAnalitic.PowerTestState("Stop");
        }
    }
    private void SetContentPositon()
    {
        int cnt = _scrollRect.content.childCount;
        float pos = (float)_level / cnt;
        _scrollRect.horizontalNormalizedPosition = pos;
    }
   private void SetUI()
    {
        _powerText.text = _currentPower.ToString();
        _maxPower.text = sonData.power.ToString();
    }
    public void ExitPowerTest()
    {
        EventManager.LostCompany.Invoke();
    }
    private void SetHardnessIndex()
    {
        if (_enemies.Count == 3)
        {
            _hardnessIndex = 2;
        }
        else if (_enemies.Count == 2)
        {
            _hardnessIndex = 1.5f;
        }
    }
    private void SetPowerProgress()
    {
        for (int i = 0; i < _grades.Length; i++)
        {
            int pw = GetPowerForGrade(i);
            _grades[i].SetPowerNeed(pw);
            if (pw <= sonData.power)
            {
                _grades[i].CompleteGrade();
            }
        }
    }
    private void SetGradesData()
    {
        if (sonData.powerTestLevel > _items.Length - 1)
        {
            return;
        }
        var grades = Resources.LoadAll<PowerTestGradeItem>("PowerTestItems/Level_" + sonData.powerTestLevel);
        for (int i = 0; i < _grades.Length; i++)        
        {
            for (int j = 0; j < grades.Length; j++)
            {
                if (grades[j].level == i)
                {
                    _grades[i].SetItem(grades[j].reward, grades[j].count);
                    break;
                }
            }            
        }
    }
}
