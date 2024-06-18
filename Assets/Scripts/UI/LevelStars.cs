using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStars : MonoBehaviour
{
    [SerializeField] private Image[] _stars;
    
    public void SetLvl(int lvl)
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].enabled = i <= lvl;
        }
    }
}
