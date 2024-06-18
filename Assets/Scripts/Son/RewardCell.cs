using UnityEngine;
using UnityEngine.UI;

public class RewardCell : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _count;

    private int _counter;
    public void SetData(Sprite icon, int count)
    {
        gameObject.SetActive(true);
        _icon.sprite = icon;
        _counter += count;
        _count.text = _counter==0?"": _counter.ToString();
    }
    public void Clear()
    {
        _counter = 0;
        gameObject.SetActive(false);
    }
}
