using UnityEngine;
using static GeneralData;

public class TravelPoint : MonoBehaviour
{
    [SerializeField] private DateTimeManager _timer;
    [SerializeField] private GameObject _sonMark;
    [SerializeField] private CompanyManager _manager;
    void Start()
    {
        _timer.setTimeOffline.AddListener(SetTime);
        _timer.everySecondEvent.AddListener(() => SetTime(1));
    }
    private void SetTime(int seconds)
    {
        Vector2 start = _manager.pointBase[sonData.startCompanyIndex].transform.position;
        Vector2 end = _manager.pointBase[sonData.companyIndex].transform.position;
        int timeTo = Mathf.Max(1, _manager.pointBase[sonData.companyIndex].secondsTo);
        sonData.secondsToCompany = Mathf.Max(0, sonData.secondsToCompany -= seconds);

        if(sonData.secondsToCompany == 0)
        {
            sonData.startCompanyIndex = sonData.companyIndex;
        }
        _sonMark.transform.position = Vector2.Lerp(end, start, (float)sonData.secondsToCompany / timeTo);
    }
}
