using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;

public class SmeltQueue : MonoBehaviour, ICookinQueue
{
    [SerializeField] private CookinQueueCell _cellPrefab;
    [SerializeField] private Transform _queueContent;
    [SerializeField] private InProcessIcon _procIcon;
    [SerializeField] private TextGenerator _strenghtText;
    [SerializeField] private GameObject _crucibleSelected;

    private int _curProgress;    
    private List<CookinQueueCell> _cells = new List<CookinQueueCell>();
    private int _curHardness { get { return smeltQueue.Count > 0 ? CookBook.item[(CurrencyType)smeltQueue[0].typeIndex].hardness : 0; } }
    public void Init()
    {
        
        if (smeltQueue.Count > 0) 
        {
            for (int i = 0; i < smeltQueue.Count; i++)
            {
                AddQueueContent(i);
            }
            _curProgress = (int)(_curHardness * smeltQueue[0].progress);
        }
    }
    public void AddToQueue(int index)
    {
        CookData cd = new CookData();
        cd.typeIndex = index;
        cd.progress = 0;
        smeltQueue.Add(cd);
        AddQueueContent(smeltQueue.Count - 1);
        ChangeCurrency(cd.typeIndex, false);
        Tutorial.TutorialHandler.tutorEvent.Invoke(Tutorial.TutorialHandler.IterationName.fastedSmelt);
    }

    public void AddQueueContent(int index)
    {
        _cells.Add(Instantiate(_cellPrefab, _queueContent));
        _cells[_cells.Count - 1].Initialize(CurrencyBase.Base[(CurrencyType)smeltQueue[index].typeIndex].icon, smeltQueue[index].progress,_procIcon, this);
        _crucibleSelected.SetActive(true);
    }
    public void RemoveItem(CookinQueueCell cell, bool isComplete)
    {
        int idx =_cells.IndexOf(cell);
        _cells.Remove(cell);
        Destroy(cell.gameObject);        
        if (!isComplete)
        {
            ChangeCurrency(smeltQueue[idx].typeIndex, true);
        }
        smeltQueue.RemoveAt(idx);
        _crucibleSelected.SetActive(smeltQueue.Count > 0);
    }

    private void ChangeCurrency(int index, bool isAdd)
    {
        var item = (CurrencyType)index;
        for (int i = 0; i < CookBook.item[item].ingredients.Length; i++)
        {
            CurrencyType igd = CookBook.item[item].ingredients[i].item;
            int count = CookBook.item[item].ingredients[i].count;
            EventManager.AddCurrency(igd, isAdd ? count : -count);
        }
    }
    public void AddProgress(int count)
    {
       
        if (_curHardness > 0)
        {
            _curProgress += count;
            _strenghtText.StartFly(count.ToString(), false);
            var nIt = new CookData();
            nIt = smeltQueue[0];
            nIt.progress = (float)_curProgress / _curHardness;
            smeltQueue[0] = nIt;
            _cells[0].ChangeProgress(nIt.progress);
            if (_curProgress >= _curHardness)
            {
                int trail = _curProgress - _curHardness;
                CutQueue();
                if (trail > 0)
                {
                    AddProgress(trail);
                }
                return;
            }            
        }
    }
    private void CutQueue()
    {
        var type = (CurrencyType)smeltQueue[0].typeIndex;
        _curProgress = 0;
        RemoveItem(_cells[0], true);
        EventManager.AddCurrency(type, 1);
        GameAnalitic.Smelting(type.ToString());
    }
}
