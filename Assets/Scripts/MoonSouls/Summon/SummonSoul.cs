using UnityEngine;
using static EnumsData;
using static GeneralData;
using static CalculationData;

public class SummonSoul : MonoBehaviour
{
    [SerializeField] private SoulsManager _manager;
    public SoulItem Summon(CurrencyType stone)
    {
        EventManager.AddCurrency(stone, -1);
        switch (stone)
        {
            case CurrencyType.greenStone: return SummonProc(0);
            case CurrencyType.blueStone: return SummonProc(1);
            default: return SummonProc(2);
        }
    }

    private SoulItem SummonProc(int index)
    {
        int idx = SummonSoulCalculation(index);
        int lvl = index == 0 ? 0 : index + 1;
        EventManager.AddSoul(idx, lvl);
        return SoulsBase.IndexBase[idx];
    }
}
