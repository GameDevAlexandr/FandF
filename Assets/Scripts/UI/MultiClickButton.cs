
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class MultiClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _delay;
    [SerializeField] private float _rise;
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    [SerializeField] private bool _vibrate = true;
    private bool _isDown;
    private Button _thisButton;
    private float _curRise;
    private float _curDelay;
    private float _curSpeed;

    private void Awake()
    {
        _thisButton = GetComponent<Button>();
        _thisButton.onClick.AddListener(Click);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _isDown = true;
        _curRise = _rise;
        _curDelay = _delay;
        _curSpeed = _maxDelay;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDown = false;
        
    }

    private void Update()
    {
        if (_isDown)
        {
            if (_curSpeed > _minDelay)
            {
                _curRise -= Time.deltaTime;
            }

                _curDelay -= Time.deltaTime;
            if (_curDelay <= 0)
            {
                _curSpeed -= Time.deltaTime;
                if (_curSpeed <= 0)
                {                    
                    if (_thisButton.interactable)
                    {
                        _thisButton.onClick.Invoke();
                    }
                    if (_curSpeed > _minDelay)
                    {
                        _curSpeed = _maxDelay - 1f / _curRise;
                    }
                    else
                    {
                        _curSpeed = _minDelay;

                    }
                }
            }
        }
    }
    private void Click()
    {
        if (_vibrate)
        {
            //Sounds.chooseSound.StartVibro();
        }
    }
    private void OnDisable()
    {
        _isDown = false;
    }
}
