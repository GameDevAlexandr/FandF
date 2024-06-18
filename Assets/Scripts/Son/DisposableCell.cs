using UnityEngine;
using UnityEngine.UI;

public class DisposableCell:MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _count;
    [SerializeField] private Button _button;
    [SerializeField] private DisposableItemInfo _info;

    private PotionItem _item;
    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        _info.SetData(_item);
        Sounds.chooseSound.otherButtons.Play();
    }
    public void Init(PotionItem item, int count)
    {
        _item = item;
        _icon.enabled = true;
        _button.interactable = true;
        _icon.sprite = _item.icon;
        _count.text = count.ToString();
    }
    public void Empty()
    {
        _icon.enabled = false;
        _button.interactable = false;
        _count.text = "";
    }
}
