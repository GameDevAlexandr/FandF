using System.Collections.Generic;
using UnityEngine;
using static GeneralData;
public class AlmanacManager : MonoBehaviour
{
    [SerializeField] private AlmanackItem[] _almaItems;
    private void Awake()
    {
        if (almanac == null)
        { 
            almanac = new List<AlmaData>();
        }
        for (int i = 0; i < _almaItems.Length; i++)
        {
            _almaItems[i].Init();
        }       
    }
}
