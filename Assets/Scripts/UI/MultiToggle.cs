using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MultiToggle : MonoBehaviour
{
    [SerializeField] Data[] _content;
    [SerializeField] Text _titleText;
    [SerializeField] Text _handlerText;
    private Slider _slider;

    [System.Serializable]
    public struct Data
    {
       public GameObject openedObject;
       public string titleText;
       public string handlerText;
    }
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _content.Length-1;
        _slider.onValueChanged.AddListener(Change);

    }
    private void Change(float count)
    {
        for (int i = 0; i < _content.Length; i++)
        { 
            _content[i].openedObject.SetActive(i == (int)count);            
        }
        _titleText.text = _content[(int)count].titleText;
        _handlerText.text = _content[(int)count].handlerText;
    }
}



