using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIndicator",menuName = "Rarity",order = 16)]
public class RarityIndicator : ScriptableObject
{
    [SerializeField] private int _index;
    [ShowAssetPreview][SerializeField] private Sprite _image;

    public int index => _index;
    public Sprite image => _image;

}
