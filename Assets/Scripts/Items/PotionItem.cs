using NaughtyAttributes;
using UnityEngine;
using static EnumsData;

[CreateAssetMenu(fileName = "NewPotion", menuName = "Potions",order = 16)]
public class PotionItem : ScriptableObject
{
    [ShowAssetPreview] [SerializeField] private Sprite _icon;
    [SerializeField] private PotionType _type;
    [SerializeField] private int _level;
    [SerializeField] private string _name;
    [TextArea][SerializeField] private string _description;
    [SerializeField] private int _strength;

    public Sprite icon => _icon;
    public PotionType type => _type;
    public int level => _level;
    public string itemName => _name;
    public string description => _description;
    public int strength => _strength;
    
}
