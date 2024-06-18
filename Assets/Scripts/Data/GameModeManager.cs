using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;
using static CalculationData;

public class GameModeManager : MonoBehaviour
{
    [SerializeField] MapSonInfo _mSonInfo;
    [SerializeField] private SwitchData[] _switchMods;

    [System.Serializable]
    public struct SwitchData
    {
        public GameMode mode;
        public GameObject[] close;
        public GameObject[] open; 
    }
   
    public void FightMode()
    {
        Switch(GameMode.fight); 
        Sounds.chooseSound.ChangeBackground(Sounds.chooseSound.backGroundFight);
        if (sonData.figthSouls[0] != -1)
        {
            TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.soulFight);
        }
    }
    public void PowerTestMode()
    {
        MapMode();
        FightMode();
        gameMode = GameMode.powerTest;
    }
    public void MapMode()
    {
        Sounds.chooseSound.ChangeBackground(Sounds.chooseSound.backGroundMap);
        Switch(GameMode.map);
        if(sonData.companyIndex == 0)
        {
            sonData.hp = GetSonHealth();
        }
        _mSonInfo.SetData();
        TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.fight_1);
    }

    public void SmithyMode()
    {
        if (gameMode == GameMode.mine)
        {            
            Sounds.chooseSound.OverlapBackground(Sounds.chooseSound.backGroundSmith);
        }
        else
        {
            Sounds.chooseSound.ChangeBackground(Sounds.chooseSound.backGroundSmith);
        }
        Switch(GameMode.smythy);
        if (currency[(int)CurrencyType.moonTears] > 0 && currency[(int)CurrencyType.greenStone] > 3 && !inCompany)
        {
            TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.craftAmulet);
        }
    }
    public void MineMode()
    {
        Sounds.chooseSound.OverlapBackground(Sounds.chooseSound.backGroundMine);
        Switch(GameMode.mine);
    }

    private void Switch(GameMode mode)
    {
        gameMode = mode;
        for (int i = 0; i < _switchMods.Length; i++)
        {
            if(_switchMods[i].mode == mode)
            {
                for (int j = 0; j < _switchMods[i].close.Length; j++)
                {
                    _switchMods[i].close[j].SetActive(false);
                }
                for (int j = 0; j < _switchMods[i].open.Length; j++)
                {
                    _switchMods[i].open[j].SetActive(true);
                }
                break;
            }
        }
    }
}
