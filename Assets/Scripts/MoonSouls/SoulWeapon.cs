using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulWeapon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _swordSprite;
    [SerializeField] private Animator _animation;
    [SerializeField] private TextGenerator _dmgCount;

    private int _targetIdx;
    private List<Enemy> _enemies;
    private int _damage;
    private int _otherDamage;
    [SerializeField] private TextGenerator _damageText;
    public void Attack(List<Enemy> enemyes, int targetIdx, int dmg, int otherDmg)
    {
        _targetIdx = targetIdx;
        _enemies = enemyes;
        _damage = dmg;
        _otherDamage = otherDmg;
        _animation.SetTrigger("start");
    }
    public void SetWeapon(Sprite sprite)
    {
        //_swordSprite.enabled = sprite != null;
        //_swordSprite.sprite = sprite;
    }
    public void ToDamage()
    {
        Enemy[] enemies = _enemies.ToArray();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i])
            {               
                if (i == _targetIdx)
                {                    
                    _damageText.StartFly(_damage.ToString(), false, enemies[i].transform.position);
                    enemies[i].GetDamage(_damage);
                }
                else if(_otherDamage>0)
                {
                    _damageText.StartFly(_otherDamage.ToString(), false, enemies[i].transform.position);
                    enemies[i].GetDamage(_otherDamage);                     
                }                
            }
        }
    }
}
