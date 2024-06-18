using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _helth;
    [SerializeField] private Slider _damage;
    [SerializeField] private float _speed;

    private float _dmgProgress = 1;
    private float _hlgProgress = 1;
    
    public void TakeDamage(float value)
    {
        _helth.value = value;
        _dmgProgress = 0;
    }

    public void Healing(float value)
    {
        _damage.value = value;
        _hlgProgress = 0;
    }
    public void Fill()
    {
        _helth.value = 1;
    }
    public void Empty()
    {
        _helth.value = 0;
    }
    private void Update()
    {
        if(_dmgProgress < 1)
        {
            _damage.value = Mathf.Lerp(_damage.value, _helth.value, _dmgProgress);
            _dmgProgress += Time.deltaTime * _speed;
        }
        if (_hlgProgress < 1)
        {
            _helth.value = Mathf.Lerp(_helth.value, _damage.value, _hlgProgress);
            _hlgProgress += Time.deltaTime * _speed;
        }
    }
}
