
using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

namespace Tutorial {
    public class Hole : MonoBehaviour
    {
        [SerializeField] private GameObject _back;
        [SerializeField] private Transform _backTransform;
        [SerializeField] private float _speedAnimation;
        [SerializeField] private GameObject _finger;
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
        private void Init()
        {
            _rt = GetComponent<RectTransform>();
            _holeImg = GetComponent<Image>();
            _standartHole = _holeImg.sprite;
            _startPosition = transform.position;
            _backPosition = _backTransform.position;
            _backTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
            _stepCounter = 1;
            Controll.getPositionEvent.AddListener(ClickInHole);
        }
        public void SetNewHolePosition(TutorialHandler.tData data)
        {
            if (!_holeImg)
            {
                Init();
            }
            Time.timeScale = 0;
            _back.SetActive(true);
            _tData = data;
            _isRady = false;
            _isFinger = data.finger;
            _finger.SetActive(false);
            _endPosition = data.lookObject.gameObject.layer == 5 ? data.lookObject.position : Camera.main.WorldToScreenPoint(data.lookObject.position);
            _stepCounter = 0;
            transform.position = _startPosition;
            newRect = new Vector2(3000, 3000);
            _rt = GetComponent<RectTransform>();
            _rt.sizeDelta = newRect;
            _finger.SetActive(_isFinger);
            _targetSize = new Vector2(160, 160);
            if (_tData.isCustomHole)
            {
                var rt = _tData.lookObject.GetComponent<RectTransform>();
                _targetSize = new Vector2(rt.rect.size.x + 5, rt.rect.size.y + 5);
                _holeImg.sprite = _customHole;
            }
            else
            {
                _holeImg.sprite = _standartHole;
            }
            if (_targetButton = data.lookObject.GetComponent<Button>())
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
                _stepCounter += Time.unscaledDeltaTime * _speedAnimation;
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
                if (_tData.finger && RectTransformUtility.RectangleContainsScreenPoint(_rt, position))
                {
                    bool isClosed = false;
                    if (_handler.CheckIsComplete())
                    {
                        Close();
                        isClosed = true;
                    }
                    if (_targetButton)
                    {
                        _targetButton.onClick.Invoke();

                    }
                    else
                    {
                        Controll.getOutUiPositionEvent.Invoke(Camera.main.ScreenToWorldPoint(position));
                    }
                    if (!isClosed)
                    {
                        Close();
                    }
                }
                else if (!_tData.finger)
                {
                    Close();
                }
            }
        }

        private void Close()
        {
            Time.timeScale = 1;
            _isRady = false;
            _finger.SetActive(false);
            _back.SetActive(false);
            _handler.CompleteIteration();
        }
    }
}
