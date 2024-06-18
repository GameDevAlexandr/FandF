using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

public class PocketCell : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _countText;
    [SerializeField] private Button _button;

    private UseDisposableManager _manager;
    private PotionItem _item;
    private int _index;

    private void Awake()
    {
        _button.onClick.AddListener(GetInfo);
    }
    public void SetItem(PotionItem item, int pocketIndex, UseDisposableManager manager)
    {
        gameObject.SetActive(true);
        _item = item;
        _index = pocketIndex;
        _manager = manager;
        ChangeCount();
    }

    public void ChangeCount()
    {
        if (sonData.pocket[_index].count > 0)
        {
            _icon.enabled = true;
            _icon.sprite = _item.icon;
            _countText.text = sonData.pocket[_index].count.ToString();
        }
        else
        {
            gameObject.SetActive(false);
            _icon.enabled = false;
            _countText.text = "";
        }
        _button.interactable = sonData.pocket[_index].count > 0;
    }

    public void GetInfo()
    {
        _manager.SetItem(_item, _index);
    }
}
