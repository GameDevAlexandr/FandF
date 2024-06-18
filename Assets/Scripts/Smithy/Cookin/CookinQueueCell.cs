using UnityEngine;
using UnityEngine.UI;

public class CookinQueueCell : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _progressBar;
    [SerializeField] private Button _removeButton;

    private ICookinQueue _queue;
    private InProcessIcon _procIcon;
    public void Initialize(Sprite icon, float progress, InProcessIcon procIcon, ICookinQueue queue)
    {
        _procIcon = procIcon;
        _icon.sprite = icon;
        _queue = queue;
        _removeButton.onClick.AddListener(Remove);
        ChangeProgress(progress);
    }

    private void Remove()
    {
        _queue.RemoveItem(this, false);
        _procIcon.Activate(false);
    }

    public void ChangeProgress(float progress)
    {
        if(progress > 0)
        {
            if (!_procIcon.onActive)
            {
                _procIcon.Activate(true);
                _procIcon.SetItem(_icon.sprite);
            }
            _procIcon.SetData(progress);
        }
        _progressBar.transform.parent.gameObject.SetActive(progress > 0);
        _progressBar.fillAmount = progress;
    }
}
