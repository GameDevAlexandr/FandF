using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Controll : MonoBehaviour
{
    public static UnityEvent clickEven = new UnityEvent();
    public static UnityEvent clickAnyPlaceEvent = new UnityEvent();
    public static UnityEvent<Vector2> getPositionEvent = new UnityEvent<Vector2>();
    public static UnityEvent<Vector2> getOutUiPositionEvent = new UnityEvent<Vector2>();
    public static UnityEvent<Vector2> getPositionWorldEvent = new UnityEvent<Vector2>();
    public static UnityEvent<Vector2> getSwipeOutUIPositonEvent = new UnityEvent<Vector2>();
    private void Update()
    {
        Vector2 point = Vector2.zero;
        if (Input.touchCount>0&& Input.GetTouch(0).phase == TouchPhase.Began)
        {
             point = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)&&Time.timeScale!=0)
            {
                clickEven.Invoke();                
                getOutUiPositionEvent.Invoke(point);
            }
            clickAnyPlaceEvent.Invoke();
            getPositionEvent.Invoke(Input.touches[0].position);
            getPositionWorldEvent.Invoke(point);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            { 
                Vector2 cur = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                getSwipeOutUIPositonEvent.Invoke(cur-point);
            }
        }
    }
}
