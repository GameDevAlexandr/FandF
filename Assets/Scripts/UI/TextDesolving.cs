using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextDesolving : MonoBehaviour
{
    private Text _text=> GetComponent<Text>();
    private void OnEnable()
    {
        _text.enabled = true;
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1); 
    }

    private void Update()
    {
        if (_text.enabled)
        {
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a - Time.deltaTime / (_text.color.a*5));
            if(_text.color.a <= 0.1f)
            {
                _text.enabled = false;
            }
        }
    }

}
