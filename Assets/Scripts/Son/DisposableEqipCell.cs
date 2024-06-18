using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;

public class DisposableEqipCell : MonoBehaviour
{
    public bool isEquped => _isEquiped;
    public PotionItem pItem => _item;

    [SerializeField] private Image _icon;
    [SerializeField] private Text _countText;
    [SerializeField] private EquipCellManager _manager;
    [SerializeField] private DisposableItemInfo _infoPanel;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _lockObject;
    [SerializeField] private int _capacity;
    [SerializeField] private int _pocketNumber;

    private PotionType _type;
    private PotionItem _item;
    private int _level => _item.level;
    private bool _isEquiped;

    private void Awake()
    {
        _button.onClick.AddListener(GetInfo);
        ChangeData();
    }
    public void Init()
    {
        _button.onClick.AddListener(GetInfo);
        ChangeData();
       if( sonData.pocket[_pocketNumber].count > 0)
        {
            _icon.enabled = true;
            _item = PotionItemsBase.Base[sonData.pocket[_pocketNumber].item][sonData.pocket[_pocketNumber].level];
            _icon.sprite = _item.icon;
            _isEquiped = true;
            SetCountUI();
        }
    }
    public void Equip(PotionItem item)
    {
        if (_isEquiped)
        {
            Remove();
        }
        _type = item.type;
        _item = item;        
        sonData.pocket[_pocketNumber].item = _type;
        sonData.pocket[_pocketNumber].level = _level;
        int cnt = potionItems[(int)_type].items[item.level];
        cnt = cnt >= _capacity ? _capacity : cnt;
        sonData.pocket[_pocketNumber].count = cnt;
        EventManager.AddPotion(_type,item.level, -cnt);
        _icon.enabled = true;
        _icon.sprite = item.icon;
        _isEquiped = true;
        SetCountUI();
    }
    public void Remove()
    {
        EventManager.AddPotion(_type, _level, sonData.pocket[_pocketNumber].count);
        sonData.pocket[_pocketNumber].count = 0;
        _icon.enabled = false;
        _isEquiped = false;
        SetCountUI();
    }

    private void SetCountUI()
    {
        _countText.text =_isEquiped?sonData.pocket[_pocketNumber].count + "/" + _capacity:"";
    }
    private void GetInfo()
    {
        transform.localScale = new Vector2(1.3f, 1.3f);
        _infoPanel.SetCell(_item, this);
        _manager.ActivateDespInvent();
        Sounds.chooseSound.otherButtons.Play();
    }

    public void ChangeData()
    {
        int level = singleItems[(int)SingleItemType.travelBag];
        if (level > 0)
        {
            _capacity = SingleItemsBase.Base[SingleItemType.travelBag][level].capacity;
            _lockObject.SetActive(_pocketNumber >= SingleItemsBase.Base[SingleItemType.travelBag][level].pocets);
            _button.interactable = !_lockObject.activeSelf;           
            SetCountUI();
        }
    }
    public void SetNormalSize()
    {
        transform.localScale = Vector2.one;
    }
    private void OnEnable()
    {
        if (sonData.pocket[_pocketNumber].count <= 0)
        {
            _icon.enabled = false;
            _isEquiped = false;
        }
        SetCountUI();
    }
    public bool IsFull(PotionItem item)
    {
        if (!_isEquiped)
        {
            return false;
        }
        return item == _item && _capacity - sonData.pocket[_pocketNumber].count == 0 && potionItems[(int)_type].items[_item.level]==0;
    }
}
