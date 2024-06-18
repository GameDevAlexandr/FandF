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

    private void AddItem(ForgeItemType type, int level, int count)
    {
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                var fItem = ForgeItemBase.Base[type][level];
                _items.Add(Instantiate(_item, _storageContent));
                _items[_items.Count-1].SetData(fItem);
                _items[_items.Count-1].storage = this;
            }
        }
        else if (count < 0)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if(_items[i].item.type == type && _items[i].item.level == level)
                {
                    RemoveItem(_items[i]);
                    break;
                }
            }
        }
        _notification.SetNotification(CheckIsMerge());
        _emptyObj.SetActive(_items.Count == 0);
    }
    public void RemoveItem(MergeItems item)
    {
        _items.Remove(item);
        Destroy(item.gameObject);
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
