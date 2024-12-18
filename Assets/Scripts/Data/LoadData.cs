using static GeneralData;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    public void Load(string saveName)
    {
        if (saveName == "" || !PlayerPrefs.HasKey(saveName))
        {
            return;
        }

        string data = PlayerPrefs.GetString(saveName);
        Debug.Log(data);
        SaveData saveData = new SaveData();
        JsonUtility.FromJsonOverwrite(data, saveData);

        sonData = saveData.sSonData;
        smithData = saveData.sSmithData;
        dateValue = saveData.sDateValue;
        souls = saveData.sSsoul;
        currency = saveData.sCurrency;
        singleItems = saveData.sSingleItems;
        forgeItems = saveData.sForgeItems;
        potionItems = saveData.sPotionItems;
        smeltQueue = saveData.sSmeltQueue;
        forgeQueue = saveData.sForgeQueue;
        mining = saveData.sMining;
        almanac = saveData.sAlmanac;
        adRecovery = saveData.sAdRecovery;
        tutorData = saveData.sTotorData;
        settings = saveData.sSettings;
        questData = saveData.sQuestData;
        eventCompanyPoint = saveData.sEventCompanyPoint;
        receivedDailyReward = saveData.sReceivedDailyRewards;
        gameMode = (EnumsData.GameMode)saveData.sGameMode;
    }   
}
