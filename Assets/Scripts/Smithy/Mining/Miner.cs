using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;
using static CalculationData;

public class Miner : MonoBehaviour
{
    [SerializeField] private MineGenerator _mine;
    [SerializeField] private MiningManager _manager;
    [SerializeField] private MineBusterManager _boostManager;
    [SerializeField] private MinerControll _controll;
    [SerializeField] private TakeOrePanel _takeOre;
    [SerializeField] private MinerBag _bag;
    [SerializeField] private Text _autoPickText;
    [SerializeField] private Slider _autoPick;
    [SerializeField] private Text _stepsText;
    [SerializeField] private Text _distanceText;
    [SerializeField] private TextGenerator _pickEffect;
    [SerializeField] private ParticleSystem _wreckEffect;
    [SerializeField] private SpriteRenderer[] _stepSprites;
    [SerializeField] private Sprite _pickaxe;
    [SerializeField] private Sprite _hand;
    [SerializeField] private Sprite _foot;
    [SerializeField] private Transform _navigator;

    private GroundCell _currentCell;
    private Vector2Int _minerPosition;
    private Vector2Int _startPosition;
    private int _distance;
    private int _steps;
    private bool _isAutoPick;
    private Bounds _spawnBounds;
    private void Awake()
    {
        _controll.MoveEvent.AddListener(Move);
        _bag.emptyEvent.AddListener(FindOre);
        _autoPick.onValueChanged.AddListener(SetAutoPick);
    }
    public void StartMining()
    {
        GameAnalitic.StartMining();
        _takeOre.gameObject.SetActive(false);
        _steps = StepsCount();
        _minerPosition = _mine.start;
        _spawnBounds = new Bounds((Vector2)_minerPosition, new Vector2(3, 3));
        transform.position = _mine.GlobalPosition(_minerPosition);
        _startPosition = _minerPosition;
        VisualizeCell();
        for (int i = _minerPosition.x-1; i <= _minerPosition.x+1; i++)
        {
            for (int j = _minerPosition.y-1; j <= _minerPosition.y+1; j++)
            {
                _mine.cells[i, j].SetBorder(true);
                _mine.cells[i, j].Mine();
            }
        }
        _currentCell = _mine.cells[_minerPosition.x, _minerPosition.y];
        _bag.SetRadyCells(BagCells(), mining.booseters[(int)MineBoostType.bag] ? 6 : 4);
        _manager.CloseLoadingPanel();
        _boostManager.ResetBoost();        
    }

    private void Move(int x, int y)
    {
        _mine.cells[_minerPosition.x, _minerPosition.y].SetBorder(true);
        int str = _mine.cells[_minerPosition.x + x, _minerPosition.y + y].groundData.strength;
        if (str > singleItems[(int)SingleItemType.pickaxe]+1)
        {
            return;
        }
        if (_mine.cells[_minerPosition.x + x,_minerPosition.y + y].Mine())
        {            
            _minerPosition.x += x;
            _minerPosition.y += y;
            transform.position = _mine.GlobalPosition(_minerPosition);
            _currentCell = _mine.cells[_minerPosition.x,_minerPosition.y];
            _distance = Mathf.Max(Mathf.Abs(_minerPosition.x - _startPosition.x), Mathf.Abs(_minerPosition.y - _startPosition.y));
            _distanceText.text = _distance.ToString();
            VisualizeCell();
            FindOre();
            if (_spawnBounds.Contains((Vector2)_minerPosition))
            {
                _bag.EmptyBag(true);
                _navigator.gameObject.SetActive(false);
            }
            else
            {
                _navigator.gameObject.SetActive(true);
            }
            float angle = Mathf.Atan2(- transform.position.y,- transform.position.x) * Mathf.Rad2Deg + transform.eulerAngles.y;            
            _navigator.transform.rotation = Quaternion.Euler(0, 0, angle);
            Sounds.chooseSound.RandomPitch(Sounds.chooseSound.mineSteps, 0.15f);
        }
        else if(_mine.cells[_minerPosition.x + x, _minerPosition.y + y].groundData.strength==0)
        {
            _wreckEffect.transform.position = _mine.GlobalPosition(new Vector2Int(_minerPosition.x + x, _minerPosition.y + y));
            _wreckEffect.Play();
        }

        ChangeSteps(1);
        SetStepSprites();
    }
    private void SetStepSprites()
    {
        GroundCell[] cells = new GroundCell[] 
        {
            _mine.cells[_minerPosition.x+1, _minerPosition.y],
            _mine.cells[_minerPosition.x-1, _minerPosition.y],
            _mine.cells[_minerPosition.x, _minerPosition.y+1],
            _mine.cells[_minerPosition.x, _minerPosition.y-1]
        };
        for (int i = 0; i < cells.Length; i++)
        {
            _stepSprites[i].enabled = true;
            if (cells[i].groundData.strength > singleItems[(int)SingleItemType.pickaxe] + 1)
            {
                _stepSprites[i].enabled = false;
            }
            else if (cells[i].groundData.strength == 0)
            {
                _stepSprites[i].sprite = _foot;
            }
            else
            {
                _stepSprites[i].sprite = _pickaxe;
            }
        }
    }

    private void VisualizeCell()
    {
        for (int i = _minerPosition.x - 3; i <= _minerPosition.x + 3; i++)
        {
            for (int j = _minerPosition.y - 3; j <= _minerPosition.y + 3; j++)
            {
                if (!_mine.cells[i, j].isVisible)
                {
                    _mine.cells[i, j].Visualize();
                }
            }
        }
    }

    private void FindOre()
    {        
        if (_currentCell.isOre)
        {
            _takeOre.SetData(ToCurrency(_currentCell.groundData.type), _currentCell.oreCount);
            if (!_isAutoPick)
            {
                _takeOre.gameObject.SetActive(true);                
            }
            else
            {
                int oc = _currentCell.oreCount;
                _currentCell.oreCount = _takeOre.AutoPick();
                oc -= _currentCell.oreCount;
                if (oc != 0)
                {
                    _pickEffect.StartFly("x" + oc, false, _currentCell.groundData.ore);
                }
                if (_currentCell.oreCount <= 0)
                {
                    _currentCell.RemoveOre();
                }
            }
        }
        else
        {
            _takeOre.gameObject.SetActive(false);
        }
    }
    public void RemoveOre()
    {
        _currentCell.oreCount--;
        if (_currentCell.oreCount <= 0)
        {
            _currentCell.RemoveOre();
        }
    }
    public CurrencyType ToCurrency(GroundType type)
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(CurrencyType)).Length; i++)
        {
            CurrencyType cType = (CurrencyType)i;
            if (cType.ToString() == type.ToString())
            {
                return cType;
            }
        }
        return CurrencyType.coin;
    }
    public void ChangeSteps(int count)
    {
        _steps-=count;
        _stepsText.text = _steps.ToString();
        _bag.SetPrice(_distance, _steps);
    }
    public void SetAutoPick(float value)
    {
        _autoPickText.text = value > 0 ? "ON" : "OFF";
        _isAutoPick = value > 0;
    }
        
}
