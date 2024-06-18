using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;

public class ADRecoveryItem : MonoBehaviour
{
    public UnityEvent<int> CompleteEvent;
    public UnityEvent OpenStoreEvent;

    [SerializeField] private ApplovinAD _adManager;
    [SerializeField] private DateTimeManager _timeManager;
    [SerializeField] private AdRecType _type;
    [SerializeField] private Button _wachButton;
    [SerializeField] private Button _buyGemButton;
    [SerializeField] private int _adCount;
    [SerializeField] private Text _adCountText;
    [SerializeField] private string _adID;
    [SerializeField] private int _gemPrice;


    public void Init()
    {
        _timeManager.dayInGameEvent.AddListener(Recovery);
        _adManager.completeEvent.AddListener(CompleteAd);
        _wachButton.onClick.AddListener(WachAd);
        _buyGemButton.onClick.AddListener(BuyForGems);
        EventManager.ChangeCurrency.AddListener(SetInteractableGemButton);
        SetUI();
    }

    private void WachAd()
    {
        _adManager.ShowReward(_adID);
    }
    private void CompleteAd(string adID)
    {
        if(_adID == adID)
        {
            adRecovery[(int)_type]--;            
            CompleteEvent.Invoke(50);
            SetUI();
            gameObject.SetActive(false);
            SetUI();
        }
    }
    private void BuyForGems()
    {
        EventManager.AddCurrency(CurrencyType.energyPotion, -_gemPrice);
        CompleteEvent.Invoke(15);
        SetUI();
    }
    private void SetUI()
    {      
        _wachButton.interactable = adRecovery[(int)_type] > 0 && !isFull();
        _adCountText.text = adRecovery[(int)_type] + "/" + _adCount;
        SetInteractableGemButton(CurrencyType.energyPotion,0);
    }
    private void SetInteractableGemButton(CurrencyType type, int count)
    {
        if(type!= CurrencyType.energyPotion)
        {
            return;
        }
        bool ep = currency[(int)CurrencyType.energyPotion] > 0;
        _buyGemButton.onClick.RemoveAllListeners();
        _buyGemButton.onClick.AddListener(ep ? BuyForGems : () => OpenStoreEvent.Invoke());
    }
    private void Recovery()
    {
        adRecovery[(int)_type] = _adCount;
        SetUI();
    }
    private bool isFull()
    {
        switch (_type)
        {
            case AdRecType.sonEnergy:
                return sonData.energy>=100;
            case AdRecType.smithEnergy:
                return smithData.energy >= 300;
            default: return sonData.hp >= CalculationData.GetSonHealth();
        }
    }
    private void OnEnable()
    {
        SetUI();
    }
}
