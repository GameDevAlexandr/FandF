using UnityEngine;
using static GeneralData;

public class FlyBoxManager : MonoBehaviour
{
    [SerializeField] private SpawnData _gemBox;
    [SerializeField] private SpawnData _coinBox;
    [SerializeField] private DateTimeManager _timeManager;
    [System.Serializable]
    public struct SpawnData
    {
        public FlyingBox box;
        public int spawndDelay;
        public int spawnRepeat;
        public int count;
        [HideInInspector]public int waitTime;
        [HideInInspector]public bool isRepeat=>box.isRepeat;
    }

    public void Init()
    {
        _timeManager.everySecondEvent.AddListener(OneSecond);
        _timeManager.dayInGameEvent.AddListener(RestoreBoxes);
    }

    private void OneSecond()
    {
        if (flyBoxes[0] > 0)
        {
            _gemBox.waitTime++;
        }
        if (flyBoxes[1] > 0)
        {
            _coinBox.waitTime++;
        }
    }

    private void Spawn( ref SpawnData box)
    {
        int overTime = box.isRepeat ? box.spawnRepeat : box.spawndDelay;
        if (box.waitTime >= overTime)
        {
            if (_gemBox.box.gameObject.activeSelf)
            {
                _coinBox.box.gameObject.SetActive(false);
            }
            box.box.StartFly();
            box.waitTime = 0;
        }
    }

    private void GemBoxSpawn()
    {
        Spawn(ref _gemBox);
        if (_coinBox.box.gameObject.activeSelf)
        {
            _coinBox.box.gameObject.SetActive(false);
        }
    }
    private void CoinBoxSpawn()
    {
        if(!_gemBox.box.gameObject.activeSelf)
        Spawn(ref _coinBox);
    }
    private void RestoreBoxes()
    {
        if (flyBoxes == null)
        {
            flyBoxes = new int[2];
        }
        flyBoxes[0] = _gemBox.count;
        flyBoxes[1] = _coinBox.count;
    }
}
