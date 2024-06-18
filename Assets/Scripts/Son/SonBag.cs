using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumsData;

public class SonBag : MonoBehaviour
{
    [SerializeField] private SonBagCell[] cells;
    [SerializeField] private RewardCell[] _lootCells;
    [SerializeField] private GameObject _bag;

    public void PutToBag(CurrencyType type, int count)
    {
        CurrencyItem item = CurrencyBase.Base[type];
        for (int i = 0; i < cells.Length; i++)
        {
            if (!cells[i].isfull ||(cells[i].isfull&&cells[i].type == type))
            {
                cells[i].SetData(item, count);
                _lootCells[i].SetData(item.icon, count);
                break;
            }
        }
    }
    public void ClearBag()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].SetEmpty();
            _lootCells[i].Clear();
        }
    }
    private void OnEnable()
    {
        ClearBag();
    }
}
