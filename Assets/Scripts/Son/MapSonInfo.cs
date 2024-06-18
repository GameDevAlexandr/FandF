using UnityEngine;
using UnityEngine.UI;
using static GeneralData;
using static CalculationData; 
public class MapSonInfo : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    [SerializeField] private Text _hpText;
    [SerializeField] private GameObject _hpADRecoveryButton;
    [SerializeField] private Image _expBar;
    [SerializeField] private Text _expText;
    [SerializeField] private Text _expPoint;
 
    public void SetData()
    {
        _hpBar.fillAmount = (float)sonData.hp / GetSonHealth();
        _hpADRecoveryButton.SetActive(sonData.hp < GetSonHealth());
        _hpText.text = sonData.hp + "/" + GetSonHealth();
        _expBar.fillAmount =  (float)sonData.exp / SonExpForLvlUp(sonData.level);
        _expText.text = sonData.exp + "/" + SonExpForLvlUp(sonData.level);
        _expPoint.text = sonData.skillPoints.ToString();
    }
    private void OnEnable()
    {
        SetData();
    }
}
