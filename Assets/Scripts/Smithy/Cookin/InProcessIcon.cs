using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InProcessIcon : MonoBehaviour
{
    public bool onActive { get; private set;}

    [SerializeField] private Image _icon;
    [SerializeField] private Image _thisImage;
    [SerializeField] private Image _progresBar;
    [SerializeField] private Text _progresText;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private float _endScale;
    [SerializeField] private float _moveSpeed;

    private float _progress = 1;
    private Image _radyIcon;
    
    private Vector2 _endScl => new Vector2(_endScale, _endScale);
    public void SetItem(Sprite icon)
    {

        Activate(true);
        _icon.sprite = icon;
    }
    public void SetData(float progress)
    {
        _progresBar.fillAmount = progress;
        if (progress >= 1)
        {
            Finish();
        }
        _progresText.text = "";//currnt + "/" + max;
    }
    
    private void Finish()
    {
        _icon.enabled = false;
        if (!_radyIcon)
        {
            _radyIcon = Instantiate(_icon, transform.position, Quaternion.identity, transform);
        }
            _progress = 0;
        Activate(false);
    }

    public void Activate(bool isActive)
    {
        onActive = isActive;
        gameObject.SetActive(isActive);
        _icon.enabled = isActive;
        _progresBar.transform.parent.gameObject.SetActive(isActive);
    }
    private void Update()
    {
        if (_progress < 1)
        {
            _progress += Time.deltaTime*_moveSpeed;
            _radyIcon.transform.position = Vector2.Lerp(transform.position, _endPosition.position, _progress);
            _radyIcon.transform.localScale = Vector2.Lerp(Vector2.one, _endScl, _progress);
            if (_progress >= 1)
            {
                Destroy(_radyIcon.gameObject);
            }
        }
    }
}
