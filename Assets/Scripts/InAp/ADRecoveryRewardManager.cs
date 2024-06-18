using UnityEngine;
using static GeneralData;
using static CalculationData;

public class ADRecoveryRewardManager : MonoBehaviour
{
    [SerializeField] private MapSonInfo _sonInfo;
    [SerializeField] private SonEnergy _sonEnergy;
    [SerializeField] private Cooking _smithCooking;
    [SerializeField] private ADRecoveryItem _smithEnergyItem;
    [SerializeField] private ADRecoveryItem _sonEnergyItem;
    private void Awake()
    {
        _smithEnergyItem.CompleteEvent.AddListener(RecoverySmithEnergy);
        _sonEnergyItem.CompleteEvent.AddListener(RecoverySonEnergy);
    }
    public void RecoverySonHP()
    {
        sonData.hp = GetSonHealth();
        _sonInfo.SetData();
    }
    public void RecoverySonEnergy(int count)
    {
        sonData.energy = Mathf.Min(100, sonData.energy + count);
        _sonEnergy.AddEnergy(0);
    }
    public void RecoverySmithEnergy(int count)
    {
        _smithCooking.AddEnergy(count);
    }
}
