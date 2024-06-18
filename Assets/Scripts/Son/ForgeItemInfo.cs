using UnityEngine;
using static EnumsData;
using static GeneralData;
using static CalculationData;
using UnityEngine.UI;

public class ForgeItemInfo : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _back;
    [SerializeField] private Image _frame;
    [SerializeField] private Text _name;
    [SerializeField] private Text _description;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Text _buttonText;
    [SerializeField] bool _isSoul;

    private ForgeItem _item;
    private bool _equped;
    private AmmoEquipedCell _cell;
    public void SetData(ForgeItem item)
    {
        _equipButton.onClick.RemoveAllListeners();
        _equipButton.onClick.AddListener(() => Equip(_equped));
        _equipButton.gameObject.SetActive(false);
        _item = item;
        _frame.sprite = RarityBase.frames[item.level];
        _back.sprite = RarityBase.backs[item.material];
        _icon.sprite = _item.icon;
        _name.text = _item.itemName;
        _description.text = GetDescription();
    }

    public void SetData(ForgeItem item, AmmoEquipedCell cell, bool isEqiped)
    {
        _cell = cell;
        _equped = isEqiped;
        SetData(item);
        _equipButton.gameObject.SetActive(true);
        _buttonText.text = !isEqiped ? "REMOVE" : "EQIP";
    }

    private void Equip(bool isEquip)
    {
        if (isEquip)
        {
            _cell.Equip(_item);            
        }
        else
        {
            _cell.Remove();
        }
        SetData(_item, _cell, !isEquip);
    }
    private string GetDescription()
    {
        if (_item.ammoType == AmmoType.sword)
        {
            return "<color=white>Increases Damage by</color> " + (_item.strenght * 100 - 100).ToString("0,0") + "%";
        }
        else if(_item.ammoType == AmmoType.armour)
        {
            return "<color=white>Increases Armor by</color> " + _item.strenght;
        }
        else
        {
            if (_isSoul)
            {
                return "<color=white>Increases energy by</color> +" + _item.soulStrength*100+"%";
            }
            return "<color=white>Gives</color> " + _item.fightPocket + " <color=white>combat cells of moon spirits, and</color> " + _item.spare+
                " <color=white>spare. Increases energy of moon spirits by</color> +" + _item.strenght*100+"%";
        }

    }
    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
