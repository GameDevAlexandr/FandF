using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static GeneralData;

public class TutorialHandler : MonoBehaviour
{
    public static UnityEvent<IterationName> tutorEvent = new UnityEvent<IterationName>();  

    [SerializeField] private bool _isDisable;
    [SerializeField] private CoalHintEvent _coal;
    [SerializeField] private Hole _hole;
    [SerializeField] private TriggerButton[] _tButtons;
    [SerializeField] private tData[] _data;

    private int _iteration;

    [System.Serializable]
    public struct tData
    {
        public IterationName iterationName;
        public IterationName nextIteration;
        public IterationName skipIteration;
        public bool mascot;
        [AllowNesting][ShowIf("mascot")][TextArea(0,10)] public string hinttext;
        public Transform lookObject;
        public bool trigger;
        [AllowNesting] [ShowIf("isLoock")] public bool hole;
        [AllowNesting] [ShowIf("hole")] public bool isCustomHole;
        [AllowNesting] [ShowIf("isLoock")] public bool finger;
        public GameObject padLock;
        public GameObject unhide;
        private bool isLoock => lookObject != null;
    }
    [System.Serializable]
    public struct TriggerButton
    {
        public Button button;
        public IterationName name;
    }
    public enum IterationName
    {
        none,
        hello,
        future,
        ammo_0,
        ammo_1,
        ammo_2,
        ammo_3,
        ammo_4,
        ammo_5,
        ammo_6,
        ammo_7,
        ammo_8,
        ammo_9,
        ammo_10,
        ammo_11,
        fight_0,
        fight_1,
        fight_2,
        fight_3,
        goToMission,
        fight_4,
        fight_5,
        fight_6,
        fight_7,
        fight_8,
        fight_9,
        fight_10,
        fight_11,
        fight_12,
        missionComplete,
        fight_13,
        nextMission_0,
        nextMission_1,
        nextMission_2,
        nextMission_3,
        nextMission_4,
        lost_0,
        lost_1,
        startCraft,
        craft_0,
        craft_1,
        craft_2,
        craft_3,
        craft_4,
        craft_5,
        craft_6,
        forgeComplete,
        craft_7,
        craft_8,
        craft_9,
        craft_10,
        craft_11,
        craft_12,
        craft_13,
        goSmithy,
        buyCoal,
        craft_14,
        mergeComplete,
        mine_0,
        mine_1,
        mine_2,
        mine_3,
        mine_4,
        startMining,
        mine_5,
        finishMining,
        startSmelting,
        smelt_0,
        craftAmulet,
        amulet_0,
        amulet_1,
        amuletComplete,
        amulet_2,
        amulet_3,
        amulet_4,
        amulet_5,
        amulet_6,
        amulet_7,
        soul_0,
        soul_1,
        soul_2,
        soul_3,
        soul_4,
        soul_5,
        soul_6,
        soul_7,
        soul_8,
        soul_9,
        soul_10,
        soul_11,
        soul_12,
        soul_13,
        soul_14,
        soul_15,
        soul_16,
        soul_17,
        soul_18,
        soul_19,
        soul_20,
        soul_21,
        soul_22,
        soul_23,
        soul_24,
        soul_25,
        soulFight,
        soulFight_1,
        soulFight_2,
        levelUp,
        levelUp_0,
        levelUp_1,
        fightPoint_14,
        newChapter,
        newChapter_1,
        chapterComplete,
        travelBag,
        forgeMaxLevel,
        buyPickAxe
    }
    //private void Start()
    //{
    //    if (tutorData == null)
    //    {
    //        tutorData = new bool[_data.Length];
    //    }
    //    else if (tutorData.Length < _data.Length)
    //    {
    //        System.Array.Resize(ref tutorData, _data.Length);
    //    }
    //    if (_isDisable)
    //    {
    //        DisableTutorial();
    //        return;
    //    }                
    //    UnlockComplite();
    //    DefineIteration();
    //    //GameManager.addMoneyEvent.AddListener(AddMoneyEvent);
    //    tutorEvent.AddListener(SetNewIteration);
    //    for (int i = 0; i < _tButtons.Length; i++)
    //    {
    //        int res = i;
    //        _tButtons[i].button.onClick.AddListener(() => SetNewIteration(_tButtons[res].name));
    //    }
    //}
    


    //public void SetNewIteration(int iteration)
    //{
    //    if (gameObject.activeSelf)
    //    {
    //        Debug.Log("tutor iteration " + iteration);
    //        if (!tutorData[iteration])
    //        {
    //            if (_data[iteration].padLock)
    //            {
    //                _data[iteration].padLock.SetActive(false);

    //            }
    //            if (_data[iteration].unhide)
    //            {
    //                _data[iteration].unhide.SetActive(true);
    //            }
    //            if (_data[iteration].skipIteration != IterationName.none)
    //            {
    //                tutorData[DefineIteration(_data[iteration].skipIteration)] = true;
    //            }
    //            tData data = _data[iteration];
    //            if (data.mascot)
    //            {
    //                _coal.SetCoalHint(data);
    //            }
    //            else if (data.hole)
    //            {
    //                _coal.gameObject.SetActive(false);
    //                _hole.transform.parent.gameObject.SetActive(true);
    //                _hole.SetNewHolePosition(data);
    //            }
    //        }
    //    }
    //}
    //public void SetNewIteration(IterationName name)
    //{
    //    SetNewIteration(DefineIteration(name));
    //}

    //private void DefineIteration()
    //{
    //    for (int i = 0; i < tutorData.Length; i++)
    //    {
    //        if (!tutorData[i] && !_data[i].trigger)
    //        {
    //            SetNewIteration(i);
    //            return;
    //        }
    //    }
    //}

    //private void UnlockComplite()
    //{
    //    for (int i = 0; i < tutorData.Length; i++)
    //    {
    //        if (_data[i].padLock)
    //        {
    //            _data[i].padLock.SetActive(!tutorData[i]);
    //        }
    //        if(_data[i].unhide)
    //        { 
    //            _data[i].unhide.SetActive(tutorData[i]);
    //        }
    //    }
    //}

    //public int DefineIteration(IterationName name)
    //{
    //    if(name == IterationName.none)
    //    {
    //        return 0;
    //    }
    //    for (int i = 0; i < _data.Length ; i++)
    //    {
    //        if(_data[i].iterationName == name)
    //        {
    //            return i;
    //        }
    //    }
    //    return 0;
    //}
    //private void DisableTutorial()
    //{
    //    for (int i = 0; i < tutorData.Length; i++)
    //    {
    //        if (_data[i].padLock)
    //        {
    //            _data[i].padLock.SetActive(false);
    //        }
    //        if (_data[i].unhide)
    //        {
    //            _data[i].unhide.SetActive(true);
    //        }
    //    }
    //}
}
