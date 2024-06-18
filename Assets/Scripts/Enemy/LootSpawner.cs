using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumsData;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] LootItem _lootPrefab;
    [SerializeField] Transform _minPoint;
    [SerializeField] Transform _maxPoint;
    [SerializeField] SonBag _inventory;

    Vector2 _start;
    Vector2 _end;
    private void Awake()
    {
        float x = Random.Range(_minPoint.position.x, _maxPoint.position.x);
        float y = Random.Range(_minPoint.position.y, _maxPoint.position.y);
        _end = new Vector2(x, y);
    }

    public void SetLoot(EnemyItem.Reward[] rewards, Vector2 enemyPosition)
    {
        Vector2  x = new Vector2(_minPoint.position.x, _maxPoint.position.x);
        Vector2  y = new Vector2(_minPoint.position.y, _maxPoint.position.y);
        
        List<EnemyItem.Reward> rws = new List<EnemyItem.Reward>();
        for (int i = 0; i < rewards.Length; i++)
        {
            for (int j = 0; j < rewards[i].chance; j++)
            {
                rws.Add(rewards[i]);
            }
            
        }
        int rndRw = Random.Range(0, 100);
        if (rndRw >= rws.Count)
        {
            return;
        }
        CurrencyItem rw = CurrencyBase.Base[rws[rndRw].type];
        int cnt = Random.Range(rws[rndRw].spread.x, rws[rndRw].spread.y + 1);
        for (int i = 0; i < cnt; i++)
        {
            _end = new Vector2(Random.Range(x.x, x.y), Random.Range(y.x, y.y));
            Instantiate(_lootPrefab, transform.position, Quaternion.identity).Drop(_start, _end, _inventory, rw, 1);
        }        
    }
}
