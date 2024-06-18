using UnityEngine;
using Spine.Unity;
using static GeneralData;

public class BossColoss: Enemy
{
    [SerializeField] SkeletonAnimation _topAnimation;
    [SerializeField] SkeletonAnimation _botAnimation;
    [SerializeField] private float _delay;

    private void Awake()
    {
        gameObject.SetActive(false);
        Invoke("StartBossAnimation", _delay);
        spawner.bkHandler.Arrived(_delay);
        _topAnimation.AnimationState.Complete += AnimationState_Complete;
    }

    private void AnimationState_Complete(Spine.TrackEntry trackEntry)
    {
        //_topAnimation.AnimationState.SetAnimation(0, "idle", true);
        _botAnimation.AnimationState.SetAnimation(0, "idle", true);
    }

    private void StartBossAnimation()
    {
        gameObject.SetActive(true);
        _topAnimation.AnimationState.SetAnimation(0,"start",false);
        _botAnimation.AnimationState.SetAnimation(0,"start", false);
        
    }
    protected override void Death()
    {
        base.Death();
        spawner.bkHandler.Finish();
        sonData.chapterComplete = Mathf.Max(sonData.chapterComplete, sonData.currentChapter+1);
        TutorialHandler.tutorEvent.Invoke(TutorialHandler.IterationName.chapterComplete);
    }
}
