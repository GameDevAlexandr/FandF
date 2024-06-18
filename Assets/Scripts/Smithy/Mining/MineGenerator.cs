using System.Collections.Generic;
using UnityEngine;
using static EnumsData;
using static CalculationData;

public class MineGenerator : MonoBehaviour
{
    //[HideInInspector] public List<List<GroundCell>> cells = new List<List<GroundCell>>();
    [HideInInspector] public GroundCell[,] cells;
    [HideInInspector] public Vector2Int start;

    public SpriteRenderer leftClear;
    public SpriteRenderer rightClear;
    public SpriteRenderer topClear;
    public SpriteRenderer downClear;
    public SpriteRenderer strengt;
    public SpriteRenderer baseGround;
    public Material defaultMat;
    public Sprite emptyGround;
    public Sprite[] groundStrenght;

    [SerializeField] private MinerControll _controll;    
    [SerializeField] private GroundCell _cell;
    [SerializeField] private GameObject _mine;
    [SerializeField] private Miner _miner;
    [SerializeField] private Transform _uploadPlace;
    [SerializeField] private int[] _stepsMultiplication;
    [SerializeField] private GroundData[] _grounds;

    private float _cellSize;
    private Vector2 _generationPoint;

    private List<List<GroundData>> _chaceData = new List<List<GroundData>>();
    [System.Serializable]
    public struct GroundData
    {
        public GroundType type;
        public int strength;
        public Sprite sprite;
        public Sprite ore;
        public int chance;
        public int lodeChance;
        public float[] chanceMultiplier;
    }
 
    public void Generate()
    {
        Bounds cBound = _cell.GetComponent<SpriteRenderer>().bounds;
        _cellSize = cBound.size.x;
        int count = (int)StepsCount() + 10;
        _generationPoint = new Vector2(-_cellSize * count / 2, -_cellSize * count / 2);
        
        RestoreCells();
        CreateChance();
        start = new Vector2Int(count / 2, count / 2);
        cells = new GroundCell[count,count];
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                cells[i, j] = new GroundCell();
                cells[i, j].SetData(new Vector2Int(i, j), this);
                cells[i, j].groundData = GetGround(new Vector2Int(i, j));
            }
        }
        _miner.StartMining();
        _uploadPlace.position = _miner.transform.position;
    }

    private void RestoreCells()
    {        
        if (cells != null) 
        {
            int cnt = cells.GetLength(0);
            for (int i = 0; i < cnt; i++)
            {
                for (int j = 0; j < cnt; j++)
                {
                    cells[i, j].Restore();
                }
            }
        }
    }
    public Vector2 GlobalPosition(Vector2Int position)
    {
        float xPos = _generationPoint.x + _cellSize * position.x;
        float ypos = _generationPoint.y + _cellSize * position.y;
        return new Vector2(xPos, ypos);
    }
  
    private GroundData GetGround(Vector2Int cellPosition)
    {
        int xDist = Mathf.Abs(start.x - cellPosition.x);
        int yDist = Mathf.Abs(start.y - cellPosition.y);
        int dist = Mathf.Max(xDist,yDist);
        GroundData domination = new GroundData();
        domination.type = GroundType.empty;
        int idx = 0;
        for (int i = _stepsMultiplication.Length - 1; i >= 0; i--)
        {
            if (dist >= _stepsMultiplication[i])
            {
                idx = i;                
                break;
            }
        }
        if (_chaceData[idx].Count == 0)
        {
            CreateChanceInGarde(idx);
        }
        var xData = cellPosition.x > 0 ? cells[cellPosition.x - 1, cellPosition.y].groundData : _grounds[0];
        var yData = cellPosition.y > 0 ? cells[cellPosition.x, cellPosition.y - 1].groundData : _grounds[0];
            if (cellPosition.x > 0 && xData.type != GroundType.empty)
            {
                int rnd = Random.Range(0, 100);
                if (rnd < xData.lodeChance)
                {
                    domination = xData;
                }
            }
            if (cellPosition.y > 0 && yData.type != GroundType.empty)
            {
                int rnd = Random.Range(0, 100);
                if (rnd < yData.lodeChance)
                {
                    domination = yData;
                }
            }
        if (domination.type == GroundType.empty)
        {
            int rnd = Random.Range(0, _chaceData[idx].Count - 1);
            domination = _chaceData[idx][rnd];
            _chaceData[idx].RemoveAt(rnd);
        }
        else
        {
            for (int i = 0; i < _chaceData[idx].Count; i++)
            {
                if (domination.type == _chaceData[idx][i].type)
                {
                    _chaceData[idx].RemoveAt(i);
                    break;
                }
            }
        }
        Bounds eBounds = new Bounds((Vector2)start, new Vector2(3, 3));
        if (eBounds.Contains((Vector2)cellPosition))
        {
            domination.type = GroundType.empty;
            domination.ore = null;
        }
        return domination;
    }

    private void CreateChance()
    {
        
        for (int i = 0; i < _stepsMultiplication.Length; i++)
        {
            _chaceData.Add(new List<GroundData>());
            CreateChanceInGarde(i);
        }
    }
    private void CreateChanceInGarde(int index)
    {
        for (int i = 0; i < _grounds.Length; i++)
        {
            for (int j = 0; j < _grounds[i].chance + (int)(_grounds[i].chanceMultiplier[index]* _grounds[i].chance); j++)
            {
                _chaceData[index].Add(_grounds[i]);
            }
        }
    }
}
