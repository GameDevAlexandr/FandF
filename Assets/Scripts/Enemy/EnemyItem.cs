using NaughtyAttributes;
using UnityEngine;
using Spine.Unity;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Fight/Enemy", order = 12)]
public class EnemyItem : ScriptableObject
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private string _name;
    [TextArea] [SerializeField] private string _description;
    [SerializeField] private int _strenght;
    [SerializeField] private int _spread;
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private int _exp;
    [SerializeField] private Reward[] _reward;
    public Enemy enemy { get { _enemy.item = this; return _enemy; } }
    public SkeletonDataAsset icon => _enemy.icon;
    public string enemyName => _name;
    public string description => _description;
    public int strenght => _strenght;
    public int spread => _spread;
    public int health => _health;
    public float speed => _speed;
    public int exp => _exp;
    public Reward[] reward => _reward;
    [ShowNativeProperty] public int hardness => GetHardness();

    [System.Serializable]
    public struct Reward
    {
        public EnumsData.CurrencyType type;
        [MinMaxSlider(0, 1000)]
        public Vector2Int spread;
        public int chance;
    }
    private int GetHardness()
    {
        return (int)(health * strenght * speed/100);
    }
}
