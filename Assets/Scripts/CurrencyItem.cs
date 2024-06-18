using UnityEngine;
using static EnumsData;
using NaughtyAttributes;
[CreateAssetMenu(fileName = "NewCurrency",menuName ="Currency", order = 10)]
public class CurrencyItem : ScriptableObject
{
    [SerializeField] private CurrencyType _type;
    [SerializeField] private CrcyType _cType;
    
    [ShowAssetPreview]
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [TextArea][SerializeField] private string _description;

    [ShowIf("_isPotion")]
    [SerializeField] private int _strenght;
    [ShowIf("_isPotion")]
    [SerializeField] private PotionType _potionType;

    private bool _isPotion => _cType == CrcyType.potion;

    public CurrencyType type => _type;
    public CrcyType cType => _cType; 
    public Sprite icon => _icon; 
    public string itemName => _name; 
    public string description =>_description; 
    public int strenght => _strenght;
    public PotionType potionType => _potionType;
}
