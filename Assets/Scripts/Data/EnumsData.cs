
public static class EnumsData
{
    public enum CurrencyType
    {
        coin = 0,
        greenStone = 1,
        blueStone = 2,
        redStone = 3,
        coal = 4,
        copperOre = 5,
        ironOre = 6,
        titanOre = 7,
        mithrilOre = 8,
        adamantinOre = 9,
        demonicOre = 10,
        copper = 11,
        iron = 12,
        titan = 13,
        mithril = 14,
        adamantin = 15,
        demonit = 16,
        flux = 17,
        whetstone = 18,
        leather = 20,
        hpPotion0 = 19,
        moonTears = 21,
        mineKey = 22,
        adMineKey = 23,
        adCoin = 24, 
        gem = 25,
        energyPotion = 26,
        miningCupon = 27,
    }
    public enum GroundType
    {
        wall,
        coal,
        copperOre,
        ironOre,
        titanOre,
        mithrilOre,
        adamantinOre,
        demonicOre,
        empty,
        greenStone,
        blueStone,
        redStone
    }

    public enum ForgeItemType
    {
        copperSword = 0,
        ironSword = 1,
        titanSword = 2,
        mithrilSword = 3,
        adamantSword = 4,
        demonSword = 5,
        cooperArmour = 6,
        ironArmour = 7,
        titanArmour = 8,
        mitrilArmour = 9,
        adamantArmour = 10,
        demonArmour = 11,
        commonAmulet = 12,
        uncommonAmulet = 13,
        rareAmeulet = 14,
        epicAmulet = 15,
        legendaryAmulet = 16
    }

    public enum AmmoType
    {
        sword = 0,
        armour = 1,
        amulet = 2,
    }
    
    public enum CrcyType
    {
        coins = 0,
        ore = 1,
        metall = 2,
        stone = 3,
        additive = 4,
        potion = 5
    }
    public enum SingleItemType
    {
        mineBag =0,
        lantern =1,
        hoist =2,
        pickaxe=3,
        hammer=4,
        bellows=5,
        altar=6,
        crystal =7,
        travelBag =8,
        anvil
    }

    public enum CraftType
    {
        smelt,
        forge
    }
    public enum SkillsType
    {
        strength = 0,
        stamina = 1,
        agility = 2,
        bondsOfMoon =3
    }
    public enum GameMode
    {
        fight = 0,
        smythy =1,
        map = 2,
        mine =3,
        powerTest
    }
    public enum PotionType
    {
        hpRec = 0,
        staminaAdd = 1,
        strenghtAdd = 2,
        agilityAdd = 3
    }
    public enum StoreItemType
    {
        currency = 0,
        single = 1,
        potion = 2,
        forge = 3, 
        soul = 4
    }
    public enum MineBoostType
    {
        oil,
        dwarf,
        bag
    }
    public enum FightBoostType
    {
        strength = 0,
        agility = 1,
        stamina = 2
    }
    public enum AdRecType
    {
        sonEnergy,
        smithEnergy,
        sonHealth
    }
    public enum QuestType
    {
        questComplete,
        levelComplete,
        watchAd,
        summon,
        kill,
        mineOre,
        mineCoal,
        mineStone
    }
}
