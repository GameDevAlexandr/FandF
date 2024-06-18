using UnityEngine;
using UnityEngine.Events;

public class CloseToOut : MonoBehaviour
{
    [SerializeField] private UnityEvent closeEvent;
    [SerializeField] private float _delayShowAfter;

    private RectTransform _rt;
    private bool _closeRady;

    private void Start()
    {
        _rt = GetComponent<RectTransform>();
        Controll.getPositionEvent.AddListener(Close);

    }

    private void OnEnable()
    {
        _closeRady = false;
        Invoke("CloseRady", _delayShowAfter);
    }

    private void CloseRady()
    {
        _closeRady = true;
    }
    private void Close(Vector2 position)
    {
        if (_closeRady && !RectTransformUtility.RectangleContainsScreenPoint(_rt, position))
        {
            _closeRady = false;
            closeEvent?.Invoke();
        }
    }
    public void Close()
    {
        if (_closeRady)
        {
            _closeRady = false;
            closeEvent?.Invoke();
        }
    }

    public void CloseButton()
    {
        _closeRady = true;
        Close();
    }

}
