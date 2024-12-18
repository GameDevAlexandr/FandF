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
        Tutorial.TutorialHandler.tutorEvent.Invoke(Tutorial.TutorialHandler.IterationName.mine);
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
        Tutorial.TutorialHandler.tutorEvent.Invoke(Tutorial.TutorialHandler.IterationName.afterMine);
    }

    public void CloseLoadingPanel()
    {
        _loadMine.SetActive(false);
    }
}
