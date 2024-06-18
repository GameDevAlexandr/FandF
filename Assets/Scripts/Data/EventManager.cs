using UnityEngine.Events;
using UnityEngine;
using static EnumsData;
using static GeneralData;
using static CalculationData;

public class EventManager : MonoBehaviour
{
    public static UnityEvent<CurrencyType,int> ChangeCurrency = new UnityEvent<CurrencyType, int>();
    public static UnityEvent<ForgeItemType, int, int> ChangrForgeitem = new UnityEvent<ForgeItemType, int, int>();
    public static UnityEvent<PotionType, int, int> ChangePotions = new UnityEvent<PotionType, int, int>();
    public static UnityEvent<float> FightStep = new UnityEvent<float>();
    public static UnityEvent LostCompany = new UnityEvent();
    public static UnityEvent WinCompany = new UnityEvent();
    public static UnityEvent AddSonExperience = new UnityEvent();
    public static UnityEvent SonLvlUp = new UnityEvent();
    public static UnityEvent ChangeSonSkillPoint = new UnityEvent();
    public static UnityEvent<SingleItemType> ChangeSingleItem = new UnityEvent<SingleItemType>();
    public static UnityEvent<int, bool> EquipSoul = new UnityEvent<int,bool>();
    public static UnityEvent<float> ShakeCamera = new UnityEvent<float>();
    public static UnityEvent ChangeSouls = new UnityEvent();
    public static UnityEvent<AmmoType> EquipAmmo = new UnityEvent<AmmoType>();
    public static UnityEvent<MergeItems, bool> SelectMergeItem = new UnityEvent<MergeItems,bool>();
    public static UnityEvent<string> UpdateAlmanack = new UnityEvent<string>();
    public static UnityEvent EnemyDeath = new UnityEvent();
    
    public static void AddCurrency(CurrencyType type, int count)
    {
        currency[(int)type] += count;
        ChangeCurrency.Invoke(type, count);
    }
    public static void AddPotion(PotionType type, int level, int count)
    {
        if(level != -1)
        {
            potionItems[(int)type].items[level] += count;
            if (singleItems[(int)SingleItemType.travelBag] == 0)
            {
                TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.travelBag);
            }
            ChangePotions.Invoke(type, level, count);
        }
        
    }
    public static void AddForgeItem(ForgeItemType type, int level, int count)
    {
        if (level != -1)
        {
            forgeItems[(int)type].items[level] += count;            
            ChangrForgeitem.Invoke(type, level, count);
            if (level == 4 && forgeItems[(int)type].items[level] > 0)
            {
                TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.forgeMaxLevel);
            }
        }
    }

    public static void AddSingleItem(SingleItemType type, int level)
    {
        singleItems[(int)type] = level;
        ChangeSingleItem.Invoke(type);
    }
    public static void AddSpeedTime(float speed)
    {
        float fs = 1f / speed;
        FightStep.Invoke(fs);
    }

    public static void AddExpForSon(int count)
    {
        sonData.exp += count;
        if (sonData.exp >= SonExpForLvlUp(sonData.level))
        {
            sonData.exp -= SonExpForLvlUp(sonData.level);
            sonData.level++;
            SonLvlUp.Invoke();
            AddSonSkillPoint(1);
            TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.levelUp);
        }
        AddSonExperience.Invoke();
    }
    public static void AddSonSkillPoint(int count)
    {
        sonData.skillPoints+=count;
        ChangeSonSkillPoint.Invoke();
    }

    public static void AddSoul(int index, int level)
    {
        var item = SoulsBase.IndexBase[index];
        SoulsData data = new SoulsData();
        data.index = item.index;
        data.level = level;
        data.energy = item.energy;
        data.sword.index = -1;
        data.armour.index = -1;
        data.amulet.index = -1;
        souls.Add(data);
        ChangeSouls.Invoke();
    }
    public static void KillEnemy(string eName)
    {
        EnemyDeath.Invoke();
        for (int i = 0; i < almanac.Count; i++)
        {
            if (almanac[i].eName == eName)
            {
                almanac[i].count++;
                return;
            }
        }
        var ad = new AlmaData();
        ad.eName = eName;
        ad.count = 1;
        almanac.Add(ad);
        UpdateAlmanack.Invoke(eName);
    }
}
