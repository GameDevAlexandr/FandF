using UnityEngine;

public class MiningManager : MonoBehaviour
{
    [SerializeField] private MineGenerator _generator;
    [SerializeField] private GameObject _loadMine;
    [SerializeField] private GameModeManager _modeManager;

    public void StartMinig()
    {
        _loadMine.SetActive(true);
        SwithPanels(true);
        _generator.Generate();
        TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.startMining);
    }

    public void SwithPanels(bool isMining)
    {
        if (isMining)
        {
            _modeManager.MineMode();
        }
        else 
        { 
            _modeManager.SmithyMode();
        }
        TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.finishMining);
    }

    public void CloseLoadingPanel()
    {
        _loadMine.SetActive(false);
    }
}
