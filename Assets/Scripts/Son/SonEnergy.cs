using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

public class SonEnergy : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private Text _countText;
    [SerializeField] private Text _timeText;
    [SerializeField] private Text _energyForPointText;
    [SerializeField] private Button _runButton;
    [SerializeField] private int _maxEnergy;
    [SerializeField] private int _minutePrice;
    [SerializeField] private int _recovery;
    [SerializeField] private DateTimeManager _timeManager;
    [SerializeField] private GameObject _energyAdRecoveryButton;

    private int _secCounter;

    public void Init()
    {
        
        _timeManager.setTimeOffline.AddListener(AddEnergy);
        _timeManager.everySecondEvent.AddListener(()=>AddEnergy(1));
        _timeManager.dayInGameEvent.AddListener(FillEnergy);
        _runButton.onClick.AddListener(RunClick);
    }

    public void RunClick()
    {
        sonData.energy -= EnergyForPoint();
        sonData.secondsToCompany = 0;
        SetInterctable();
    }

    private void SetInterctable()
    {
        _runButton.gameObject.SetActive(sonData.secondsToCompany > 0);
        _runButton.interactable = sonData.energy >= EnergyForPoint();
        _countText.text = sonData.energy +"/"+_maxEnergy;
        _progressBar.fillAmount = (float)sonData.energy / _maxEnergy;
        _timeText.text = sonData.secondsToCompany>0? MyString.GetMS(sonData.secondsToCompany):"";
    }

    private int EnergyForPoint()
    {
       return sonData.secondsToCompany / (60/_minutePrice);
    }

    public void AddEnergy(int sec)
    {
        if (sonData.energy < _maxEnergy)
        {
            _secCounter += sec;
            while (_secCounter > _recovery && sonData.energy<100)
            {
                sonData.energy++;
                _secCounter -= _recovery;
            }
        }
        else
        {
            _secCounter = 0;
        }
        _energyAdRecoveryButton.SetActive(sonData.energy < 50);
        SetInterctable();
        _energyForPointText.text = EnergyForPoint().ToString();
    }
    public void FillEnergy()
    {
        sonData.energy = _maxEnergy;
    }
}
