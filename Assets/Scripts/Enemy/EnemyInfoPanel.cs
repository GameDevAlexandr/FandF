using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class EnemyInfoPanel : MonoBehaviour
{
    [SerializeField] private SkeletonGraphic _icon;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _damageText;
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _speed;
    [SerializeField] private RewardCell[] _loot;

    public void SetData(EnemyItem item)
    {
        for (int i = 0; i < _loot.Length; i++)
        {
            _loot[i].gameObject.SetActive(false);
        }
        _icon.skeletonDataAsset = item.icon;
        _icon.Initialize(true);
        _nameText.text = item.enemyName;
        _damageText.text = (item.strenght - item.spread) + "-"+(item.strenght + item.spread);
        _healthText.text = item.health.ToString();
        _speed.text = item.speed.ToString();
        for (int i = 0; i < item.reward.Length; i++)
        {
            _loot[i].gameObject.SetActive(true);
            Sprite icon = CurrencyBase.Base[item.reward[i].type].icon;
            _loot[i].SetData(icon, 0);
        }
    }
}
