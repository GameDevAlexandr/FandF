using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;

public class DisposableManager : MonoBehaviour
{
    [SerializeField] private DisposableEqipCell[] _cells;
    [SerializeField] private DisposableCell[] _dCells;

    private void Awake()
    {
        EventManager.ChangeSingleItem.AddListener(ChangeParam);
        EventManager.ChangePotions.AddListener((PotionType t, int l, int c) => SetDCells());
        SetDCells();
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].Init();
        }
    }

    private void ChangeParam(SingleItemType type)
    {
        if(type == SingleItemType.travelBag)
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].ChangeData();
            }
        }
    }
    private void SetDCells()
    {
        int idx = 0;
        for (int i = 0; i < potionItems.Length; i++)
        {
            for (int j = 0; j < potionItems[i].items.Length; j++)
            {
                if (potionItems[i].items[j] > 0)
                {
                    var item = PotionItemsBase.Base[(PotionType)i][j];
                    _dCells[idx].Init(item, potionItems[i].items[j]);
                    idx++;
                }
            }
        }
        for (int i = idx; i < _dCells.Length; i++)
        {
            _dCells[i].Empty();
        }
    }
}
