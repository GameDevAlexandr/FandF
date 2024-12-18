
using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

public class Hole : MonoBehaviour
{
    [SerializeField] private Transform _backTransform;
    [SerializeField] private float _speedAnimation;
    [SerializeField] private float _minHoleScale;
    [SerializeField] private GameObject _finger;
    [SerializeField] private GameObject _backLock;
    [SerializeField] private TutorialHandler _handler;
    [SerializeField] private Sprite _customHole;

    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private Vector2 _backPosition;
    private RectTransform _rt;
    private float _stepCounter;
    private Vector2 newRect;
    private bool _isFinger;
    private bool _isRady;
    private Button _targetButton;
    private TutorialHandler.IterationName _nextIteration;
    private TutorialHandler.tData _tData;
    private Image _holeImg;
    private Sprite _standartHole;
    private Vector2 _targetSize;
    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _holeImg = GetComponent<Image>();
        _standartHole = _holeImg.sprite;
        _startPosition = transform.position;
        _backPosition = _backTransform.position;
        _stepCounter = 1;
        Controll.getPositionEvent.AddListener(ClickInHole);
    }
    public void SetNewHolePosition(TutorialHandler.tData data)
    {
        _tData = data;
        _isRady = false;
        _isFinger = data.finger;
        _nextIteration = data.nextIteration;
        _finger.SetActive(false);
        _endPosition = data.lookObject.gameObject.layer == 5?data.lookObject.position: Camera.main.WorldToScreenPoint(data.lookObject.position);
        _stepCounter = 0;
        transform.position = _startPosition;
        newRect = new Vector2(3000, 3000);
        _rt = GetComponent<RectTransform>();
        _rt.sizeDelta = newRect;
        _finger.SetActive(_isFinger);
        _targetSize = new Vector2(160, 160);
        if (_tData.lookObject.gameObject.layer == 5 && _tData.isCustomHole)
        {
            var rt = _tData.lookObject.GetComponent<RectTransform>();
            _targetSize = new Vector2(rt.rect.size.x + 5, rt.rect.size.y + 5);
        }
        if (_tData.isCustomHole)
        {
            _holeImg.sprite = _customHole;
        }
        else
        {
            _holeImg.sprite = _standartHole;
        }
        if (_targetButton =data.lookObject.GetComponent<Button>())
        {
            return;
        }
        else
        {
            _targetButton = null;
        }
    }

    private void Update()
    {
        if (_stepCounter < 1)
        {
            transform.position = Vector2.Lerp(transform.position, _endPosition, _stepCounter);
            
            newRect = Vector2.Lerp(newRect, _targetSize, _stepCounter);
            _rt.sizeDelta = newRect;
            _backTransform.position = _backPosition;
            _stepCounter += Time.deltaTime * _speedAnimation;
            _finger.transform.position = transform.position;
            if (_stepCounter >= 0.5f)
            {                
                _isRady = true;
            }
        }
    }
    private void ClickInHole(Vector2 position)
    {
        if (_isRady)
        {

            if (_targetButton )
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(_rt, position))
                {
                    _targetButton.onClick.Invoke();                   
                    _targetButton = null;
                    Close();
                }
            }
            else
            {                
                Close();
                if (_tData.finger)
                {
                    Controll.getOutUiPositionEvent.Invoke(Camera.main.ScreenToWorldPoint(position));
                }
            }            
        }
    }
    
    private void Close()
    {
        //_isRady = false;
        //tutorData[_handler.DefineIteration(_tData.iterationName)] = true;
        //_backLock.SetActive(false);
        //_finger.SetActive(false);
        //if (_nextIteration!= TutorialHandler.IterationName.none)
        //{
        //    _handler.SetNewIteration(_nextIteration);
        //}
    }
}
