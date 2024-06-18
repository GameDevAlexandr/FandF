using NaughtyAttributes;
using UnityEngine;
using static EnumsData;

[CreateAssetMenu(fileName = "FightPoint",menuName ="Fight/FightPoint", order =13)]
public class CompanyItem : ScriptableObject
{
    [ShowAssetPreview] [SerializeField] private Sprite _icon;
    [SerializeField] private EnemyWave[] _waves;
    [SerializeField] private Reward[] _reward;
    [SerializeField] private bool _isEvent;
    [ShowIf("_isEvent")] [SerializeField] private int _index;

    public Sprite icon => _icon;
    public EnemyWave[] wawes => _waves;
    public Reward[] reward => _reward;
    public bool isEvent => _isEvent;
    public int index => _index;
    [ShowNativeProperty] public int difficulty =>Difficulty();
    

    [System.Serializable]
    public struct EnemyWave
    {
        public EnemyItem[] enemyes;
    }

    [System.Serializable]
    public struct Reward
    {
        public StoreItemType type;
        [ShowIf("isPotion")][AllowNesting] public PotionType pType;
        [ShowIf("isForge")] [AllowNesting] public ForgeItemType fTipe;
        [ShowIf("isCurrency")] [AllowNesting] public CurrencyType cType;
        [HideIf("isCurrency")] [AllowNesting] public int level;
        public int count;

        private bool isPotion => type == StoreItemType.potion;
        private bool isForge => type == StoreItemType.forge;
        private bool isCurrency => type == StoreItemType.currency;
    }

    private int Difficulty()
    {
        int df = 0;
        for (int i = 0; i < _waves.Length; i++)
        {
            if (_waves[i].enemyes.Length == 1)
            {
                df += _waves[i].enemyes[0].hardness;
            }
            if (_waves[i].enemyes.Length == 2)
            {
                for (int j = 0; j < _waves[i].enemyes.Length; j++)
                {
                    df += (int)(_waves[i].enemyes[j].hardness*1.5f);
                }
                
            }
            if (_waves[i].enemyes.Length == 3)
            {
                for (int j = 0; j < _waves[i].enemyes.Length; j++)
                {
                    df += _waves[i].enemyes[j].hardness * 2;
                }

            }
        }

        return df;
    }
}
