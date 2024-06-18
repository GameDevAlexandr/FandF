using UnityEngine;
using UnityEngine.UI;
using static CalculationData;
using static GeneralData;

public class SonInfo : MonoBehaviour
{
    [SerializeField] private Text _damage;
    [SerializeField] private Text _health;
    [SerializeField] private Text _armor;
    [SerializeField] private Text _speed;
    [SerializeField] private GameObject _onTheWay;
    [SerializeField] private Text _expCount;
    [SerializeField] private Text _skillpointCount;
    [SerializeField] private Text _levelText;
    [SerializeField] private Image _expBar;

    private void Start()
    {
        SetData();
        EventManager.AddSonExperience.AddListener(ChangeExpParam);
        EventManager.ChangeSonSkillPoint.AddListener(ChangeExpParam);
        EventManager.ChangeSonSkillPoint.AddListener(SetData);
        ChangeExpParam();
        SetData();
    }
    public void SetData()
    {
        _damage.text = GetSonDamage().ToString();
        _health.text = GetSonHealth().ToString();
        _armor.text = GetSonArmor().ToString();
        _speed.text = GetSonSpeed().ToString();
    }

    private void ChangeExpParam()
    {
        _expBar.fillAmount = (float)sonData.exp / SonExpForLvlUp(sonData.level);
        _expCount.text = sonData.exp+"/"+ SonExpForLvlUp(sonData.level);
        _skillpointCount.text = sonData.skillPoints.ToString();
        _levelText.text = "LV."+ sonData.level;
    }
    private void OnEnable()
    {
        _onTheWay.SetActive(inCompany);
    }
}
