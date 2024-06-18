using NaughtyAttributes;
using UnityEngine;
using static EnumsData;

[CreateAssetMenu(fileName ="NewItem", menuName ="SingleItem",order = 11)]
public class SingleItem : ScriptableObject
{
    [SerializeField] private SingleItemType _type;
    [ShowAssetPreview]
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [TextArea][SerializeField] private string _description;
    [SerializeField] private int _level;
    [ShowIf("_isTBag")] [SerializeField] private int _pocet;
    [ShowIf("_isTBag")] [SerializeField] private int _capacity;
    [HideIf("_isTBag")] [SerializeField] private int _strenght;
    [ShowIf("_isPicAxe")] [SerializeField] private Vector2Int[] _oreCount;

    private bool _isTBag=>_type== SingleItemType.travelBag;
    private bool _isCraft=>_type== SingleItemType.bellows;
    private bool _isAnvil => _type == SingleItemType.anvil;
    private bool _isPicAxe => _type == SingleItemType.pickaxe; 
    public SingleItemType type => _type;
    public Sprite icon => _icon;
    public string itemName => _name;
    public string description => _description;
    public int level => _level;
    public int pocets => _pocet;
    public int capacity => _capacity;
    public int strenght => _strenght;
    public Vector2Int[] oreCount => _oreCount;

}
