using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyText : MonoBehaviour
{
    [HideInInspector] public TextGenerator generator;

    [SerializeField] private Image _icon;
    [SerializeField] private Color _bonusColor;
    [SerializeField] private float _spread;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private Text _text;

    private Vector2 _startPosition;    
    private Vector2 _endPosition;    
    private int _startFontSize;
    private Color _startFontColor;
    private float _progress = 1;
    private void Start()
    {
        _startPosition = transform.position;
        _startFontSize = _text.fontSize;
        _startFontColor = _text.color;
        if (_icon)
        {
            _icon.enabled = false;
        }
    }

    public void StartFly(Vector2 startPosition, string message, bool bonus)
    {
        _startPosition = startPosition;
        _text.fontSize = bonus ? _startFontSize * 2 : _startFontSize;
        _text.color = bonus ? _bonusColor : _startFontColor;
        if (_icon)
        {
            _icon.enabled = true;
        }
        _text.text = message;
        _spread = Random.Range(-_spread, _spread);
        _endPosition = new Vector2(_startPosition.x + _spread, _startPosition.y + _distance);
        _progress = 0;
    } 
    public void StartFly(Vector2 startPosition, string message, bool bonus, Sprite icon)
    {
        _icon.sprite = icon;
        StartFly(startPosition, message, bonus);
    }
    private void Update()
    {
        if (_progress < 1)
        {
            transform.position = Vector2.Lerp(_startPosition, _endPosition, _progress);
            _progress += Time.deltaTime * _speed;
            if (_progress >= 1)
            {
                DisableFlyText();
            }
        }
    }
    private void DisableFlyText()
    {
        if (_icon)
        {
            _icon.enabled = false;
        }
        transform.position = _startPosition;
        _text.text = "";
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1);
        generator.RemoveText(this);
    }
    private void OnEnable()
    {
        DisableFlyText();
    }
}
