using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;

public class MineBusterManager : MonoBehaviour
{
    public UnityEvent OpenStoreEvent;

    [SerializeField] private ApplovinAD _adManager;
    [SerializeField] private BoostData[] _data;
    [SerializeField] private Button _startMineButton;
    [SerializeField] private Button _startMineAdButton;
    [SerializeField] private Image _keyImg;
    [SerializeField] private Text _keyCountText;
    [SerializeField] private MiningManager _mineManager;
    [SerializeField] private DateTimeManager _timeManager;
    [SerializeField] private int _keyRecoverySeconds;
    [SerializeField] private Text _keyRecoveryTimeText;
    [SerializeField] private int _gemPrice;
    [SerializeField] private Button _addKeyButton;
    [SerializeField] private NotificationItem _notification;
    
    private bool _useFree = false;
    private const string _ADID = "Use_Mine_Key";

    [System.Serializable]
    public struct BoostData
    {
        public Button freeButton;
        public Button adButton;
        public Button gemButton;
        public MineBoostType type;
        public string adID;
    }

    public void Init()
    {
        if (mining.booseters == null)
        {
            mining.booseters = new bool[System.Enum.GetValues(typeof(MineBoostType)).Length];
        }
        _adManager.completeEvent.AddListener(AdReward);
        EventManager.ChangeCurrency.AddListener(SetMineButtonStatus);
        EventManager.ChangeCurrency.AddListener(SetInteractableGemButton);
        _startMineAdButton.onClick.AddListener(WachAd);
        _startMineButton.onClick.AddListener(StartMining);
        _timeManager.dayInGameEvent.AddListener(EveryDayReward);
        _timeManager.everySecondEvent.AddListener(() => SetKeyRecoveryTime(1));
        _timeManager.setTimeOffline.AddListener(SetKeyRecoveryTime);
        _addKeyButton.onClick.AddListener(() => OpenStoreEvent.Invoke());
        
        for (int i = 0; i < _data.Length; i++)
        {
            if (mining.booseters[i])
            {
                _useFree = true;
                break;
            }
        }

        for (int i = 0; i < _data.Length; i++)
        {
            var type = _data[i].type;
            var id = _data[i].adID;
            _data[i].freeButton.onClick.AddListener(() => OnFree(type));
            _data[i].adButton.onClick.AddListener(() => OnAd(id));
            _data[i].gemButton.onClick.AddListener(() => OnForGems(type));
            _data[i].freeButton.gameObject.SetActive(!_useFree);
            _data[i].adButton.gameObject.SetActive(_useFree && !mining.booseters[(int)_data[i].type]);
        }
        SetButtonStatus();
        SetMineButtonStatus(CurrencyType.mineKey, 0);
        SetMineButtonStatus(CurrencyType.adMineKey, 0);
    }
    private void OnFree(MineBoostType type)
    {
        mining.booseters[(int)type] = true;
        _useFree = true;
        SetButtonStatus();
    }
    private void OnAd(string adId)
    {
        _adManager.ShowReward(adId);
    }

    private void OnForGems(MineBoostType type)
    {
        EventManager.AddCurrency(CurrencyType.miningCupon, -_gemPrice);
        mining.booseters[(int)type] = true;
        SetButtonStatus();
    }
    private void AdReward(string adId)
    {
        AdCopmlete(adId);
        for (int i = 0; i < _data.Length; i++)
        {
            if(_data[i].adID == adId)
            {
                OnFree(_data[i].type);
                break;
            }
        }
    }
    private void SetButtonStatus()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            _data[i].freeButton.gameObject.SetActive(!_useFree);
            _data[i].adButton.gameObject.SetActive(_useFree && !mining.booseters[(int)_data[i].type]);
            _data[i].gemButton.gameObject.SetActive(_useFree && !mining.booseters[(int)_data[i].type]);
        }
        SetInteractableGemButton(CurrencyType.miningCupon, 0);
    }
    private void SetInteractableGemButton(CurrencyType type, int count)
    {
        if(type!= CurrencyType.miningCupon)
        {
            return;
        }
        bool mc = currency[(int)CurrencyType.miningCupon] > 0;
        for (int i = 0; i < _data.Length; i++)
        {            
            _data[i].gemButton.onClick.RemoveAllListeners();
            int idx = i;
            _data[i].gemButton.onClick.AddListener( mc? () => OnForGems(_data[idx].type) : () => OpenStoreEvent.Invoke());
        }
    }
    public void ResetBoost()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            _data[i].freeButton.gameObject.SetActive(true);
            _data[i].adButton.gameObject.SetActive(false);
            _data[i].gemButton.gameObject.SetActive(false);
            mining.booseters[i] = false;
        }
        _useFree = false;
    }

    private void SetMineButtonStatus(CurrencyType type, int count)
    {
        int key = currency[(int)CurrencyType.mineKey];
        int adKey = currency[(int)CurrencyType.adMineKey];
        if (type == CurrencyType.mineKey)
        {
            _startMineButton.gameObject.SetActive(key > 0);
            _addKeyButton.gameObject.SetActive(key <= 0);
        }
        if(type == CurrencyType.adMineKey)
        {
            _startMineAdButton.interactable = adKey > 0;            
        }
        _notification.SetNotification(key > 0);
        _keyCountText.text = key > 0 ? key + "/5":adKey+"/5";
        _keyImg.sprite = key > 0 ? CurrencyBase.Base[CurrencyType.mineKey].icon : CurrencyBase.Base[CurrencyType.adMineKey].icon;
    }
    private void WachAd()
    {
        _adManager.ShowReward(_ADID);
    }
    private void AdCopmlete(string adID)
    {
        if(adID == _ADID)
        {
            EventManager.AddCurrency(CurrencyType.adMineKey, -1);
            EventManager.AddCurrency(CurrencyType.mineKey, 1);
        }
    }
    private void StartMining()
    {
        EventManager.AddCurrency(CurrencyType.mineKey, -1);
        if (currency[(int)CurrencyType.mineKey] == 0)
        {
            SetMineButtonStatus(CurrencyType.adMineKey, 0);
        }
        _mineManager.StartMinig();
    }

    private void SetKeyRecoveryTime(int seconds)
    {
        while (currency[(int)CurrencyType.mineKey] < 5 && seconds>0)
        {
            mining.timeRecovery += seconds;
            if (mining.timeRecovery >= _keyRecoverySeconds)
            {
                EventManager.AddCurrency(CurrencyType.mineKey, 1);
                mining.timeRecovery = 0;
                
            }
            seconds -= _keyRecoverySeconds;
        }
        _keyRecoveryTimeText.gameObject.SetActive(currency[(int)CurrencyType.mineKey] < 5);
        _keyRecoveryTimeText.text = MyString.GetMS(_keyRecoverySeconds - mining.timeRecovery);
    }
    private void EveryDayReward()
    {
        currency[(int)CurrencyType.mineKey] = Mathf.Max(5, currency[(int)CurrencyType.mineKey]);
        currency[(int)CurrencyType.adMineKey] = 5;
        EventManager.AddCurrency(CurrencyType.mineKey, 0);
    }
}
