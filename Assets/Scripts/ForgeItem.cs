using NaughtyAttributes;
using UnityEngine;
using static EnumsData;

[CreateAssetMenu(fileName = "NewForgeItem", menuName = "Smithy/ForgeItem", order = 10)]
public class ForgeItem : ScriptableObject
{
    [SerializeField] private ForgeItemType _type;
    [SerializeField] private int _materialIndex;
    [SerializeField] private AmmoType _ammoType;
    [SerializeField] private int _level;
    [SerializeField] private bool _isLast;
    [ShowAssetPreview][SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private float _strnght;
    [ShowIf("isAmulet")] [SerializeField] private int _figthPocket;
    [ShowIf("isAmulet")] [SerializeField] private int _spare;
    [ShowIf("isAmulet")] [SerializeField] private float _soulStrangth;

    private bool isAmulet => _ammoType == AmmoType.amulet;
    private Sprite _bufIcon;
    public ForgeItemType type => _type;
    public AmmoType ammoType => _ammoType;
    public int level => _level;
    public int material => _materialIndex;
    public bool isLast => _isLast;
    public Sprite icon => _icon;
    public string itemName => _name;
    public string description => _description;
    public float strenght => _strnght;
    public int fightPocket => _figthPocket;
    public int spare => _spare;
    public float soulStrength => _soulStrangth;
}
