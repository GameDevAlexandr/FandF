using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class FlyingBox : MonoBehaviour
{
    [HideInInspector] public bool isRepeat=>_banner.isRepeat;
    [SerializeField] private FlyBoxBanner _banner;
    [SerializeField] private ProcAnimation _animation;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _amplitude;
    [SerializeField] private SpriteRenderer _sprite;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        Controll.getOutUiPositionEvent.AddListener(OnClick);
        _animation.EndAnimation.AddListener(() => { gameObject.SetActive(false); });
    }

    // Update is called once per frame
    public void StartFly()
    {
        _banner.isRepeat = true;
        gameObject.SetActive(true);
        _animation.BalisticShot(_startPosition.position, _endPosition.position, _amplitude, _speed);
    }
    private void OnClick(Vector2 position)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(position);
        if (_sprite.bounds.Contains(pos))
        {
            _banner.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        transform.position = _startPosition.position;
    }
}
