using UnityEngine;
using UnityEngine.UI;
using static GeneralData;
public class ChoiceTestPanel : MonoBehaviour
{
    [SerializeField] private GameObject _rewardsPanel;
    [SerializeField] private Transform _rewardsPanelPosition;
    [SerializeField] private Text _curentPowerText;
    [SerializeField] private Text _currentLevelText;
    [SerializeField] private Button _startTestButton;

    private Transform _startRwPanelPosition;
    private void Awake()
    {
        _startRwPanelPosition = _rewardsPanel.transform.parent;
    }
    private void OnEnable()
    {
        _startTestButton.interactable = sonData.powerTestLevel <= 1;
        _currentLevelText.text = "POWER TEST LEVEL " + (sonData.powerTestLevel + 1);
        _curentPowerText.text = "CURRENT POWER " + sonData.power;
        _rewardsPanel.transform.parent = _rewardsPanelPosition;
        _rewardsPanel.transform.localPosition = Vector2.zero;
    }
    public void StartTest()
    {
        _rewardsPanel.transform.parent = _startRwPanelPosition;
        _rewardsPanel.transform.localPosition = Vector2.zero;
        gameObject.SetActive(false);
    }
}
