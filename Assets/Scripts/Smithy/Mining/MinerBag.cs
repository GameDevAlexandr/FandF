using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static EnumsData;

public class MinerBag : MonoBehaviour
{
    [HideInInspector] public UnityEvent emptyEvent = new UnityEvent();
    [SerializeField] private Miner _miner;
    [SerializeField] private ApplovinAD _adManager;
    [SerializeField] private MinerBagCell[] _cells;
    [SerializeField] private GameObject _totalPanel;
    [SerializeField] private TotalMiningCell[] _totalCells;
    [SerializeField] private Button _uploadButton;
    [SerializeField] private Button _adUploadButton;
    [SerializeField] private Button _adExtraStepsButton;
    [SerializeField] private Text _priceText;

    private int _price;
    private int _curIndex;
    private int _dwarf;
    private const string _ADID = "Upload_Miner_Bag";
    private const string _ESTEPID = "+10_Miner_Steps";
    private int _itemsCounter;
    private void Awake()
    {
        _adManager.completeEvent.AddListener(ADReward);
        _adUploadButton.onClick.AddListener(ADUpload);
        _adExtraStepsButton.onClick.AddListener(ADExtraSteps);
    }
    public void SetRadyCells(int count, int capacity)
    {        
        _totalPanel.SetActive(false);
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].gameObject.SetActive(i < count);
            _cells[i].SetCapacity(capacity);
        }
        for (int i = 0; i < _totalCells.Length; i++)
        {
            _totalCells[i].Clear();
            _totalCells[i].gameObject.SetActive(false);
        }
        EmptyBag(true);
        _dwarf = GeneralData.mining.booseters[(int)MineBoostType.dwarf] ? 3 : 0;
        _adUploadButton.interactable = false;
        _adUploadButton.gameObject.SetActive(true);
        _uploadButton.gameObject.SetActive(_dwarf > 0);
        _uploadButton.interactable = false;
        _priceText.text = _dwarf + "/3";
        _adExtraStepsButton.gameObject.SetActive(true);
        _itemsCounter = 0;
    }

    public void SetOre(CurrencyType type)
    {
        _cells[_curIndex].SetOre(type);
        for (int i = 0; i < _totalCells.Length; i++)
        {
            _totalCells[i].AddItem(type);
        }
        _itemsCounter++;
        _uploadButton.gameObject.SetActive(_dwarf>0);
        _uploadButton.interactable = true;
        _adUploadButton.interactable = true;
    }

    public bool CheckEmptyCell(CurrencyType type)
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i].CheckEmpty(type))
            {
                _curIndex = i;
                return true;
            }
        }
        return false;
    }
    public void EmptyBag(bool isFree)
    {
        for (int i = 0; i < _cells.Length; i++)
        {
           _cells[i].EmptyCell();
        }
        if (isFree)
        {
            emptyEvent.Invoke();
            return;
        }
        if (_dwarf <= 0)
        {
            emptyEvent.Invoke();
        }
        else
        {
            _dwarf--;
            emptyEvent.Invoke();
        }
        _uploadButton.interactable = false;
        _adUploadButton.interactable = false;
    }
    public void SetPrice(int distance, int steps)
    {
        if (steps < 0)
        {
            _totalPanel.SetActive(true);
        }
        //_price = Mathf.Max(2, distance);
        ////_uploadButton.interactable = (steps >= _price || _dwarf > 0)&&!CheckEmptyCell();
        _priceText.text = _dwarf+"/3";
    }
    private void ADUpload()
    {
        _adManager.ShowReward(_ADID);        
    }
    public void ADExtraSteps()
    {
        _adManager.ShowReward(_ESTEPID);
    }
    private void ADReward(string adID)
    {
        if(adID == _ADID)
        {
            EmptyBag(true);
            _adUploadButton.gameObject.SetActive(false);
        }
        else if(adID == _ESTEPID)
        {
            _miner.ChangeSteps(-11);
            _totalPanel.SetActive(false);
            _adExtraStepsButton.gameObject.SetActive(false);
        }
    }
    public void EndMineAnalitic()
    {
        GameAnalitic.EndMining(_itemsCounter);
    }
}
