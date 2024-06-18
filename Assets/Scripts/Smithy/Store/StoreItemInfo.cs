using UnityEngine;
using UnityEngine.UI;

public class StoreItemInfo : MonoBehaviour
{
    [SerializeField] private Text _description;
    [SerializeField] private Text _countText;
    [SerializeField] private Image _itemIcon;
    public void SetData(string description, int count, Sprite icon)
    {
        gameObject.SetActive(true);
        _description.text = description;
        _countText.text = count>=0?count.ToString():"";
        _itemIcon.sprite = icon;
    }
}
