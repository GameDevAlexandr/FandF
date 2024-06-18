
using UnityEngine;
using UnityEngine.UI;
using static CalculationData;
using static EnumsData;
using static GeneralData;
public class FlyBoxBanner : MonoBehaviour
{
    [HideInInspector] public bool isRepeat;
    [SerializeField] private Button _getRwButton;
    [SerializeField] private ApplovinAD _adManager;
    [SerializeField] private string _adID;
    [SerializeField] private CurrencyType _rwType;
    [SerializeField] private int _count;
    [SerializeField] private Text _countText;
    [SerializeField] private Button _closeButton;
    private void Awake()
    {
        _getRwButton.onClick.AddListener(WatchAD);
        _adManager.completeEvent.AddListener(GetReward);
    }
    private void OnEnable()
    {

        _closeButton.gameObject.SetActive(false);
        Invoke("ShowCloseButton", 3);
        if(_rwType == CurrencyType.coin)
        {
            _count = LootBoxCoins();            
        }
        else
        {
            _count = 20;
        }


    }
    private void WatchAD()
    {
        _adManager.ShowReward(_adID);
    }

    private void GetReward(string adID)
    {
        if (adID == _adID)
        {
            if (_rwType == CurrencyType.coin)
            {
               EventManager.AddCurrency(CurrencyType.coin,LootBoxCoins());
                flyBoxes[1]--;
            }
            else
            {
                EventManager.AddCurrency(CurrencyType.gem, 20);
                flyBoxes[0]--;
            }
        }
        isRepeat = false;
        _closeButton.onClick.Invoke();
    }
    private void ShowCloseButton()
    {
        _closeButton.gameObject.SetActive(true);
    }
}
