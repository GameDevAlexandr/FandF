using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GeneralData;
using static CalculationData;

public class FightSoulsManager : MonoBehaviour
{
    public int targetIndex => _spawner.targetIndex;
    public List<Enemy> enemies => _spawner.enemies;
    
    [SerializeField] private EnemySpawner _companySpawner;
    [SerializeField] private PowerTestManager _powerTestSpawner;
    [SerializeField] private FightSoulCell[] _cells;
    [SerializeField] private Image[] _spare;

    private EnemySpawner _spawner => gameMode == EnumsData.GameMode.fight ? _companySpawner : _powerTestSpawner;
    private void Awake()
    {
        Load();
        EventManager.LostCompany.AddListener(StartCompany);
        StartCompany();
    }
    public void ChangeSoul(FightSoulCell cell)
    {
        for (int i = 0; i < sonData.spareSouls.Length ; i++)
        {
            if (sonData.spareSouls[i] != -1 && _spare[i].transform.parent.gameObject.activeSelf)
            {
                _spare[i].transform.parent.gameObject.SetActive(false);
                if (souls[sonData.spareSouls[i]].energy > 0)
                {                    
                    cell.SetData(sonData.spareSouls[i]);
                    return;
                }
            }
        }
        cell.gameObject.SetActive(false);
    }

    public void StartCompany()
    {
        if(inCompany)
        {
            return;
        }
        _companySpawner.isTarget = false;
        _powerTestSpawner.isTarget = false;
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < sonData.figthSouls.Length; i++)
        {
            int idx = sonData.figthSouls[i];
            if (idx!= -1)
            {
                SoulsData soul = souls[idx];
                soul.energy = SoulEnergy(idx);
                souls[idx] = soul;
                _cells[i].gameObject.SetActive(true);
                _cells[i].SetData(sonData.figthSouls[i]);
                _companySpawner.isTarget = true;                
                _powerTestSpawner.isTarget = true;                
            }
        }
        for (int i = 0; i < _spare.Length; i++)
        {
            _spare[i].transform.parent.gameObject.SetActive(false);
        }

        for (int i = 0; i < sonData.spareSouls.Length; i++)
        {
            int idx = sonData.spareSouls[i];
            if (idx != -1)
            {
                SoulsData soul = souls[idx];
                soul.energy = SoulEnergy(idx);
                souls[idx] = soul;
                _spare[i].transform.parent.gameObject.SetActive(true);
                _spare[i].sprite = SoulsBase.IndexBase[souls[idx].index].icon;                
            }
        }
    }
    private void Load()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < _spare.Length; i++)
        {
            _spare[i].transform.parent.gameObject.SetActive(false);
        }
        int chCount = 0;
        for (int i = 0; i < sonData.figthSouls.Length; i++)
        {
            if (sonData.figthSouls[i] != -1)
            {
                _cells[i].gameObject.SetActive(true);
                if (souls[sonData.figthSouls[i]].energy > 0)
                {                   
                    _cells[i].SetData(sonData.figthSouls[i]);
                }
                else
                {
                    ChangeSoul(_cells[i]);
                    chCount++;
                }
            }
        }
        for (int i = chCount; i < _spare.Length; i++)
        {
            int idx = sonData.spareSouls[i];
            if (sonData.spareSouls[i] != -1)
            {
                _spare[i].transform.parent.gameObject.SetActive(true);
                _spare[i].sprite = SoulsBase.IndexBase[souls[idx].index].icon;
            }
        }
    }
}
