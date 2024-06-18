using UnityEngine;
using UnityEngine.UI;
using static GeneralData;
using static CalculationData;
using static EnumsData;

public class FightSoulCell : MonoBehaviour
{
    [HideInInspector] bool autoAttack;

    [SerializeField] private FightSoulsManager _manager;
    [SerializeField] private SoulWeapon _weapon;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _speedBar;
    [SerializeField] private Image _energyBar;
    [SerializeField] private Image[] _damagTypeIndicator;
    [SerializeField] private Image[] _hitIndicators;
    [SerializeField] private Text _energyText;
    [SerializeField] private Button _attackButton;

    private int _dataIndex;
    private float _attackTimer;
    private SoulItem _soul;
    private SoulsData sd;
    private int _fullEnergy;
    private int _hitCount;
    private void Awake()
    {
        EventManager.FightStep.AddListener(NextStep);
        _attackButton.onClick.AddListener(Attack);
    }
    public void SetData(int dataIndex)
    {
        _dataIndex = dataIndex;
        var soul = SoulsBase.IndexBase[souls[dataIndex].index];
        _soul = soul;
        _icon.sprite = soul.icon;
        _attackTimer = 1f / soul.speed;
        sd = souls[_dataIndex];
        _fullEnergy = SoulEnergy(_dataIndex);
        _hitCount = _soul.hitCount;
        for (int i = 0; i <_hitIndicators.Length ; i++)
        {
            _hitIndicators[i].transform.parent.gameObject.SetActive(i < _hitCount);
        }
        if (souls[dataIndex].sword.index >= 0)
        {
            _weapon.SetWeapon(ForgeItemBase.Base[(ForgeItemType)sd.sword.type][sd.sword.index].icon);
        }
        else
        {
            _weapon.SetWeapon(null);
        }
        SetUI();        
    }

    private void NextStep(float time)
    {
        if (_attackTimer - time <= 0)
        {
            if (autoAttack)
            {
                Attack();
            }
            _attackTimer = 0;
        }
        else
        {
            _attackTimer -= time;
        }
        SetUI();
    }
    private void Attack()
    {
        _weapon.Attack(_manager.enemies,_manager.targetIndex, Damage(), (int)(Damage()*_soul.otherDmg));
        _hitCount--;
        if (_hitCount <= 0)
        {
            _attackTimer = 1f / _soul.speed;
            _hitCount = _soul.hitCount;
        }
        sd.energy = Mathf.Max(0, sd.energy - SoulEnergyPerHit(_dataIndex));
        souls[_dataIndex] = sd;
        if (sd.energy == 0)
        {
            _manager.ChangeSoul(this);
        }
        SetUI();
    }
    private void SetUI()
    {
        float spd = _attackTimer / (1f / _soul.speed);
        float en = (float)sd.energy / _fullEnergy;
        _energyText.text = souls[_dataIndex].energy + "/" +_fullEnergy;
        _attackButton.interactable = _attackTimer <= 0;
        _speedBar.fillAmount = spd;
        _energyBar.fillAmount = en;
        for (int i = 0; i <_hitIndicators.Length; i++)
        {
            _hitIndicators[i].enabled = i < _hitCount;
        }
    }
    private int Damage()
    {
        float dmg = SoulDamage(_dataIndex);
        int eph = SoulEnergyPerHit(_dataIndex);
        dmg = eph > sd.energy ? dmg * ((float)sd.energy / eph) : dmg;
        dmg = Mathf.Max(5, dmg);
        return (int)dmg;
    }
}
