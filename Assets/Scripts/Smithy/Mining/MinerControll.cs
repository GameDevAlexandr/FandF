using UnityEngine;
using UnityEngine.Events;

public class MinerControll : MonoBehaviour
{
    public UnityEvent<int, int> MoveEvent = new UnityEvent<int, int>();  

    [SerializeField] private SpriteRenderer _right;
    [SerializeField] private SpriteRenderer _left;
    [SerializeField] private SpriteRenderer _up;
    [SerializeField] private SpriteRenderer _down;

    private void Awake()
    {
        Controll.getOutUiPositionEvent.AddListener(ClickMoveArea);
    }

    private void ClickMoveArea(Vector2 area)
    {
        if (_right.bounds.Contains(area))
        {
            MoveEvent.Invoke(1, 0);
        }
        else if(_left.bounds.Contains(area))
        {
            MoveEvent.Invoke(-1, 0);
        }
        else if (_up.bounds.Contains(area))
        {
            MoveEvent.Invoke(0, 1);
        }
        else if (_down.bounds.Contains(area))
        {
            MoveEvent.Invoke(0, -1);
        }
    }
}
