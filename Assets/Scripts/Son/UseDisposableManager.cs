using UnityEngine;
using UnityEngine.UI;
using static GeneralData;
using static EnumsData;
using static CalculationData;
public class UseDisposableManager : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _countText;
    [SerializeField] private Text _name;
    [SerializeField] private Text _description;
    [SerializeField] private Button _useButton;
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private MapSonInfo _sonInfo;
    [SerializeField] private Button _potionMenuButton;
    [SerializeField] private Button _potionButton;
    [SerializeField] private PocketCell[] _cells;

    private PotionItem _item;
    private int _pocketIndex;
    private void Awake()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].gameObject.SetActive(false);
        }
        SetNewPocketCells();
        SetInteractable();
    }

    public void SetNewPocketCells()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            PotionItem item = PotionItemsBase.Base[sonData.pocket[i].item][sonData.pocket[i].level];
            _cells[i].SetItem(item, i, this);
        }
        SetInteractable();
    }
    public void SetItem(PotionItem item, int pocketIndex)
    {
        _infoPanel.SetActive(true);
        _pocketIndex = pocketIndex;
        _item = item;
        _icon.sprite = item.icon;
        _name.text = item.itemName;
        _countText.text = sonData.pocket[_pocketIndex].count.ToString();
        _description.text = item.description;
        DefineEffect();
    }

    private void DefineEffect()
    {
        _useButton.onClick.RemoveAllListeners();
        switch (_item.type)
        {
            case PotionType.hpRec:
                {
                    _useButton.interactable = sonData.hp < GetSonHealth();
                    _useButton.onClick.AddListener(HpRecovery);
                    break;
                }
            case PotionType.strenghtAdd:
                {
                    _useButton.interactable = sonData.fightBoost[(int)FightBoostType.strength] == 0;
                    _useButton.onClick.AddListener(()=>Boost(FightBoostType.strength));
                    break;
                }
            case PotionType.agilityAdd:
                {
                    _useButton.interactable = sonData.fightBoost[(int)FightBoostType.agility] == 0;
                    _useButton.onClick.AddListener(() => Boost(FightBoostType.agility));
                    break;
                }
            case PotionType.staminaAdd:
                {
                    _useButton.interactable = sonData.fightBoost[(int)FightBoostType.stamina] == 0;
                    _useButton.onClick.AddListener(() => Boost(FightBoostType.stamina));
                    break;
                }
        }
    }

    private void SetInteractable()
    {
        _useButton.interactable = sonData.hp < GetSonHealth() && sonData.pocket[_pocketIndex].count > 0;
        for (int i = 0; i < sonData.pocket.Length; i++)
        {
            if (sonData.pocket[i].count > 0)
            {
                _potionMenuButton.interactable = true;
                _potionButton.interactable = true;
                return;
            }
        }
         _potionMenuButton.interactable = false;
         _potionButton.interactable = false;
    }
    private void HpRecovery()
    {
        sonData.hp = Mathf.Min(GetSonHealth(), sonData.hp + _item.strength);
        Use();
    }
    private void Boost(FightBoostType type)
    {
        sonData.fightBoost[(int)type] = _item.strength;
        _useButton.interactable = false;
        if(type == FightBoostType.stamina)
        {
            sonData.hp = Mathf.Min(GetSonHealth(), sonData.hp + _item.strength*20);
        }
        Use();
    }
    private void Use()
    {
        sonData.pocket[_pocketIndex].count--;
        _sonInfo.SetData();
        _cells[_pocketIndex].ChangeCount();
        _countText.text = sonData.pocket[_pocketIndex].count.ToString();
        SetInteractable();
    }
}
