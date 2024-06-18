using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PowerTestItem", menuName ="PTitem", order =0)]
public class PowerTestGradeItem : ScriptableObject
{
    [SerializeField] private int _level; 
    [SerializeField] private RewardItem _reward;
    [SerializeField] private int _count;

    public int level => _level;
    public RewardItem reward => _reward;
    public int count => _count;
}
