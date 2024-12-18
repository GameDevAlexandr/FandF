using UnityEngine;
using UnityEngine.UI;
using static EnumsData;
using static GeneralData;
using static CalculationData;

public class GameModeManager : MonoBehaviour
{
    [SerializeField] MapSonInfo _mSonInfo;
    [SerializeField] private SwitchData[] _switchMods;
    private void Start()
    {
        Debug.Log(gameMode);
        if(gameMode == GameMode.map || gameMode == GameMode.fight)
        {
            Switch(GameMode.map);
            return;
        }
        gameMode = GameMode.smythy;
    }

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
            Tutorial.TutorialHandler.tutorEvent.Invoke(Tutorial.TutorialHandler.IterationName.soulBattle);
        }
        Tutorial.TutorialHandler.tutorEvent.Invoke(Tutorial.TutorialHandler.IterationName.fight);
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
        Tutorial.TutorialHandler.tutorEvent.Invoke(Tutorial.TutorialHandler.IterationName.location);
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
            Tutorial.TutorialHandler.tutorEvent.Invoke(Tutorial.TutorialHandler.IterationName.amulet);
        }
        if (forgeItems[(int)ForgeItemType.commonAmulet].items[0] >= 1 && !inCompany)
        {
            Tutorial.TutorialHandler.tutorEvent.Invoke(Tutorial.TutorialHandler.IterationName.amuletComplete);
        }
        Tutorial.TutorialHandler.tutorEvent.Invoke(Tutorial.TutorialHandler.IterationName.buy);
    }
    public void MineMode()
    {
        Sounds.chooseSound.OverlapBackground(Sounds.chooseSound.backGroundMine);
        Switch(GameMode.mine);        
    }

    private void Switch(GameMode mode)
    {        
        gameMode = mode;
        Debug.Log(gameMode);
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
