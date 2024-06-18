using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Cinemachine;

public class BossKolossHandler : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _portalAnimation;
    [SerializeField] private SkeletonAnimation _arenaAnimation;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] ParticleSystem _arriveEffect;

    private CinemachineBasicMultiChannelPerlin _noise;
    private float _animationDelay;
    private float _sizeStep =1;
    private void Awake()
    {
        _noise = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        EventManager.LostCompany.AddListener(Finish);
    }
    public void Arrived(float delay)
    {
        //_arriveEffect.Play();
        Invoke("StartBossAnimation", delay);
        _animationDelay = delay;
        _noise.m_AmplitudeGain = 0.5f;
    }
    private void StartBossAnimation()
    {

        _arenaAnimation.AnimationState.SetAnimation(0,"koloss arrived",false);
        _portalAnimation.AnimationState.SetAnimation(0, "koloss arrived", false);
        _noise.m_AmplitudeGain = 0;
        _sizeStep = 0;
    }
    private void Update()
    {
        if (_sizeStep < 1)
        {
            _sizeStep += 2 * Time.deltaTime;
            _camera.m_Lens.OrthographicSize = Mathf.Lerp(6, 8, _sizeStep);            
        }
    }
    public void Finish()
    {
        _camera.m_Lens.OrthographicSize = 6;
        _arenaAnimation.AnimationState.SetAnimation(0, "idle", true);
        _portalAnimation.AnimationState.SetAnimation(0, "idle", true);
    }
}
