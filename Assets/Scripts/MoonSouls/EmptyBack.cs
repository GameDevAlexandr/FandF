using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBack : MonoBehaviour
{
    [SerializeField] private Transform _emptyBackground;

    private Transform _parent;
    private void Awake()
    {
        _parent = transform.parent;
    }
    public void SetBackGround()
    {
        _emptyBackground.gameObject.SetActive(true);
        transform.parent = _emptyBackground;
    }
    public void RemoveBackGround()
    {
        transform.parent = _parent;
        _emptyBackground.gameObject.SetActive(false);
    }
}
