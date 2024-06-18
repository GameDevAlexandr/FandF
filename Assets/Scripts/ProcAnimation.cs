using UnityEngine;
using UnityEngine.Events;

public class ProcAnimation : MonoBehaviour
{
    public UnityEvent EndAnimation = new UnityEvent();

    private Vector2 _startPosition;
    private Vector2 _endPositon;
    private float _shotHeight;
    private float _speedShot;
    private float _progress =1;
    public void BalisticShot(Vector2 startPosition, Vector2 endPosition, float shotHeight, float speedShot)
    {       
        _startPosition = startPosition;
        _endPositon = endPosition;
        float dist = Vector2.Distance(_startPosition, _endPositon);
        _shotHeight = dist * shotHeight;
        _speedShot = speedShot / dist;
        _progress = 0;
    }
    
    public void ClearShot(Vector2 startPosition, Vector2 endPosition, float speed)
    {
        BalisticShot(startPosition, endPosition, 0, speed);
    }

    private void Update()
    {
        if (_progress<1)
        {
            ShotProcedure();
        }
    }

    public void ShotProcedure()
    {
        _progress += _speedShot * Time.deltaTime;
        Vector2 currentPos = Vector2.Lerp(_startPosition, _endPositon, _progress);
        Vector2 heightOffset = Vector2.up * Mathf.Sin(_progress*Mathf.PI) * _shotHeight;
        transform.position = currentPos + heightOffset;
        if (_progress >= 1)
        {
            EndAnimation.Invoke();
        }
    }
}
