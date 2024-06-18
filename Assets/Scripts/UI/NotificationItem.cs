using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationItem : MonoBehaviour
{
    public NotificationItem[] _perentNotificaions;

    [SerializeField] private Image _otherNotification;
    private List<NotificationItem> _childs = new List<NotificationItem>();

    public void On()
    {
        for (int i = 0; i < _perentNotificaions.Length; i++)
        {
            _perentNotificaions[i].On();
            _perentNotificaions[i].AddChild(this, true);
        }
        gameObject.SetActive(true);
        if (_otherNotification)
        {
            _otherNotification.enabled = false;
        }
    }
    public void Off()
    {          
        if(_childs.Count == 0)
        {
            for (int i = 0; i < _perentNotificaions.Length; i++)
            {
                _perentNotificaions[i].AddChild(this, false);
                _perentNotificaions[i].Off();
            }
            gameObject.SetActive(false);
        }
        if (_otherNotification)
        {
            _otherNotification.enabled = true;
        }

    }
    public void SetNotification(bool isOn)
    {
        if (isOn)
        {
            On();
        }
        else
        {
            Off();
        }
    }
    public void AddChild(NotificationItem child, bool isAdd)
    {
        if (isAdd)
        {
           if( !_childs.Contains(child))
            {
                _childs.Add(child);
            }
        }
        else
        {
            _childs.Remove(child);
        }
    }
}
