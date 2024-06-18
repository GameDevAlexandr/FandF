using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MultiCloser : MonoBehaviour
{
    public static UnityEvent<MultiCloser, int> closeEven = new UnityEvent<MultiCloser, int>();
    [SerializeField] private GameObject _openObject;
    public UnityEvent _openEvent;
    [SerializeField] private Sprite _closeSprite;
    [SerializeField] private int _indexCollection;

    private BlackBack _blackBack;
    private Button _button;
    private bool _isOpen;
    private Sprite _radySprite;

    private void Awake()
    {
        _blackBack = _openObject.GetComponent<BlackBack>();
        _button = GetComponent<Button>();
        _radySprite = _button.image.sprite;
        _button.onClick.AddListener(OnClick);
        closeEven.AddListener(CloseForEvent);
    }

    public void OnClick()
    {
        closeEven.Invoke(this, _indexCollection);
        if (!_isOpen)
        {
            Open();
        }
        else
        {
            Close();
        }
    }
    public void Open()
    {
        if (!_isOpen)
        {
            _isOpen = true;
            _openObject.SetActive(true);
            _button.image.sprite = _closeSprite;
            closeEven.Invoke(this,_indexCollection);
        }
    }
    public void Close()
    {
        if (_isOpen)
        {
            _isOpen = false;
            _blackBack.Close();
            _button.image.sprite = _radySprite;
            closeEven.Invoke(this, _indexCollection);
        }
    }

    private void CloseForEvent(MultiCloser closer, int index)
    {
        if (closer!= this && index == _indexCollection)
        {
            Close();
        }
    }
    public void SetCloseStatus()
    {
        _isOpen = false;
        _button.image.sprite = _radySprite;
    }
}
