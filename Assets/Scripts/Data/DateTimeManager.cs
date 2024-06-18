using System;
using UnityEngine;
using UnityEngine.Events;
using static GeneralData;

public class DateTimeManager : MonoBehaviour
{
    public UnityEvent setListnersEvent = new UnityEvent();
    public UnityEvent<int> minutesInGameEvent = new UnityEvent<int>();
    public UnityEvent dayInGameEvent = new UnityEvent();
    public UnityEvent<int> setTimeOffline = new UnityEvent<int>();
    public UnityEvent everySecondEvent = new UnityEvent();
    private float minutesTimer;
    private float secondesTimer;
    private void Awake()
    {
        setListnersEvent.Invoke();
        SetOffLineTime();
        CheckDate();
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time - minutesTimer >= 60)
        {
            minutesTimer = Time.time;
            dateValue.minutesInGame++;
            dateValue.minutesPerDay++;
            minutesInGameEvent.Invoke(dateValue.minutesInGame);
        }
        secondesTimer += Time.deltaTime;
        if(secondesTimer >= 1)
        {
            everySecondEvent.Invoke();
            secondesTimer -=1;
            CheckDate();
        }
    }

    private void CheckDate()
    {
        TimeSpan ts = DateTime.Now - DateTime.MinValue;
        if (dateValue.lastDayInGame == 0)
        {
            dayInGameEvent.Invoke();
            dateValue.lastDayInGame = ts.Days;
            dateValue.itsNewDay = true;
        }
        if (ts.Days - dateValue.lastDayInGame >0)
        {
            dateValue.dayInGame++;
            dateValue.lastDayInGame = ts.Days;
            dateValue.itsNewDay = true;
            dayInGameEvent.Invoke();
        }
       dateValue.lastSecondToGame = ts.TotalSeconds;
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            CheckDate();
        }
        else
        {
            SetOffLineTime();
        }
    }

    private void SetOffLineTime()
    {
        TimeSpan ts = DateTime.Now - DateTime.MinValue;
        if (dateValue.lastSecondToGame!=0)
        {
            int offLineTime = (int)(ts.TotalSeconds - dateValue.lastSecondToGame);
            setTimeOffline.Invoke(offLineTime);
        }
    }
}
