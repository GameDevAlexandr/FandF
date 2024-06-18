
using UnityEngine;
using Cinemachine;
public class CameraSwipeMoved : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Transform _mapTransform;
    [SerializeField] private SpriteRenderer _boundsSprite;

    private Vector2 _startPos;
    private Vector2 _mapPos;
    private Transform _folowing;
    private Bounds _fixBound;

    private void Awake()
    {
        _fixBound = _boundsSprite.bounds;
    }
    private void OnEnable()
    {
        if (_camera.Follow == null)
        {
             _camera.Follow = _folowing;
        }
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (_camera.Follow != null)
            {
                _folowing = _camera.Follow;
                _camera.Follow = null;
            }
                var t = Input.GetTouch(0);
            Vector2 endPos = Vector2.zero;
            if(t.phase == TouchPhase.Began)
            {
                //_startPos = _mapTransform.position;
                _startPos = Camera.main.ScreenToWorldPoint(t.position);
                _mapPos = _mapTransform.position;
            }
            if(t.phase == TouchPhase.Moved)
            {
                endPos = Camera.main.ScreenToWorldPoint(t.position);
                var newPos = endPos- _startPos;
                newPos = _mapPos + newPos;
                if (_fixBound.Contains(newPos))
                {
                    _mapTransform.position = newPos;
                }
            }
        }
    }
}
