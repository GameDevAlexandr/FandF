using UnityEngine;
using UnityEngine.UI;
using static GeneralData;
using static EnumsData;

public class DailyQuestItem : MonoBehaviour
{
    [HideInInspector] public QuestType qType;
    [HideInInspector] public DailyQuest dailyQuestManager;

    [SerializeField] Text _descriptionText;
    [SerializeField] Image _progressBar;
    [SerializeField] Text _rwCountText;
    [SerializeField] NotificationItem _notification;
    [SerializeField] Button _getRwButton;
    [SerializeField] Text _completeText;
    [SerializeField] GameObject _complete;
    [SerializeField] Image _back;
    [SerializeField] Sprite _completeBack;
    [SerializeField] Sprite _inProgressBack;
    [SerializeField] Sprite _adCompleteBack;
    [SerializeField] ApplovinAD _adManager;
    [SerializeField] Sprite _adButtonSprite;
    [SerializeField] GameObject _adNotif;
    [SerializeField] GameObject _freeNotif;

    private int _reward;
    private string _description;
    private bool _adReward;
    private string _adIndex;
    private Sprite _buttonSprite;
    private int _index;
    private int _maxCount;
    private void Start()
    {
        _adManager.completeEvent.AddListener(CompleteAd);
    }
    public void SetData(QuestType type, string description, int maxCount, int reward, bool adReward, int itemIndex)
    {
        _index = itemIndex;
        _maxCount = maxCount;
        _adIndex = "Dayly_Quest_" + type +_index;
        _getRwButton.interactable = false;
        if (!_buttonSprite)
        {
            _buttonSprite = _getRwButton.image.sprite;
        }
        SetComplete(false);
        qType = type;
        _description = description;
        _reward = reward;
        _adReward = adReward;
        _getRwButton.onClick.RemoveAllListeners();
        if (adReward)
        {
            _getRwButton.onClick.AddListener(GetAdReward);
            _getRwButton.image.sprite = _adButtonSprite;
        }
        else
        {
            _getRwButton.onClick.AddListener(GetReward);
        }
        _adNotif.SetActive(adReward);
        _freeNotif.SetActive(!adReward);
        SetData();
    }
    public void SetData()
    {
        if(questData.daily == null || questData.daily.Length ==0)
        {
            return;
        }
        _progressBar.fillAmount = (float)questData.daily[_index].count / _maxCount;
        _rwCountText.text = _reward.ToString();
        if (_maxCount > 1 && questData.daily[_index].count <= _maxCount)
        {
            _descriptionText.text = _description + " (" + questData.daily[_index].count + "/" + _maxCount + ")";
        }
        else
        {
            _descriptionText.text = _description;
        }
        if (_adReward)
        {
            _descriptionText.text = "Bonus Quest: " + _descriptionText.text;
        }
        if (questData.daily[_index].count >= _maxCount && !questData.daily[_index].complete)
        {
            SetComplete(true);
            transform.SetAsFirstSibling();
        }
        else
        {
            SetComplete(false);
        }
        if (questData.daily[_index].complete)
        {            
            transform.SetAsLastSibling();
        }
        _complete.SetActive(questData.daily[_index].complete);
    }

    private void SetComplete(bool isComplete)
    {        
        _completeText.enabled = isComplete;
        _notification.SetNotification(isComplete && !questData.daily[_index].complete);
        _getRwButton.interactable = isComplete;
        if (isComplete)
        {
            _back.sprite = !_adReward ? _completeBack : _adCompleteBack;
        }
        else
        {
            _back.sprite = _inProgressBack;
        }
    }

    private void GetAdReward()
    {
        _adManager.ShowReward(_adIndex);
    }
    private void GetReward()
    {
        EventManager.AddCurrency(CurrencyType.gem, _reward);
        _complete.SetActive(true);
        _notification.Off();
        _back.sprite = _inProgressBack;
        questData.daily[_index].complete = true;
        dailyQuestManager.QuestComplete();
        transform.SetAsLastSibling();
        _getRwButton.interactable = false;
        Sounds.chooseSound.ButtonClick(0);
    }
    private void CompleteAd(string adId)
    {
        if (_adIndex == adId)
        {
            GetReward();
        }
    }
}
