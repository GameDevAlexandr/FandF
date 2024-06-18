using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGenerator : MonoBehaviour
{
    [SerializeField] private FlyText _flyText;
    [SerializeField] private int _count;

    private List<FlyText> _radyText = new List<FlyText>(); 
    private void Start()
    {
        for (int i = 0; i < _count; i++)
        {
            _flyText.generator = this;
            Instantiate(_flyText.gameObject, transform.position, Quaternion.identity, transform);
        }
    }
    public void StartFly(string message, bool itBonus, Vector2 position)
    {
        if (_radyText.Count > 0)
        {
            _radyText[0].StartFly(position, message, itBonus);
            _radyText.RemoveAt(0);
        }
    }
    public void StartFly(string message, bool itBonus)
    {
        StartFly(message, itBonus, transform.position);
    }

    public void StartFly(string message, bool itBonus, Sprite icon)
    {
        if (_radyText.Count > 0)
        {
            _radyText[0].StartFly(transform.position, message, itBonus, icon);
            _radyText.RemoveAt(0);
        }
    }
    public void RemoveText(FlyText text)
    {
        _radyText.Add(text);
    }
}
