using UnityEngine;
using UnityEngine.UI;
using static GeneralData;
using static CalculationData;
using static EnumsData;
using UnityEngine.Events;

public class SoulInfo : MonoBehaviour
{
    [HideInInspector] public UnityEvent<int> changeRadyEvent = new UnityEvent<int>();
    [HideInInspector] public UnityEvent removeRadyEvent = new UnityEvent();
    [HideInInspector] public UnityEvent changeAmmoEvent = new UnityEvent();
    [HideInInspector] public UnityEvent<int, bool> setEnhanceEvent = new UnityEvent<int, bool>();
    [HideInInspector] public UnityEvent<bool> setEnhanceSelectableEvent = new UnityEvent<bool>();
    public int dataIndex => _dataIndex; 

    [SerializeField] private SoulsManager _manager;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _name;
    [SerializeField] private Text _rareName;
    [SerializeField] private LevelStars _level;
    [SerializeField] private Text _damage;
    [SerializeField] private Text _hitCount;
    [SerializeField] private Text _otherDamage;
    [SerializeField] private Text _energy;
    [SerializeField] private Text _speed;
    [SerializeField] private Text _enPerHit;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _enhaceButton;
    [SerializeField] private Text _buttonText;
    [SerializeField] private Button _sonButton;
    [SerializeField] private EmptyBack _equipBack;
    [SerializeField] private EnhanceSoulPanel _enhancePanel;
    [SerializeField] private AmmoEquipedCell _armour;
    [SerializeField] private AmmoEquipedCell _weapon;
    [SerializeField] private AmmoEquipedCell _amulet;

    private int _dataIndex;
    private bool _isBasic;
    private SoulItem _item;
    private void Start()
    {
        _enhaceButton.onClick.AddListener(OnEnhance);
    }
    public void SetInfo(int dataIndex)
    {
        gameObject.SetActive(true);        
        _dataIndex = dataIndex;
        int index = souls[dataIndex].index;
        var item = SoulsBase.IndexBase[index];
        _icon.sprite = item.icon;
        _name.text = item.soulName;
        _rareName.text = item.rareName;
        _level.SetLvl(souls[dataIndex].level);
        _item = item;
        SetSoulParam();
        SetequipStatus();
        SetAmmo();
        _enhaceButton.interactable = CheckEnhanceble();
    }

    private void SetSoulParam()
    {
        _damage.text = SoulDamage(dataIndex).ToString();
        _hitCount.text = _item.hitCount.ToString();
        int other = (int)(SoulDamage(dataIndex) * _item.otherDmg);
        _otherDamage.text = other.ToString();
        _energy.text = SoulEnergy(dataIndex).ToString();
        _enPerHit.text = SoulEnergyPerHit(dataIndex).ToString();
        _speed.text = _item.speed.ToString();
    }

    private void SetAmmo()
    {
        var arm = souls[_dataIndex].armour;
        var sw = souls[_dataIndex].sword;
        var amt = souls[_dataIndex].amulet;
        if (arm.index != -1)
        {
            _armour.SetData(ForgeItemBase.Base[(ForgeItemType)arm.type][arm.index]);
        }
        else
        {
            _armour.SetData(null);
        }

        if (sw.index != -1)
        {
            _weapon.SetData(ForgeItemBase.Base[(ForgeItemType)sw.type][sw.index]);
        }
        else
        {
            _weapon.SetData(null);
        }
        if (amt.index != -1)
        {
            _amulet.SetData(ForgeItemBase.Base[(ForgeItemType)amt.type][amt.index]);
        }
        else
        {
            _amulet.SetData(null);
        }
    }
    public void ChangeAmmo()
    {
        changeAmmoEvent.Invoke();
        if (_item)
        {
            SetSoulParam();
        }
    }
    private void SetequipStatus()
    {
        _equipButton.onClick.RemoveAllListeners();
        _equipButton.gameObject.SetActive(!inCompany);
        if(sonData.eqipment[(int)AmmoType.amulet].index == -1)
        {
            _equipButton.onClick.AddListener(CallSonPanel);
            _buttonText.text = "EQUIP AMULET";
            return;
        }

        if (CheckEquiped())
        {
            _equipButton.onClick.AddListener(Remove);
            _buttonText.text = "REMOVE";
        }
        else
        {
            _equipButton.onClick.AddListener(Equip);
            _buttonText.text = "EQUIP";
        }
    }

    private void CallSonPanel()
    {
        _sonButton.onClick.Invoke();
    }
    private void Equip()
    {
        changeRadyEvent.Invoke(_dataIndex);
        _equipBack.SetBackGround();
    }

    private void Remove()
    {
        _manager.Remove(_isBasic, _dataIndex);
        gameObject.SetActive(false);
    }

    public void RemoveEquipBackground()
    {
        _equipBack.RemoveBackGround();
        removeRadyEvent.Invoke();
        SetequipStatus();
    }
    private bool CheckEquiped()
    {
        for (int i = 0; i < sonData.figthSouls.Length; i++)
        {
            if(sonData.figthSouls[i] == _dataIndex)
            {
                _isBasic = true;
                return true;
            }
        }
        for (int i = 0; i < sonData.spareSouls.Length; i++)
        {
            if (sonData.spareSouls[i] == _dataIndex)
            {
                _isBasic = false;
                return true;
            }
        }
        return false;
    }
    private void OnEnhance()
    {
        _enhancePanel.Init(_icon.sprite, _dataIndex);
        setEnhanceEvent.Invoke(_dataIndex, true);
    }
    public void OffEnhance()
    {
        setEnhanceEvent.Invoke(_dataIndex, false);
    }
    public void SelectFood(SoulCell cell, bool isSelect)
    {
        _enhancePanel.AddFood(cell, isSelect);
    }
    public void EatFood(SoulCell cell)
    {
        _manager.RemoveItem(cell);
    }
    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
    private bool CheckEnhanceble()
    {
        int sc = 0;
        for (int i = 0; i < souls.Count; i++)
        {
            if (i != _dataIndex && souls[i].level == souls[_dataIndex].level)
            {
                sc++;
            }
        }
        return souls[_dataIndex].level < sc;
    }
}
