using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

public class FightPoint : MonoBehaviour
{
    public int companyIndex => _companyIndex;
    public int secondsTo => _secondsTo;
    public FightPoint[] nextPoints;
    public CompanyItem item => _item;
    public bool isNextChapterTrigger => _isNextChapterTrigger;

    [SerializeField] private CompanyItem _item;
    [SerializeField] private FightPointInfo _info;
    [SerializeField] private Button _button;
    [SerializeField] private int _secondsTo;
    [SerializeField] private int _companyIndex;
    [SerializeField] private bool _isNextChapterTrigger;
    [SerializeField] private Text _levelText;
    private void Awake()
    {
        //Controll.getOutUiPositionEvent.AddListener(OnClick);
        _button.onClick.AddListener(OnClick);
        if (_companyIndex > 0 && _companyIndex < 15)
        {
            _levelText.text = "lv. " + _companyIndex;
        }
    }

    private void OnClick(Vector2 position)
    {
       // position = Camera.main.ScreenToWorldPoint(position);
        //if (gameMode == EnumsData.GameMode.map && !_info.gameObject.activeSelf)
        //{
        //    if (gameObject.activeSelf && _sprite.bounds.Contains(position))
        //    {
        //        _info.SetData(_item, _companyIndex, _secondsTo);                
        //    }
        //}
    }
    public void OnClick()
    {
        _info.SetData(_item, _companyIndex, _secondsTo);
    }
}
