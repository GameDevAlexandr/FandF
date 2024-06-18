using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static CalculationData;
using static GeneralData;

public class Cooking : MonoBehaviour
{
    [SerializeField] private SmeltQueue _smelt;
    [SerializeField] private ForgeQueue _forge;
    [SerializeField] private SpriteRenderer _smeltArea;
    [SerializeField] private SpriteRenderer _forgeArea;
    [SerializeField] private Image _energyBar;
    [SerializeField] private Text _energyCountText;
    [SerializeField] private GameObject _recoveryADEnergyButton;
    [SerializeField] private DateTimeManager _timeManager;
    [SerializeField] private int _energyCount;
    [SerializeField] private int _secondsForAdd;
    [SerializeField] private float _autoCraftDelay;
    [SerializeField] private int _energyFilledSpeed;
    [SerializeField] private Animator _smithAnimation;
    [SerializeField] private ParticleSystem _sparks;

    private Bounds _areaBound;
    private Bounds _forgeBound;
    private int _secondsCounter;
    private void Awake()
    {
        Controll.getOutUiPositionEvent.AddListener(AddSmelting);
        _areaBound = _smeltArea.bounds;
        _forgeBound = _forgeArea.bounds;
        _forge.Init();
        _smelt.Init();
        StartCoroutine(AutoCraft());
        gameMode = EnumsData.GameMode.smythy;
    }
    public void EventInit()
    {
        _timeManager.everySecondEvent.AddListener(() => AddTime(1));
        _forge.Init();
        _smelt.Init();
        _timeManager.setTimeOffline.AddListener(AddTime);
        _timeManager.dayInGameEvent.AddListener(FillEnergy);
    }
    public void AddSmelting(Vector2 position)
    {
        if (smithData.energy > 0 && gameMode == EnumsData.GameMode.smythy)
        {
            if (_areaBound.Contains(position) && smeltQueue.Count>0)
            {
                _smelt.AddProgress(SmeltPower());
                smithData.energy--;
                _smithAnimation.SetTrigger("Knock");
                _sparks.Play();
            }
            if (_forgeBound.Contains(position) && forgeQueue.Count > 0)
            {
                _forge.AddProgress(ForgePower());
                smithData.energy--;
                _smithAnimation.SetTrigger("Knock");
                _sparks.Play();
            }           
            SetEnergyUI();
        }
    }

    public void AddEnergy(int percent)
    {
        smithData.energy = Mathf.Min(_energyCount, smithData.energy + _energyCount / 100 * percent);
        SetEnergyUI();
    }
    private IEnumerator AutoCraft()
    {
        yield return new WaitForSeconds(_autoCraftDelay);
        _smelt.AddProgress(SmeltPower());
        _forge.AddProgress(ForgePower());
        if(smeltQueue.Count > 0|| forgeQueue.Count > 0)
        {
            _smithAnimation.SetTrigger("Knock");
            _sparks.Play();
        }
        StartCoroutine(AutoCraft());
    }
    
    private void AddTime(int seconds)
    {
        
        if (seconds >1)
        {
            int craftCount = (int)(seconds / _autoCraftDelay);
            _smelt.AddProgress(SmeltPower() * craftCount);
            _forge.AddProgress(ForgePower() * craftCount);
        }
        while (seconds > 0 && smithData.energy<_energyCount)
        {
            _secondsCounter++;
            seconds--;
            if (_secondsCounter >= _energyFilledSpeed)
            {
                smithData.energy++;
                SetEnergyUI();
                _secondsCounter = 0;
            }
        }

    }
    public void FillEnergy()
    {
        smithData.energy = _energyCount;
    }
    private void SetEnergyUI()
    {
        _energyBar.fillAmount = (float)smithData.energy / _energyCount;
        _energyCountText.text = smithData.energy + "/" + _energyCount;
        _recoveryADEnergyButton.SetActive(smithData.energy < _energyCount / 2);
    }
}
