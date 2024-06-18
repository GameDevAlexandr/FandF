
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TutorialTriggerButton : MonoBehaviour
{
    [SerializeField] private TutorialHandler.IterationName _iteration;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ClickButton);
    }
    private void ClickButton()
    {
        TutorialHandler.tutorEvent.Invoke(_iteration);
    }
}
