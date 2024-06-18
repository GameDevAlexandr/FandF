using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

public class EnhanceSoulPanel : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _enhanceBtn;
    [SerializeField] private Text _description;
    [SerializeField] private SoulInfo _info;
    [SerializeField] private SoulFood[] _food;

    private int _foodCounter;
    private int _level;
    private int _dataIndex;
    private void Start()
    {
        _enhanceBtn.onClick.AddListener(Enhacne);
    }
    public void Cancel()
    {
        _info.OffEnhance();
        gameObject.SetActive(false);
        _foodCounter = 0;
    }
    private void Enhacne()
    {
        var sd = souls[_dataIndex];
        sd.level++;
        souls[_dataIndex] = sd;
        int dc = 0;
        for (int i = 0; i <_food.Length; i++)
        {
            if ( i <= _level &&_dataIndex > _food[i].cell.dataIndex)
            {
                dc++;
            }
        }
        _dataIndex -= dc;        
        for (int i = 0; i <= _level; i++)
        {
            _info.EatFood(_food[i].cell);            
        }
        _info.SetInfo(_dataIndex);
        Cancel();
    }
    public void Init(Sprite icon, int dataIndex)
    {
        _dataIndex = dataIndex;
        _icon.sprite = icon;
        _level = souls[dataIndex].level;
        for (int i = 0; i < _food.Length; i++)
        {
            _food[i].SetLock(i <= _level);
        }
        _enhanceBtn.interactable = false;
        gameObject.SetActive(true);
        _foodCounter = 0;
    } 
    public void AddFood(SoulCell cell, bool isAdd)
    {
        _foodCounter += isAdd ? 1 : -1;
        for (int i = 0; i <= souls[cell.dataIndex].level; i++)
        {
            if (_food[i].cell == cell)
            {
                _food[i].SetData(null);
                break;
            }
            else if(_food[i].cell == null)
            {
                _food[i].SetData(cell);
                break;
            }
        }
        Debug.Log("foods " + _foodCounter + " Add "+ isAdd);
        _enhanceBtn.interactable = _foodCounter == _level + 1;
        _info.setEnhanceSelectableEvent.Invoke(_foodCounter != _level + 1);
    }
}
