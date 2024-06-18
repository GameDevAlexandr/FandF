using UnityEngine;
using static CalculationData;
using static GeneralData;
using Spine.Unity;
using UnityEngine.UI;

public class SonFight : MonoBehaviour
{
    [SerializeField] private HealthBar _hpBar;
    [SerializeField] private Text _hpText;
    [SerializeField] private HealthBar _expBar;
    [SerializeField] private Text _expText;
    [SerializeField] private Text _lvlText;
    [SerializeField] private Text _skillPointText;
    [SerializeField] private SkeletonAnimation _animation;
    [SerializeField] private TextGenerator _tGenerator;
    [SerializeField] private TextGenerator _hitGenerator;

    private Enemy _target;
    private int _armor;
    private int _damage;
    private float _speed;
    private int _health;
    private void Start()
    {
        _animation.AnimationState.Complete += AnimationState_Complete;
        EventManager.AddSonExperience.AddListener(ChangeExpData);
    }
    private void OnEnable()
    {
        _hpBar.TakeDamage((float)sonData.hp / GetSonHealth());
        _hpText.text = sonData.hp + "/" + _health;
        _armor = GetSonArmor();
        _damage = GetSonDamage();
        _health = GetSonHealth();
        _speed = GetSonSpeed();
        for (int i = 0; i < sonData.fightBoost.Length; i++)
        {
            sonData.fightBoost[i] = 0;
        }
        ChangeExpData();
    }
    private void AnimationState_Complete(Spine.TrackEntry trackEntry)
    {
        _animation.AnimationState.SetAnimation(0, "idle", true);
    }

    public void TakeDamage(int damage)
    {
        sonData.hp -= Penetration(damage);
        _hpBar.TakeDamage((float)sonData.hp / _health);
        _hpText.text = sonData.hp + "/" + _health;
        _hitGenerator.StartFly(Penetration(damage).ToString(), false);
        if (sonData.hp <= 0)
        {
            Death();
        }
    }
    public void Attak(Enemy target, Vector2 position)
    {
        _target = target;
        _target.GetDamage(Damage());
        _animation.AnimationState.SetAnimation(0, "attack1", false);
        _tGenerator.StartFly(Damage().ToString(), false, position);
        EventManager.AddSpeedTime(_speed);
    }

    private int Damage()
    {
        return _damage;
    }
    private void Death()
    {
        if (gameMode == EnumsData.GameMode.fight)
        {
            GameAnalitic.PlayerState("Lost");
        }
        TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.lost_0);
        EventManager.LostCompany.Invoke();
        
    }

    private int Penetration(int damage)
    {
        int result = (int)(damage * ((float)damage / (damage + _armor)));
        result =(int)Mathf.Max(damage * 0.15f, result);
        return result;
    }
    private void ChangeExpData()
    {
        _expBar.Healing((float)sonData.exp / SonExpForLvlUp(sonData.level));
        _expText.text = sonData.exp + "/" + SonExpForLvlUp(sonData.level);
        _lvlText.text = "LV. " + sonData.level;
        if (sonData.skillPoints > 0)
        {
            _skillPointText.gameObject.SetActive(true);
            _skillPointText.text = sonData.skillPoints.ToString();
        }
        else
        {
            _skillPointText.gameObject.SetActive(false);
        }      
    }
}
