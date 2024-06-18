using UnityEngine;
using UnityEngine.UI;

public class PowerTestGrade : MonoBehaviour
{
    public RewardItem reward => _reward;
    public int rwCount => _count;         
    
    [SerializeField] Image _rewardIcon;
    [SerializeField] Image _completeImg;
    [SerializeField] Image _progressBar;
    [SerializeField] Text _rwCountText;
    [SerializeField] Text _needPower;
    private RewardItem _reward;
    private int _count;
    public void SetItem(RewardItem reward, int count)
    {
        _reward = reward;
        _count = count;
        _rewardIcon.sprite = _reward.icon;
        _rwCountText.text = "x" + _count;
    }
    public void SetData(float progress)
    {
        _progressBar.fillAmount = progress;
    }
    public void GetReward()
    {
        _reward.GetReward(_count);
        _completeImg.enabled = true;
    }
    public void CompleteGrade()
    {
        _progressBar.fillAmount = 1;
        _completeImg.enabled = true;
    }
    public void SetPowerNeed(int power)
    {
        _needPower.text = power.ToString();
    }

}
