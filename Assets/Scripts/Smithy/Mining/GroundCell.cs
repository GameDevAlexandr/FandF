using UnityEngine;
using static CalculationData;

public class GroundCell : MonoBehaviour
{
    public bool isOre { get; private set;}
    public int oreCount;
    public bool isVisible { get; private set;}

    private SpriteRenderer _groundSprite;
    private SpriteRenderer _strenghtSprite;
    private SpriteRenderer[] _borders;
    private MineGenerator _mine;
    private Vector2Int _position;
    private int _baseStrength;

    public MineGenerator.GroundData groundData;
    public bool Mine()
    {        
        if(groundData.strength == 0)
        {
            SetBorder(true);
            return true;
        }
        Sounds.chooseSound.RandomPitch(Sounds.chooseSound.pickAxeKnock, 0.15f);
        groundData.strength--;
        _strenghtSprite.sprite = _mine.groundStrenght[Mathf.Min(_mine.groundStrenght.Length - 1, groundData.strength)];
        if (groundData.strength <= 0)
        {
            Sounds.chooseSound.RandomPitch(Sounds.chooseSound.mineOre, 0.15f);
            if (groundData.ore)
            {
                _strenghtSprite.sprite = groundData.ore;
                isOre = true;
                oreCount = OreCount(_baseStrength);
                if (groundData.type == EnumsData.GroundType.greenStone)
                {
                    
                    oreCount = Mathf.Clamp(oreCount, 1, 3);
                }
                if (groundData.type == EnumsData.GroundType.blueStone)
                {                    
                    oreCount = Mathf.Clamp(oreCount, 1, 2);
                    int rnd = Random.Range(1, oreCount);
                    oreCount = rnd;
                }
                if(groundData.type == EnumsData.GroundType.redStone)
                {
                    oreCount = 1;
                }
            }
            else
            {
                _strenghtSprite.enabled =false;
            }
            _groundSprite.sprite = _mine.emptyGround;
            _groundSprite.material = _mine.defaultMat;
            SetBorder(true);
        }
            return false;
    }
    public void Visualize()
    {
        Vector2 pos = _mine.GlobalPosition(_position);
        _groundSprite = Instantiate(_mine.baseGround, pos, Quaternion.identity, _mine.transform);
        _strenghtSprite = Instantiate(_mine.strengt, pos, Quaternion.identity, _groundSprite.transform);
        _groundSprite.sprite = groundData.sprite;
        _strenghtSprite.sprite = _mine.groundStrenght[Mathf.Min(_mine.groundStrenght.Length - 1, groundData.strength)];
        _baseStrength = groundData.strength;        
        isVisible = true;
    }
    public void RemoveOre()
    {
        _strenghtSprite.enabled = false;
        isOre = false;
    }
    public void Restore()
    {
        if (_groundSprite != null)
        {
            Destroy(_groundSprite.gameObject);
            Destroy(_strenghtSprite.gameObject);
        }
        if (_borders != null)
        {
            for (int i = 0; i < _borders.Length; i++)
            {
                if (_borders[i] != null)
                {
                    Destroy(_borders[i].gameObject);
                }
            }
        }
    }
    public void SetData(Vector2Int index, MineGenerator mine)
    {
        _position = index;
        _mine = mine;
    }
    public void SetBorder(bool isRepit)
    {
        if (_borders == null)
        {
            _borders = new SpriteRenderer[4];
            _borders[0] = Instantiate(_mine.topClear, _groundSprite.transform.position, Quaternion.identity, _groundSprite.transform);
            _borders[1] = Instantiate(_mine.downClear, _groundSprite.transform.position, Quaternion.identity, _groundSprite.transform);
            _borders[2] = Instantiate(_mine.rightClear, _groundSprite.transform.position, Quaternion.identity, _groundSprite.transform);
            _borders[3] = Instantiate(_mine.leftClear, _groundSprite.transform.position, Quaternion.identity, _groundSprite.transform);
        }
        GroundCell[] cells = new GroundCell[]
        {
            _mine.cells[_position.x, _position.y+1],
            _mine.cells[_position.x , _position.y-1],
            _mine.cells[_position.x+1, _position.y],
            _mine.cells[_position.x-1, _position.y]
        };

        for (int i = 0; i <4; i++)
        {
            _borders[i].enabled = false;
            if (cells[i].groundData.strength > 0)
            {
                _borders[i].enabled = true;
            }
            else if(isRepit)
            {
                cells[i].SetBorder(false);
            }
        }

    }
}
