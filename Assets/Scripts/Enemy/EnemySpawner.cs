using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{    
    public SonFight son;
    public List<Enemy> enemies => _enemies;
    public bool isTarget { set { _target.gameObject.SetActive(value); } }
    [HideInInspector]public int targetIndex { get; private set;}
    [HideInInspector]public int waveCount;
    public CompanyItem.EnemyWave[] wawes => company.wawes;
    public CompanyItem company { set { _company = value; } get { return _company; } }
    public BossKolossHandler bkHandler;
    [HideInInspector]public Vector2 heroPosition;

    [SerializeField] private FinishPanel _finishPanel;
    [SerializeField] private LootSpawner _loot;
    [SerializeField] private Text _enemyCounterText;
    [SerializeField] private Point[] _spawnPoint;
    [SerializeField] private Transform _target;
    [SerializeField] private TextGenerator _expTG;
    

    private CompanyItem _company;   
    private List<Enemy> _enemies = new List<Enemy>();
    private int _enemyCounter;
    [System.Serializable]
    public struct Point
    {
       public Transform[] spawnPoint;
    }
    private void Awake()
    {
        EventManager.LostCompany.AddListener(Lost);
        heroPosition = son.transform.position;
    }
    public void Spawn()
    {
        if (_enemyCounterText &&_enemyCounter == 0)
        {
            for (int i = 0; i <wawes.Length; i++)
            {
                for (int j = 0; j < wawes[i].enemyes.Length; j++)
                {
                    _enemyCounter++;
                }
            }
            _enemyCounterText.text = _enemyCounter>1? "ENEMIES: "+ _enemyCounter:"";
        }
        int el = wawes[waveCount].enemyes.Length;
        Transform[] pos = _spawnPoint[el - 1].spawnPoint;
        for (int i = 0; i < el; i++)
        {
            wawes[waveCount].enemyes[i].enemy.spawner = this;
            _enemies.Add(Instantiate(wawes[waveCount].enemyes[i].enemy, pos[i].position, Quaternion.identity));
            //_enemies[_enemies.Count - 1].spawner = this;
        }
        SetTarget(_enemies.Count > 1 ? _enemies[1] : _enemies[0]);
        waveCount++;
    }
    public virtual void DestroyEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        _loot.SetLoot(enemy.item.reward, enemy.transform.position);
        EventManager.AddExpForSon(enemy.item.exp);
        _expTG.StartFly("<color=blue>EXP+ </color>" + enemy.item.exp, false);
        _enemyCounter--;
        _enemyCounterText.text = "ENEMIES: " + _enemyCounter;
        if (_enemies.Count == 0)
        {
            if (waveCount < wawes.Length)
            {
                Spawn();
            }
            else
            {
                Victory();
            }
        }
        else
        {
            SetTarget(_enemies.Count > 1 ? _enemies[1] : _enemies[0]);
        }
    }
    private void Victory()
    {        
        _finishPanel.SetVictory(company);
        waveCount = 0;
        _enemies.Clear();
        _enemyCounter = 0;
        EventManager.WinCompany.Invoke();
        TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.missionComplete);
        GameAnalitic.PlayerState("Victory");        
    }
    private void Lost()
    {
        if (waveCount > 0)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                Destroy(_enemies[i].gameObject);
            }
            _enemies.Clear();
            waveCount = 0;
            if (GeneralData.gameMode== EnumsData.GameMode.fight)
            {
                _finishPanel.SetLost();
            }
        }
        _enemyCounter = 0;
        GameAnalitic.CompanyComplete(GeneralData.sonData.companyIndex.ToString());
    }
    public void SetTarget(Enemy targetEnemy)
    {
        _target.position = targetEnemy.transform.position;
        targetIndex = _enemies.IndexOf(targetEnemy);
    }
}
