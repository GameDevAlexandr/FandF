using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using static GeneralData;

namespace Tutorial
{
    public class TutorialHandler : MonoBehaviour
    {
        public static UnityEvent<IterationName> tutorEvent = new UnityEvent<IterationName>();
        public UnityEvent blockCompleteEvent = new UnityEvent();

        [SerializeField] private bool _isDisable;
        [SerializeField] private CoalHintEvent _mascot;
        [SerializeField] private Hole _hole;
        [SerializeField] private tBlock[] _tutorBlock;

        private int _block;
        private int _iteration;
        private IterationName _iName;
        private bool _isInitialize;
        [System.Serializable]
        public struct tBlock
        {
            public IterationName tName;
            public tData[] data;
        }
        [System.Serializable]
        public struct tData
        {
            public bool mascot;
            [AllowNesting] [ShowIf("mascot")] [TextArea(0, 10)] public string message;
            [AllowNesting] [HideIf("mascot")] public Transform lookObject;
            [AllowNesting] [HideIf("mascot")] public bool hole;
            [AllowNesting] [ShowIf("hole")] public bool isCustomHole;
            [AllowNesting] [HideIf("mascot")] public bool finger;
            public UnityEvent triggerEvent;
        }
        public enum IterationName
        {
            none,
            start,
            location,
            fight,
            chooseLevel,
            lost,
            buy,
            merge,
            minePreview,
            mine,
            afterMine,
            fastedSmelt,
            amulet,
            amuletComplete,
            soulBattle,
            lvlUp,
            newCompany,
            bossComplete,
            potion,
            upgradeGear
        }
        private void Start() => Init();
        public void Init()
        {
            Debug.Log("tutor imit");
            if (_isInitialize || _tutorBlock == null)
            {
                return;
            }
            if (tutorData == null)
            {
                tutorData = new TutorialData[_tutorBlock.Length];
            }
            else if (tutorData.Length < _tutorBlock.Length)
            {
                System.Array.Resize(ref tutorData, _tutorBlock.Length);
            }
            for (int i = 0; i < tutorData.Length; i++)
            {
                if (!tutorData[i].isComplete && (tutorData[i].isStarted || i == 0))
                {
                    _block = i;
                    withoutSave = true;
                    StartIteration();
                }
                else if (tutorData[i].isComplete)
                {
                    for (int j = 0; j < _tutorBlock[i].data.Length; j++)
                    {
                        _tutorBlock[i].data[j].triggerEvent?.Invoke();
                    }
                }
            }
            tutorEvent.AddListener(SetNewIteration);
            _isInitialize = true;
        }

        public void SetNewIteration(IterationName name)
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
            _iName = name;
            var n = DefineIteration(name);
            if (!tutorData[n].isComplete)
            {
                _block = n;
                _iteration = 0;
                withoutSave = true;
                StartIteration();
                blockCompleteEvent?.Invoke();
            }
        }
        private void StartIteration()
        {
            _tutorBlock[_block].data[_iteration].triggerEvent?.Invoke();
            if (_tutorBlock[_block].data[_iteration].mascot)
            {
                _mascot.SetCoalHint(_tutorBlock[_block].data[_iteration].message);
            }
            else
            {
                _hole.SetNewHolePosition(_tutorBlock[_block].data[_iteration]);
            }
            GameAnalitic.Tutorial(_iName.ToString(), _iteration);
        }
        public int DefineIteration(IterationName name)
        {
            for (int i = 0; i < _tutorBlock.Length; i++)
            {
                if (_tutorBlock[i].tName == name)
                {
                    return i;
                }
            }
            return 0;
        }
        public bool CheckIsComplete() => _iteration == _tutorBlock[_block].data.Length - 1;

        public void CompleteIteration()
        {
            if (_iteration >= _tutorBlock[_block].data.Length - 1)
            {
                tutorData[_block].isComplete = true;
                withoutSave = false;
                Debug.Log("Complete tutor " + _tutorBlock[_block].tName);
                return;
            }
            else
            {                
                _iteration++;
                StartIteration();
            }
        }
    }
}
