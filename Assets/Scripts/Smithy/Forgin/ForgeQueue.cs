using System.Collections.Generic;
using UnityEngine;
using static EnumsData;
using static GeneralData;
using static CalculationData;

public class ForgeQueue : MonoBehaviour, ICookinQueue
{
    [SerializeField] private CookinQueueCell _cellPrefab;
    [SerializeField] private Transform _queueContent;
    [SerializeField] private InProcessIcon _procIcon;
    [SerializeField] private TextGenerator _strenghtText;
    [SerializeField] private GameObject _anvilSelected;

    private int _curProgress;    
    private List<CookinQueueCell> _cells = new List<CookinQueueCell>();
    private int _curHardness { get { return forgeQueue.Count > 0 ? CookBook.forgeItem[GetItemTipe(0)].hardness : 0; } }
    public void Init()
    {
        if (forgeQueue.Count > 0) 
        {
            for (int i = 0; i < forgeQueue.Count; i++)
            {
                AddQueueContent(i);
            }
            _curProgress = (int)(_curHardness * forgeQueue[0].progress);
        }
    }
    public void AddToQueue(int index)
    {
        CookData cd = new CookData();
        cd.typeIndex = index;
        cd.progress = 0;
        forgeQueue.Add(cd);
        AddQueueContent(forgeQueue.Count - 1);
        ChangeCurrency(index, false);
        Sounds.chooseSound.otherButtons.Play();
    }

    public void AddQueueContent(int index)
    {
        _cells.Add(Instantiate(_cellPrefab, _queueContent));
        Sprite icon = GetItem(index).icon;
        _cells[_cells.Count - 1].Initialize(icon, forgeQueue[index].progress, _procIcon, this);
        _anvilSelected.SetActive(true);
    }
    public void RemoveItem(CookinQueueCell cell, bool isComplete)
    {
        int idx =_cells.IndexOf(cell);
        _cells.Remove(cell);
        Destroy(cell.gameObject);        
        if (!isComplete)
        {
            ChangeCurrency(forgeQueue[idx].typeIndex, true);
        }
        forgeQueue.RemoveAt(idx);
        _anvilSelected.SetActive(forgeQueue.Count > 0);
    }

    private void ChangeCurrency(int index, bool isAdd)
    {
        var rec = CookBook.forgeItem[(ForgeItemType)index].ingredients;
        for (int i = 0; i < rec.Length; i++)
        {
            int count = rec[i].count;
            if (!rec[i].isCurrency)
            {                
                ForgeItemType fIt = rec[i].fItem;
                EventManager.AddForgeItem(fIt, 4, isAdd? count:-count);               
            }
            else
            {
                CurrencyType igd = rec[i].item;
                EventManager.AddCurrency(igd, isAdd ? count : -count);
            }
        }
    }
    public void AddProgress(int count)
    {
        if (_curHardness > 0)
        {
            _curProgress += count;
            _strenghtText.StartFly(count.ToString(), false);
            var nIt = new CookData();
            nIt = forgeQueue[0];
            nIt.progress = (float)_curProgress / _curHardness;
            forgeQueue[0] = nIt;
            _cells[0].ChangeProgress(nIt.progress);
            if (Sounds.chooseSound)
            {
                Sounds.chooseSound.RandomPitch(Sounds.chooseSound.forgin, 0.1f);
            }
            if (_curProgress >= _curHardness)
            {
                int trail = _curProgress - _curHardness;
                CutQueue();
                if (trail > 0)
                {
                    AddProgress(trail);
                }
            }          
        }
    }
    private void CutQueue()
    {
        var type = (ForgeItemType)forgeQueue[0].typeIndex;
        _curProgress = 0;
        var itm = GetItem(0);
        RemoveItem(_cells[0], true);
        int grd = GetForgeItemGrade(itm.material);
        EventManager.AddForgeItem(type, grd, 1);
        if (forgeItems[(int)ForgeItemType.copperSword].items[0] >= 1)
        {
            TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.forgeComplete);
        }
        if (forgeItems[(int)ForgeItemType.commonAmulet].items[0] >= 1)
        {
            TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.amuletComplete);
        }
        GameAnalitic.Forge(type.ToString() + "_" + grd);       
    }

    private ForgeItem GetItem(int qIndex)
    {
        return (ForgeItemBase.Base[GetItemTipe(qIndex)][0]);
    }
    private ForgeItemType GetItemTipe(int qIndex)
    {
        return (ForgeItemType)forgeQueue[qIndex].typeIndex;
    }
}
