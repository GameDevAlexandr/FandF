using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "MoonSoul",menuName ="MoonSouls",order =15)]
public class SoulItem : ScriptableObject
{
    [ShowAssetPreview] [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _rare;
    [SerializeField] private int _index;
    [SerializeField] private int _strength;
    [SerializeField] private float _speed;
    [SerializeField] private int _energy;
    [Range(1,4)][SerializeField] private int _hitCount =1;
    [SerializeField] private bool _isAoe;
    [SerializeField] private bool _isPrem;
    [ShowIf("_isAoe")] [SerializeField] private float _otherDmg;

    public Sprite icon => _icon;
    public string soulName => _name;
    public int rare => _rare;
    public int index => _index;
    public int strength => _strength;
    public float speed => _speed;
    public int energy => _energy;
    public int hitCount => _hitCount;
    public bool isAoe => _isAoe;
    public bool isPrem => _isPrem;
    public float otherDmg =>_isAoe?_otherDmg:0;
    public string rareName => _rareNames[rare];

    private string[] _rareNames = new string[] { "<color=#ADADADFF>COMMON</color>", "<color=#3BFF68FF>UNCOMMON</color>", "<color=#4FCAFFff>RARE</color>",
        "<color=#E660FFFF>EPIC</color>", "<color=#FF1A37FF>LEGENDARY</color>" }; 
}
