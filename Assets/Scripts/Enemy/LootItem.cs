using UnityEngine;

public class LootItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private ProcAnimation _animation;
    [SerializeField] private float _height;
    [SerializeField] private float _speed;
    //[SerializeField] private 

    private Vector2 _startPosition;
    private Vector2 _endPositon;
    private Vector2 _inventPosition;
    private CurrencyItem _item;
    private int _count;
    private SonBag _bag;
    private void OnClick(Vector2 position)
    {
        if (_sprite.bounds.Contains(position))    
        {            
            MoveItem();
        }
    }
    private void MoveItem()
    {
        _animation.EndAnimation.AddListener(TakeReward);
        _startPosition = transform.position;
        _animation.ClearShot(_startPosition, _inventPosition, _speed*2);
    }
    public void Drop(Vector2 start, Vector2 end, SonBag invent, CurrencyItem item, int count)
    {
        EventManager.LostCompany.AddListener(TakeReward);
        EventManager.WinCompany.AddListener(TakeReward);
        _bag = invent;
        _startPosition = start;
        _endPositon = end;
        _inventPosition = Camera.main.ScreenToWorldPoint(invent.transform.position);
        _item = item;
        _count = count;
        _sprite.sprite = item.icon;
        Controll.getOutUiPositionEvent.AddListener(OnClick);
        _animation.BalisticShot(_startPosition, _endPositon, _height, _speed);
        Invoke("MoveItem", 5);
    }

    private void TakeReward()
    {
        EventManager.AddCurrency(_item.type, _count);
        _bag.PutToBag(_item.type, _count);
        Destroy(gameObject);
    }
}
