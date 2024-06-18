using UnityEngine;

public class BlackBack : MonoBehaviour
{
    [SerializeField] private Transform _blackBack;
    private Transform _parent;

    private bool _isOpen;
    private void Awake()
    {
        _parent = transform.parent;
    }
    private void OnEnable()
    {
        _isOpen = true;
        _blackBack.gameObject.SetActive(true);
        transform.parent = _blackBack;
    }
    public void Open()
    {
        gameObject.SetActive(true);
        _isOpen = true;
        _blackBack.gameObject.SetActive(true);
        transform.parent = _blackBack;
    }

    public void Close()
    {
        if (_isOpen)
        {
            transform.parent = _parent;
            _blackBack.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}

