using UnityEngine;
using static GeneralData;
using static EnumsData;
using System.Collections.Generic;

public class ForgeItemStorage : MonoBehaviour
{
    [SerializeField] private MergeItems _item;
    [SerializeField] private Transform _storageContent;
    [SerializeField] private GameObject _emptyObj;
    [SerializeField] private NotificationItem _notification;

    private List<MergeItems> _items = new List<MergeItems>(); 
    private void Awake()
    {
        CreateLine(8);
        for (int i = 0; i < forgeItems.Length; i++)
        {
            for (int j = 0; j < forgeItems[i].items.Length; j++)
            {
                AddItem((ForgeItemType)i, j, forgeItems[i].items[j]);
            }
        }
        EventManager.ChangrForgeitem.AddListener(AddItem);
        _emptyObj.SetActive(_items.Count == 0);
    }

    private void CreateLine(int count)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < count; j++)
            {
                _items.Add(Instantiate(_item, _storageContent));
                _items[_items.Count - 1].storage = this;
            }
        }
    }
    private void AddItem(ForgeItemType type, int level, int count)
    {
        if (count == 0) return;
        var fItem = ForgeItemBase.Base[type][level];
        int last = _items.Count-1;
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].item == null)
            {
                last = i - 1;
                break;
            }
        }        
        while (count > 0)
        {
            last++;
            if (_items.Count == last) CreateLine(1);
            _items[last].SetData(fItem);
            count--;
        }
        _notification.SetNotification(CheckIsMerge());
        _emptyObj.SetActive(_items.Count == 0);
    }
    public void RemoveItem(MergeItems item)
    {
        _items.Remove(item);
        _items.Add(item);
        item.EmptyCell();
    }
    private bool CheckIsMerge()
    { 
        for (int i = 0; i < forgeItems.Length; i++)
        {
            for (int j = 0; j < forgeItems[i].items.Length-1; j++)
            {
                if (forgeItems[i].items[j] > 1)
                {
                    return true;
                }
            }
            
        }
        return false;
    }
}
