
using System.Collections.Generic;
using UnityEngine;
using static GeneralData;
using static CalculationData;
using static EnumsData;
using static AppMetrica;


using AppsFlyerSDK;

public class GameAnalitic : MonoBehaviour
{    // Start is called before the first frame update
    //public static Dictionary<string, string> eventValue = new Dictionary<string, string>();
    private int minutes;
    private float timer;
    private static Dictionary<string, object> eventValue = new Dictionary<string, object>(); 
    private static Dictionary<string, string> eventValueString = new Dictionary<string, string>(); 
    void Awake()
    {
        Instance.ResumeSession();
        
        Application.RegisterLogCallback(ExeptionLog);
     
        DontDestroyOnLoad(this);
        timer = Time.time;

}
  

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Instance.PauseSession();
        }
        else
        {
            Instance.ResumeSession();
        }
    }
    private void ExeptionLog(string conditon,string stackTrace, LogType type)
    {
        if(type == LogType.Exception)
        {
            Instance.ReportError(conditon, stackTrace);
        }
    }
    public static void AddGameEvent(string key, Dictionary<string, object> body)
    {
        Instance.ReportEvent(key, body);
        //AppsFlyer.sendEvent(key, eventValueString);
        try
        {
      
        }
        catch
        {
            return;
        }
    }
    public static void AdCompleteEvent(string adID)
    {
        Dictionary<string, object> body = new Dictionary<string, object>();
        body.Add("Ad_ID", adID);
        body.Add("Session_Time", Time.unscaledTime);
        body.Add("Day_Index", dateValue.dayInGame.ToString());
        Instance.ReportEvent("Ad_Complete", body);
    }
    public static void MissionCompleteEvent(string missionID)
    {
        Dictionary<string, object> body = new Dictionary<string, object>();
        body.Add("Mission_ID", missionID);
        body.Add("Session_Time", Time.unscaledTime);
        body.Add("Day_Index", dateValue.dayInGame.ToString());
        Instance.ReportEvent("Mission_Complete", body);
    }
    public static void CompanyComplete(string missionID)
    {
        Dictionary<string, object> body = new Dictionary<string, object>();
        body.Add("Mission_ID", missionID);
        body.Add("Session_Time", Time.unscaledTime);
        body.Add("Day_Index", dateValue.dayInGame.ToString());
        Instance.ReportEvent("Company_Complete", body);
    }

    public static void StartMining()
    {
        Dictionary<string, object> body = new Dictionary<string, object>();
        body.Add("Use_Boost",mining.booseters[0]+"_"+ mining.booseters[1]+"_"+mining.booseters[2]);
        body.Add("Steps_Count", StepsCount());
        body.Add("Session_Time", Time.unscaledTime);
        body.Add("Day_Index", dateValue.dayInGame.ToString());
        Instance.ReportEvent("Mining_Start", body);
    }
    
    public static void EndMining(int mineCount)
    {
        Dictionary<string, object> body = new Dictionary<string, object>();
        body.Add("Mine_Resource", mineCount);
        body.Add("Session_Time", Time.unscaledTime);
        body.Add("Day_Index", dateValue.dayInGame.ToString());
        Instance.ReportEvent("Mining_End", body);
    }

    public static void BuyProduct(string product, string currency)
    {
        Dictionary<string, object> body = new Dictionary<string, object>();
        body.Add("Product", product);
        body.Add("Currency", currency);
        body.Add("Session_Time", Time.unscaledTime);
        body.Add("Day_Index", dateValue.dayInGame.ToString());
        Instance.ReportEvent("Buy_Item", body);
    }
    public static void Smelting(string product)
    {
        Dictionary<string, object> body = new Dictionary<string, object>();
        body.Add("Product", product);
        body.Add("Session_Time", Time.unscaledTime);
        body.Add("Day_Index", dateValue.dayInGame.ToString());
        Instance.ReportEvent("Smelt", body);
    }
    public static void Forge(string product)
    {
        Dictionary<string, object> body = new Dictionary<string, object>();
        body.Add("Product", product);
        body.Add("Session_Time", Time.unscaledTime);
        body.Add("Day_Index", dateValue.dayInGame.ToString());
        Instance.ReportEvent("Forge", body);
    }
    public static void PlayerState(string state)
    {
        Dictionary<string, object> body = new Dictionary<string, object>();
        body.Add("State", state);
        body.Add("Mission_Index", sonData.companyIndex);
        body.Add("Gold_Count", currency[(int)CurrencyType.coin]);
        body.Add("Gem_Count", currency[(int)CurrencyType.gem]);
        body.Add("WatchCoin_Count", currency[(int)CurrencyType.gem]);
        int sls = 0;
        for (int i = 0; i < sonData.figthSouls.Length; i++)
        {
            if (sonData.figthSouls[i] >= 0)
            {
                sls++;
            }
        }
        for (int i = 0; i < sonData.spareSouls.Length; i++)
        {
            if (sonData.spareSouls[i] >= 0)
            {
                sls++;
            }
        }
        body.Add("Spirits", sls);
        body.Add("Sword", (ForgeItemType)sonData.eqipment[0].type + "_" + sonData.eqipment[0].index);
        body.Add("Armour", (ForgeItemType)sonData.eqipment[1].type + "_" + sonData.eqipment[1].index);
        body.Add("Session_Time", Time.unscaledTime);
        body.Add("Day_Index", dateValue.dayInGame.ToString());
        Instance.ReportEvent("Player_State", body);
    }
    public static void PowerTestState(string state)
    {
        Dictionary<string, object> body = new Dictionary<string, object>();
        body.Add("State", state);
        body.Add("Power", sonData.power);
        body.Add("Session_Time", Time.unscaledTime);
        body.Add("Day_Index", dateValue.dayInGame.ToString());
        Instance.ReportEvent("Power_Test", body);
    }
    public static void ADClick()
    {
        Dictionary<string, object> body = new Dictionary<string, object>();
        body.Add("Session_Time", Time.unscaledTime);
        body.Add("Day_Index", dateValue.dayInGame.ToString());
        Instance.ReportEvent("Click_AD", body);
    }
        public static void PurchaseToAF(UnityEngine.Purchasing.Product product)
    {
        Dictionary<string, string> purchaseEvent = new Dictionary<string, string>();
        purchaseEvent.Add(AFInAppEvents.CURRENCY, product.metadata.isoCurrencyCode);
        purchaseEvent.Add(AFInAppEvents.REVENUE, product.metadata.localizedPrice.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")));
        purchaseEvent.Add(AFInAppEvents.QUANTITY, "1");
        purchaseEvent.Add(AFInAppEvents.CONTENT_TYPE, product.definition.id);
        purchaseEvent.Add(AFInAppEvents.VALIDATED, product.receipt!=null?"true":"false");
        AppsFlyer.sendEvent("af_purchase", purchaseEvent);
    }

    
}
