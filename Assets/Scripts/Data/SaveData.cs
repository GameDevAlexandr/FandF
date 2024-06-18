using static GeneralData;
using UnityEngine;
using System.Collections.Generic;

public class SaveData : MonoBehaviour
{
    [HideInInspector] public SonData sSonData;
    [HideInInspector] public SmithData sSmithData;
    [HideInInspector] public DateValue sDateValue;
    [HideInInspector] public List<SoulsData> sSsoul;
    [HideInInspector] public int[] sCurrency;
    [HideInInspector] public int[] sSingleItems;
    [HideInInspector] public FItem[] sForgeItems;
    [HideInInspector] public PtItem[] sPotionItems;
    [HideInInspector] public List<CookData> sSmeltQueue;
    [HideInInspector] public List<CookData> sForgeQueue;
    [HideInInspector] public MiningData sMining;
    [HideInInspector] public List<AlmaData> sAlmanac;
    [HideInInspector] public int[] sAdRecovery;
    [HideInInspector] public bool[] sTotorData;
    [HideInInspector] public GameSettings sSettings;
    [HideInInspector] public QuestData sQuestData;
    [HideInInspector] public List<int> sEventCompanyPoint;
    [HideInInspector] public bool[] sReceivedDailyRewards;

    public void Save(string saveName)
    {
        SaveData saveData = new SaveData();

        saveData.sSonData = sonData;
        saveData.sSmithData = smithData;
        saveData.sDateValue = dateValue;
        saveData.sSsoul = souls;
        saveData.sCurrency = currency;
        saveData.sSingleItems = singleItems;
        saveData.sForgeItems = forgeItems;
        saveData.sPotionItems = potionItems;
        saveData.sSmeltQueue = smeltQueue;
        saveData.sForgeQueue = forgeQueue;
        saveData.sMining = mining;
        saveData.sAlmanac = almanac;
        saveData.sAdRecovery = adRecovery;
        saveData.sTotorData = tutorData;
        saveData.sSettings = settings;
        saveData.sQuestData = questData;
        saveData.sEventCompanyPoint = eventCompanyPoint;
        saveData.sReceivedDailyRewards = receivedDailyReward;

        string data = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(saveName, data);
        PlayerPrefs.Save();
    }
}
