using Spine.Unity;
using UnityEngine;
using static GeneralData;

public class AlmanackItem : MonoBehaviour
{
    [SerializeField] private SkeletonGraphic _enemy;
    [SerializeField] private GameObject _lock;
    [SerializeField] private EnemyItem _item;

    public void Init()
    {
        _enemy.skeletonDataAsset = _item.icon;
        _enemy.Initialize(true);
        _enemy.gameObject.SetActive(false);
        for (int i = 0; i < almanac.Count; i++)
        {
            if (almanac[i].eName == _item.enemyName && almanac[i].count > 0)
            {
                _enemy.gameObject.SetActive(true);
                _lock.gameObject.SetActive(false);
                break;
            }
        }
        EventManager.UpdateAlmanack.AddListener(Unlock);
    }
    private void Unlock(string eName) 
    {
        if (_lock.activeSelf)
        {
            if(eName== _item.enemyName)
            {
                _enemy.gameObject.SetActive(true);
                _lock.gameObject.SetActive(false);
            }
        }
    }
}
