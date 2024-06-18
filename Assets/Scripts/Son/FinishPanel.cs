using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] private GameModeManager _gameMode;
    [SerializeField] private Text _finishtext;
    [SerializeField] private GameObject _rewardPanel;
    [SerializeField] private RewardCell[] _items;
    public void SetVictory(CompanyItem item)
    {
        _rewardPanel.SetActive(true);
        Clear();
        var rw = item.reward; 
        for (int i = 0; i < rw.Length; i++)
        {
            Sprite icon;
            if (rw[i].type == EnumsData.StoreItemType.currency)
            {
                EventManager.AddCurrency(rw[i].cType, rw[i].count);
                icon = CurrencyBase.Base[rw[i].cType].icon;
            }
            else if(rw[i].type == EnumsData.StoreItemType.forge)
            {
                EventManager.AddForgeItem(rw[i].fTipe, rw[i].level, rw[i].count);
                icon = ForgeItemBase.Base[rw[i].fTipe][rw[i].level].icon;
            }
            else
            {
                EventManager.AddPotion(rw[i].pType, rw[i].level, rw[i].count);
                icon = PotionItemsBase.Base[rw[i].pType][rw[i].level].icon;
            }
            _items[i].SetData(icon, rw[i].count);
        }
        _finishtext.text = "VICTORY";
        if (item.isEvent)
        {
            eventCompanyPoint.Remove(item.index);
        }
    }

    public void SetLost()
    {        
        _rewardPanel.SetActive(false);
        Clear();
        _finishtext.text = "MISSION FAILED";
        sonData.startCompanyIndex = 0;
        sonData.companyIndex = 0;
    }

    private void Clear()
    {
        _gameMode.MapMode();
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].Clear();
        }
        gameObject.SetActive(true);
    }
}
