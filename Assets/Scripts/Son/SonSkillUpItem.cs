using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static CalculationData;
using static GeneralData;
public class SonSkillUpItem : MonoBehaviour
{
    [SerializeField] private SkillsType _type;
    [SerializeField] private Text _priceText; 
    [SerializeField] private Text _levelText;
    [SerializeField] private Button _upButton;

    private int _lvl
    {
        get { return sonData.skills[(int)_type]; }
        set { sonData.skills[(int)_type] = value; }
    }
    private int _price => PointForSonSKillUp(_lvl);
    private void Awake()
    {
        SetEnableButton();
        EventManager.ChangeSonSkillPoint.AddListener(SetEnableButton);
        _upButton.onClick.AddListener(LevelUp);
    }
    private void SetEnableButton()
    {
        _upButton.gameObject.SetActive(sonData.skillPoints >= _price);
    }

    private void LevelUp()
    {
        _lvl++;
        EventManager.AddSonSkillPoint(-PointForSonSKillUp(_lvl-1));
        SetUI();
    }
    private void SetUI()
    {
        _priceText.text = _price.ToString();
        _levelText.text = _lvl.ToString();
    }
    private void OnEnable()
    {
        SetUI();
    }
}
