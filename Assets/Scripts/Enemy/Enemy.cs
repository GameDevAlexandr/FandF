using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using static CalculationData;

public class Enemy : MonoBehaviour
{
    public SkeletonDataAsset icon => _animation.skeletonDataAsset;
    [HideInInspector] public EnemyItem item;
    [HideInInspector] public EnemySpawner spawner;

    [SerializeField] SkeletonAnimation _animation;
    [SerializeField] private HealthBar _hpBar;
    [SerializeField] private Image _speedBar;
    [SerializeField] private SpriteRenderer _bounds;
    [SerializeField] private ParticleSystem _hit;
    [SerializeField] private ParticleSystem _shot;
    [SerializeField] private ParticleSystem _die;

    private int _health;
    private float _attakTimer;
    private float _hitTime;
    private void Start()
    {
        _health = item.health;
        EventManager.FightStep.AddListener(AddAttackTime);
        Controll.getOutUiPositionEvent.AddListener(GetTarget);
        _attakTimer = 1f / item.speed;
        _hitTime = _attakTimer;
        _animation.AnimationState.Complete += AnimationState_Complete;
    }

    private void AnimationState_Complete(Spine.TrackEntry trackEntry)
    {
        _animation.AnimationState.SetAnimation(0, "idle", true);
    }

    private void GetTarget(Vector2 tapPos)
    {
        if (_bounds.bounds.Contains(tapPos))
        {
            spawner.SetTarget(this);
            spawner.son.Attak(this, tapPos);
        }
    }
    public void GetDamage(int damage)
    {
        _health -= damage;
        _hpBar.TakeDamage((float)_health / item.health);
        _animation.AnimationState.SetAnimation(0, "hit", false);
        _hit.Play();
        if (_health <= 0)
        {
            Death();
        }
    }
    private void AddAttackTime(float time)
    {
        if (_attakTimer - time <= 0)
        {
            Attak();
            _attakTimer = _hitTime+ (_attakTimer - time);
        }
        else
        {
            _attakTimer -= time;
        }
        _speedBar.fillAmount = _attakTimer / _hitTime;
    }
    private void Attak()
    {
        if (_health <= 0)
        {
            return;
        }
        spawner.son.TakeDamage(Damage());
        float angle = Mathf.Atan2(spawner.heroPosition.y - transform.position.y, spawner.heroPosition.x - transform.position.x);
        var shape = _shot.shape;
        shape.rotation = new Vector2(-angle * Mathf.Rad2Deg, shape.rotation.y);
        _shot.Play();
    }
    protected virtual void Death()
    {
        _die.transform.parent = null;
        _die.Play();
        spawner.DestroyEnemy(this);
        EventManager.KillEnemy(item.enemyName);
        Destroy(gameObject);
    }
    
    private int Damage()
    {
        int rnd = Random.Range(-item.spread, item.spread + 1);
        return item.strenght + rnd;
    }
     
}
