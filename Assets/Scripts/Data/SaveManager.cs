using UnityEngine;
using static EventManager;
using static EnumsData;
using System.Collections;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private SaveData _save;
    private void Awake()
    {
        ChangeCurrency.AddListener((CurrencyType t, int c) => Save());
        ChangeSingleItem.AddListener((SingleItemType t) => Save());
        ChangrForgeitem.AddListener((ForgeItemType t, int l, int g) => Save());
        ChangePotions.AddListener((PotionType t, int l, int c) => Save());
        ChangeSouls.AddListener(Save);
        EquipSoul.AddListener((int idx, bool eq) => Save());
        LostCompany.AddListener(Save);
        WinCompany.AddListener(Save);
        AddSonExperience.AddListener(Save);
        StartCoroutine(SaveTimer());
    }
    private IEnumerator SaveTimer()
    {
        yield return new WaitForSeconds(1);
        Save();
        StartCoroutine(SaveTimer());
    }
    public void Save()
    {
        _save.Save("AutoSave");
    }
}
