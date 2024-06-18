using System.Collections.Generic;
using UnityEngine;
using static EnumsData;
using static GeneralData;

public static class CalculationData
{
    public static int StepsCount()
    {
        int boost = mining.booseters[(int)MineBoostType.oil] ? 20 : 0;
        int lvl = singleItems[(int)SingleItemType.lantern];
        int str = 0;
        if (lvl > 0)
        {
            str = SingleItemsBase.Base[SingleItemType.lantern][lvl].strenght;
        }
        return 50 +str +boost;
    }
    public static int BagCells()
    {        
        return 3 + singleItems[(int)SingleItemType.mineBag];
    }

    public static int OreCount(int strength)
    {
        int lvl = singleItems[(int)SingleItemType.pickaxe];
        if(lvl == 0)
        {
            return 1;
        }
        strength = Mathf.Max(1, strength);
        Vector2Int str = SingleItemsBase.Base[SingleItemType.pickaxe][lvl].oreCount[strength-1];
        int cnt = Random.Range(str.x,str.y+1);
        return cnt;
    }
    public static int SmeltPower()
    {
        int lvl = singleItems[(int)SingleItemType.bellows];
        int str = lvl == 0 ? 0 : SingleItemsBase.Base[SingleItemType.bellows][lvl].strenght;
        return 100 + str;
    }

    public static int ForgePower()
    {
        int lvl = singleItems[(int)SingleItemType.hammer];
        int str = lvl == 0 ? 0 : SingleItemsBase.Base[SingleItemType.hammer][lvl].strenght;
        return 100 + str;
    }

    private static float[] fCL1 = new float[] { 90, 9, 1, 0, 0, 0, 0 }; 
    private static float[] fCL2 = new float[] { 80, 15, 4, 1, 0, 0, 0 }; 
    private static float[] fCL3 = new float[] { 70, 20, 7, 2.3f, 0.6f, 0.1f,0 }; 
    private static float[] fCL4 = new float[] { 60, 21, 10, 6, 2.2f, 0.6f, 0.2f }; 
    private static float[] fCL5 = new float[] { 50, 18, 15, 10, 5, 1.4f, 0.6f }; 
    private static float[][] forgeChances = new float[][] {fCL1,fCL2,fCL3,fCL4,fCL5}; 

    public static int GetForgeItemGrade(int grade)
    {
        int lvl = singleItems[(int)SingleItemType.anvil];
        if(lvl == 0)
        {
            return 0;
        }
        lvl --;
        float rnd = Random.Range(0, 100);
        float buf = forgeChances[lvl][0];
        for (int i = 1; i < forgeChances[lvl].Length; i++)
        {
            if (rnd <= buf)
            {
                var j = Mathf.Clamp(i - 1 - grade, 0, 4);
                return i-1;
            }
            buf += forgeChances[lvl][i];
        }
        return 0;
    }
    public static int GetSkillsValue(SkillsType type)
    {
        if(sonData.skills == null)
        {
            sonData.skills = new int[System.Enum.GetValues(typeof(SkillsType)).Length];
        }
        switch (type)
        {
            case SkillsType.strength: return 10 + sonData.skills[(int)type] 
                    + sonData.fightBoost[(int)FightBoostType.strength] * 2;

            case SkillsType.stamina: return 100 + sonData.skills[(int)type]*20 
                    + sonData.fightBoost[(int)FightBoostType.stamina] * 20;

            case SkillsType.agility: return 10 + sonData.skills[(int)type] 
                    + sonData.fightBoost[(int)FightBoostType.agility] * 2;
        }
        return 0;
    }

    public static int GetSonDamage()
    {
        int sval = GetSkillsValue(SkillsType.strength);
        var sw = sonData.eqipment[(int)AmmoType.sword];
        float str = sw.index != -1? ForgeItemBase.Base[(ForgeItemType)sw.type][sw.index].strenght:1;
        return Mathf.RoundToInt(sval * str);
    }
    public static int GetSonHealth()
    {
        return GetSkillsValue(SkillsType.stamina);
    }
    public static int GetSonArmor()
    {
        var sw = sonData.eqipment[(int)AmmoType.armour];
        float str = sw.index != -1 ? ForgeItemBase.Base[(ForgeItemType)sw.type][sw.index].strenght : 0;
        return (int)(str);
    }
    public static float GetSonSpeed()
    {
        return GetSkillsValue(SkillsType.agility);
    }
    public static int SonExpForLvlUp(int level)
    {
       return 100 + level * 10;
    }
    public static int PointForSonSKillUp(int skillLvl)
    {
        return skillLvl + 1;
    }

    private static float[] greenStoneChances = new float[] { 80, 17, 3, 0, 0};
    private static float[] blueStoneChances = new float[] { 0, 0, 90, 9, 1};
    private static float[] redStoneChances = new float[] { 0, 0, 0, 90, 10};
    private static float[][] summonRareChances = new float[][] { greenStoneChances, blueStoneChances, redStoneChances };
    public static int SummonSoulCalculation(int stoneRare)
    {
        List<int> chances = new List<int>();
        for (int i = 0; i < summonRareChances[stoneRare].Length; i++)
        {
            for (int j = 0; j < summonRareChances[stoneRare][i]; j++)
            {
                chances.Add(i);
            }
        }
        int rare = chances[Random.Range(0, chances.Count)];
        List<SoulItem> items = SoulsBase.Base[rare];
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].isPrem)
            {
                items.RemoveAt(i);
                i--;
            }
        }
        SoulItem item = items[Random.Range(0, items.Count)];

        return item.index;
    }
    public static int SoulDamage(int dataIndex)
    {
        var sd = souls[dataIndex];
        int index = sd.index;
        int str = SoulsBase.IndexBase[index].strength;
        str += (int)(str * sd.level * 0.3f);
        float swPw = 1;
        if (sd.sword.index >= 0)
        {
            swPw = ForgeItemBase.Base[(ForgeItemType)sd.sword.type][sd.sword.index].strenght;
        }
        str = Mathf.RoundToInt(str* swPw);
        return str;
    } 
    
    private static int SoulBaseDamage(int dataIndex)
    {
        var sd = souls[dataIndex];
        int index = sd.index;
        int str = SoulsBase.IndexBase[index].strength;
        str += (int)(str * sd.level * 0.3f);
        return str;
    }
    public static int SoulEnergyPerHit(int dataIndex)
    {
        var sd = souls[dataIndex];
        int str = SoulDamage(dataIndex);
        float swPw = 1;
        if (sd.sword.index >= 0)
        {
            swPw = ForgeItemBase.Base[(ForgeItemType)sd.sword.type][sd.sword.index].strenght;
        }
        float armPw = 0;
        if (sd.armour.index >= 0)
        {
            armPw = ForgeItemBase.Base[(ForgeItemType)sd.armour.type][sd.armour.index].strenght*3;
        }
        str = (int)(str * ((float)str / (str + armPw*swPw)));
        return str;
    }
    public static int SoulEnergy(int dataIndex)
    {
        var sd = souls[dataIndex];
        int index = sd.index;
        int en = SoulsBase.IndexBase[index].energy;
        var sAmIdx = sonData.eqipment[(int)AmmoType.amulet];
        var mAmIdx = souls[dataIndex].amulet;
        float sAmStrength = 1;
        float mAmStrength = 1;
        if (sAmIdx.index != -1)
        {
            var sAm = ForgeItemBase.Base[(ForgeItemType)sAmIdx.type][sAmIdx.index];
            sAmStrength = (1 + sAm.strenght);
        }
        if (mAmIdx.index != -1)
        {
            var sAm = ForgeItemBase.Base[(ForgeItemType)mAmIdx.type][mAmIdx.index];
            mAmStrength = (1 + sAm.soulStrength);
        }
        
        en += (int)(en * sd.level * 0.35);
        en = (int)( en*sAmStrength * mAmStrength);
        return en;
    }
    public static float SoulSpeed(int dataIndex)
    {
        var sd = souls[dataIndex];
        int index = sd.index;
        float en = SoulsBase.IndexBase[index].speed;
        en += en * sd.level * 0.1f;
        return en;
    }


    public static int GetPowerGrade(int power)
    {
        int grade = 0;
        int cp = PTStartValue();
        while (true)
        {
            cp += PTRatio() + grade * PTRatio();
            if (power > cp)
            {
                grade += 1;
            }
            else
            {
                break;
            }
        }
        return grade;
    }
    public static float GetPowerProgress(int level)
    {
        

        if (level == 0)
        {
            return (float)sonData.power / GetPowerForGrade(0);
        }
        else
        {
            return (float)(sonData.power - GetPowerForGrade(level - 1)) / (GetPowerForGrade(level) - GetPowerForGrade(level - 1));
        }
    }
    public static int GetPowerForGrade(int level)
    {
        int cp = PTStartValue();
        for (int i = 0; i <= level; i++)        
        {
            
            cp += PTRatio() + i * PTRatio();
        }
        return cp;
    }

    private static int PTStartValue()
    {
        return 30000 * sonData.powerTestLevel;
    } 
    private static int PTRatio()
    {
        return 100 * (1 + sonData.powerTestLevel * 7);
    }

    public static int LootBoxCoins()
    {
        return 100 + 50 * (sonData.level / 5);
    }
}
