
using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

[RequireComponent(typeof(Button))] 
public class SoulCell : MonoBehaviour
{
    [HideInInspector] public bool isEquiped;
    [HideInInspector] public int dataIndex;
    
    public Button mainButton;


    public Image icon;
    public LevelStars level;

    [SerializeField] private Image _sword;
    [SerializeField] private Image _armour;
    [SerializeField] private Image _amulet;
    [SerializeField] private Image _equipImg;
    [SerializeField] private GameObject _onWayObj;
    [SerializeField] private Image _selectFrame;

    private SoulInfo _sInfo;
    private int _index;
    private bool _onWay;
    private bool _enhanceRady;
    private bool _isFood;
    private void Start()
    {
    }
    public virtual void SetData(int dataIndex, SoulInfo info)
    {
        if (!_sInfo)
        {
            info.changeAmmoEvent.AddListener(SetUI);
            info.setEnhanceEvent.AddListener(EnhanceRady);
            info.setEnhanceSelectableEvent.AddListener(SetEnhanceSelectable);
        }
        _sInfo = info;
        this.dataIndex = dataIndex;
        _index = souls[dataIndex].index;
        SetUI();
        mainButton.onClick.RemoveAllListeners();
        mainButton.onClick.AddListener(SetInfo);
    }

    public virtual void Equip(bool isEquip)
    {
        _equipImg.enabled = isEquip;
        isEquiped = isEquip;
    }
    private void SetUI()
    {
        icon.sprite = SoulsBase.IndexBase[_index].icon;
        level.SetLvl(souls[dataIndex].level);
        _sword.enabled = souls[dataIndex].sword.index >= 0;
        _armour.enabled = souls[dataIndex].armour.index >= 0;
        _amulet.enabled = souls[dataIndex].amulet.index >= 0;
    }
    public void SetInfo()
    {
        _sInfo.SetInfo(dataIndex);
    }
    private void OnEnable()
    {
        _onWay = isEquiped && inCompany;
        mainButton.interactable = !_onWay;
        _onWayObj.SetActive(_onWay);
    }
    public void SelectForEnhance()
    {
        _enhanceRady = !_enhanceRady;
        float sc = _enhanceRady ? 1.2f : 1f;
        transform.localScale = new Vector2(sc, sc);
        _sInfo.SelectFood(this,_enhanceRady);
    }
    public virtual void EnhanceRady(int dataIndex, bool rady)
    {
        if (dataIndex!=this.dataIndex && souls[this.dataIndex].level == souls[dataIndex].level)
        {
            _selectFrame.enabled = rady &&!_onWay;
            _isFood = rady;
            transform.localScale = Vector2.one;
            mainButton.onClick.RemoveAllListeners();
            mainButton.onClick.AddListener(rady?SelectForEnhance:SetInfo);           
        }
        else
        {
            mainButton.interactable = !rady;
        }
        if (!rady)
        {
            _isFood = false;
            mainButton.interactable = true;
            transform.localScale = Vector2.one;
            _selectFrame.enabled = false;
            mainButton.onClick.AddListener(SetInfo);
            _enhanceRady = false;
        }
    }
    public virtual void SetEnhanceSelectable(bool isSelectable)
    {
        if (_isFood && !_enhanceRady)
        {
            mainButton.interactable = isSelectable;
            _selectFrame.enabled = isSelectable;
        }
    }
    public void Destroy()
    {
        _sInfo.changeAmmoEvent.RemoveListener(SetUI);
        _sInfo.setEnhanceEvent.RemoveListener(EnhanceRady);
        _sInfo.setEnhanceSelectableEvent.RemoveListener(SetEnhanceSelectable);
        Destroy(gameObject);
    }
}
