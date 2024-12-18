using System;
using System.Collections.Generic;
using UnityEngine;
using static EnumsData;
 
public class GeneralData : MonoBehaviour
{
    public static GameMode gameMode;
    public static bool withoutSave;
    public static bool inCompany => !(sonData.companyIndex==0 && sonData.secondsToCompany == 0);
    public static SmithData smithData;
    public static DateValue dateValue;
    public static MiningData mining;
    public static List<AlmaData> almanac;
    public static List<int> eventCompanyPoint;
    public static TutorialData[] tutorData;
    public static GameSettings settings;
    public static QuestData questData;
    public static bool[] receivedDailyReward;
    public static int[] flyBoxes;

    public static int[] adRecovery 
    {
        get { InitializeAdRecovery();return _adRecovery; }
        set { _adRecovery = value; }
    }
    public static SonData sonData
    {
        get { InitializeSonData(); return _sonData; }
        set { _sonData = value; }
    }
    public static List<SoulsData> souls
    {
        get { InitializeSouls(); return _souls; }
        set { _souls = value; }
    }
    public static int[] currency
    { 
        get { InitializeCurrency(); return _currency; } 
        set { _currency = value; } 
    }
    public static int[] singleItems
    {
        get { InitializeSingleItems(); return  _singleItems; }
        set { _singleItems = value; }
    }
    public static FItem[] forgeItems
    {
        get { InitializeForgeItems(); return _forgeItems; }
        set {  _forgeItems = value; }
    }

    public static PtItem[] potionItems
    {
        get { InitialisePotionItems(); return _potionItems; }
        set { _potionItems = value; }
    }
    public static List<CookData> smeltQueue 
    { 
        get { InitializeSmeltQueue(); return _smeltQueue; } 
        set { _smeltQueue = value; } 
    }
    public static List<CookData> forgeQueue
    {
        get { InitializeForgeQueue(); return _forgeQueue; }
        set { _forgeQueue = value; }
    }

    private static SonData _sonData;
    private static int[] _singleItems;
    private static int[] _currency;
    private static FItem[] _forgeItems;
    private static PtItem[] _potionItems;
    private static List<CookData> _smeltQueue;
    private static List<CookData> _forgeQueue;
    private static List<SoulsData> _souls;
    private static int[] _adRecovery;

    [Serializable]
    public struct GameSettings
    {
        public float soundVolume;
        public float musicVolume;
        public bool mute;
    }

    [Serializable]
    public struct CookData
    {
        public int typeIndex;
        public float progress;
    }
    [Serializable] 
    public struct SmithData
    {
        public int energy;
    }
    [Serializable]
    public class SonData
    {
        public fItemData[] eqipment;        
        public int level;
        public int exp;
        public int skillPoints;
        public int hp;
        public int energy;
        public int powerTestLevel;
        public int power;
        public int startCompanyIndex;
        public int currentChapter;
        public int chapterComplete;
        public int chapterUnlock;
        public int companyIndex;
        public int secondsToCompany;
        public bool companyIsComplete;
        public int[] figthSouls;
        public int[] spareSouls;
        public int[] skills;
        public int[] fightBoost;
        public Pocket[] pocket;
    }

    [Serializable]
    public struct FItem
    {
      public int[] items;
    }
    [Serializable]
    public struct PtItem
    {
        public int[] items;
    }

    [Serializable] 
    public struct Pocket
    {
      public PotionType item;
      public int level;
      public int count;
    }

    [Serializable]
    public struct fItemData
    {
       public int type;
       public int index;
    }

    [Serializable]
    public struct DateValue
    {
        public int dayInGame;
        public int lastDayInGame;
        public bool itsNewDay;
        public int minutesInGame;
        public int minutesPerDay;
        public double lastSecondToGame;
    }

    [Serializable]
    public struct SoulsData
    {
        public int index;
        public int level;
        public int energy;
        public fItemData sword;
        public fItemData armour;
        public fItemData amulet;
    }

    [Serializable]
    public struct MiningData
    {
        public bool[] booseters;
        public int timeRecovery;
    }
    [Serializable]
    public class AlmaData
    {
        public string eName;
        public int count;
    }
    [Serializable]
    public struct QuestData
    {
        public int qNumber;
        public int killCount;
        public DailyQuest[] daily;
    }
    [Serializable]
    public struct DailyQuest
    {
        public int count;
        public bool complete;
    }
    [Serializable]
    public struct TutorialData
    {
      public bool isStarted;
      public bool isComplete;
    }

    #region Initialize  
    private static void InitializeCurrency()
    {
        if (_currency == null)
        {
            _currency = new int[Enum.GetValues(typeof(CurrencyType)).Length];
        }
        else if (_currency.Length < Enum.GetValues(typeof(CurrencyType)).Length)
        {
            Array.Resize(ref _currency, Enum.GetValues(typeof(CurrencyType)).Length);
        }

    }
    private static void InitializeSingleItems()
    {
        if(_singleItems == null)
        {
            _singleItems = new int[Enum.GetValues(typeof(SingleItemType)).Length];
        }
        else if(_singleItems.Length< Enum.GetValues(typeof(SingleItemType)).Length)
        {
            Array.Resize(ref _singleItems, Enum.GetValues(typeof(SingleItemType)).Length);
        }
    }   
    private static void InitializeForgeItems()
    {
        int typs = Enum.GetValues(typeof(ForgeItemType)).Length;
        if (_forgeItems== null)
        {
            _forgeItems = new FItem[typs];
            for (int i = 0; i < _forgeItems.Length; i++)
            {
                _forgeItems[i].items = new int[5];
            }

        }
        else if (_forgeItems.Length < typs)
        {
            int bf = _forgeItems.Length;
            Array.Resize(ref _forgeItems, typs);
            for (int i = bf; i < typs; i++)
            {
                _forgeItems[i].items = new int[5];
            }
        }
    }
    private static void InitialisePotionItems()
    {
        int tCount = Enum.GetValues(typeof(PotionType)).Length;
        if(_potionItems == null)
        {
            _potionItems = new PtItem[tCount];
            for (int i = 0; i < _potionItems.Length; i++)
            {
                _potionItems[i].items = new int[5];
            }
        }
        else if(_potionItems.Length<tCount)
        {
            int bf = _potionItems.Length;
            Array.Resize(ref _potionItems, tCount);
            for (int i = bf; i < tCount; i++)
            {
                _potionItems[i].items = new int[5];
            }
        }
    }
    private static void InitializeSmeltQueue()
    {
        if(_smeltQueue == null)
        {
            _smeltQueue = new List<CookData>();
        }
    }
    private static void InitializeForgeQueue()
    {
        if (_forgeQueue == null)
        {
            _forgeQueue = new List<CookData>();
        }
    }
    private static void InitializeSonData()
    {
        if (_sonData == null)
        {
            _sonData = new SonData();
            InitializePocket();
            InitializeSonSkills();
            InitializeEquipment();
            InitializeSoulsPokets();
            InitializeFightBoost();
        }
    }
    private static void InitializePocket()
    {
        _sonData.pocket = new Pocket[10];
    }
    private static void InitializeSonSkills()
    {
        _sonData.skills = new int[Enum.GetValues(typeof(SkillsType)).Length];
        if (_sonData.skills.Length < Enum.GetValues(typeof(SkillsType)).Length)
        {
            Array.Resize(ref _sonData.skills, Enum.GetValues(typeof(SkillsType)).Length);
        }
    }
    private static void InitializeEquipment()
    {
        _sonData.eqipment = new fItemData[Enum.GetValues(typeof(AmmoType)).Length];
        for (int i = 0; i < _sonData.eqipment.Length; i++)
        {
            _sonData.eqipment[i].index = -1;
        }
    }
    private static void InitializeSoulsPokets()
    {
        _sonData.figthSouls = new int[5];
        for (int i = 0; i < _sonData.figthSouls.Length; i++)
        {
            _sonData.figthSouls[i] = -1;
        }

        _sonData.spareSouls = new int[10];
        for (int i = 0; i < _sonData.spareSouls.Length; i++)
        {
            _sonData.spareSouls[i] = -1;
        }
    }
    private static void InitializeFightBoost()
    {
        _sonData.fightBoost = new int[Enum.GetValues(typeof(FightBoostType)).Length];
    }
    private static void InitializeSouls()
    {
        if (_souls == null)
        {
            _souls = new List<SoulsData>();
        }
    }
    private static void InitializeAdRecovery()
    {
        if(_adRecovery == null)
        {
            _adRecovery = new int[Enum.GetValues(typeof(AdRecType)).Length];
        }
        else if(_adRecovery.Length< Enum.GetValues(typeof(AdRecType)).Length)
        {
            Array.Resize(ref _adRecovery, Enum.GetValues(typeof(AdRecType)).Length);
        }
    }
    #endregion 
}
