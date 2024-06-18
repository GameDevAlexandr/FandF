using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

public class DailyRewardItem : MonoBehaviour
{
    public RewardItem reward => _reward;
    public int rwCount => _count;

    [SerializeField] private RewardItem _reward;
    [SerializeField] private int _count;
    [SerializeField] private Image _rewardIcon;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Image _completeImg;
    [SerializeField] private Text _rwCountText;
    [SerializeField] private Text _dayNumberText;
    [SerializeField] private NotificationItem _notification;
 
    private int _index;
    public void Init(int index)
    {
        _index = index;
        _dayNumberText.text = "DAY " + (index + 1).ToString();
        _rewardIcon.sprite = _reward.icon;
        _rwCountText.text = "x" + _count;
        SetActive(index < dateValue.dayInGame);
        _completeImg.gameObject.SetActive(receivedDailyReward[index]);
    }

    public void SetActive(bool isActive)
    {
        _lockImage.gameObject.SetActive(!isActive);
        _notification.SetNotification(isActive && !receivedDailyReward[_index]);
    }

    public void GetReward()
    {
        _reward.GetReward(_count);
        receivedDailyReward[_index] = true;
        _completeImg.gameObject.SetActive(true);
        _notification.Off();
    }
}
