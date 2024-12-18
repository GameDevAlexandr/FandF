
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using static GeneralData;

public class Sounds : MonoBehaviour
{
    public static Sounds chooseSound { get; private set; }
    [SerializeField] private AudioMixerGroup mixer;
    public AudioSource backGroundSmith;
    public AudioSource backGroundMap;
    public AudioSource backGroundMine;
    public AudioSource backGroundFight;
    public AudioSource forgin;
    public AudioSource sonAttack;
    public AudioSource swordAttack;
    public AudioSource createNewSword;
    public AudioSource buyAtCoins;
    public AudioSource helpMessage;
    public AudioSource buttonCraftFigth;
    public AudioSource otherButtons;
    public AudioSource enemyDie;
    public AudioSource levelUp;
    public AudioSource enemyHit;
    public AudioSource bubble;
    public AudioSource reward;
    public AudioSource hint;
    public AudioSource abilityReady;
    public AudioSource abilityRoulette;
    public AudioSource startAbility;
    public AudioSource bossKolosStart;
    public AudioSource bossKolosFinish;
    public AudioSource mergeWeapon;
    public AudioSource openLootBox;
    public AudioSource bossFIght;
    public AudioSource purchaseReward;
    public AudioSource purchaseRewardCell;
    public AudioSource pickAxeKnock;
    public AudioSource mineOre;
    public AudioSource mineSteps;

    private AudioSource _curBackground;
    private void Awake()
    {
        if (chooseSound == null)
        {
            chooseSound = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _curBackground = backGroundSmith;
    }
    public void RandomPitch(AudioSource pitchedAudio, float spread)
    {
        float pitch = Random.Range(-spread, spread);
        pitchedAudio.pitch = 1 + pitch;
        if (!pitchedAudio.isPlaying)
        {
            pitchedAudio.Play();
        }
        else if(pitchedAudio.time>0.1f)
        {
            pitchedAudio.Play();
        }
    }
    public void SetMusicVolume()
    {

        mixer.audioMixer.SetFloat("MusicVolume", settings.musicVolume);        
    }
    public void SetSoundsVolume()
    {
        mixer.audioMixer.SetFloat("SoundsVolume", settings.soundVolume);
    }
    public void Mute()
    {
        if (settings.mute)
        {
            mixer.audioMixer.SetFloat("MasterVolume", -80);
        }
        else
        {
            mixer.audioMixer.SetFloat("MasterVolume", 0);
        }
    }
    public void ButtonClick(int typeNumber)
    {
        switch(typeNumber)
        {
            case 1: buttonCraftFigth.Play();
                break;
            case 2: buyAtCoins.Play();
                break;
            default: otherButtons.Play();
                break;
        }
    }

    public void ChangeBackground(AudioSource source)
    {
        StartCoroutine(FadeSound(source));
    }
    public void OverlapBackground(AudioSource source)
    {
        float tr = _curBackground.time;
        _curBackground.Stop();
        _curBackground = source;
        _curBackground.time = tr;
        _curBackground.Play();
    }
    private IEnumerator FadeSound(AudioSource source)
    {
        float fadeTime = 1;
        float elapsedTime = 0;
        source.volume = 0;
        source.Play();
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(0, 1, elapsedTime / fadeTime);
            _curBackground.volume = Mathf.Lerp(1, 0, elapsedTime / fadeTime);
            yield return null;
        }
        _curBackground.Stop();
        _curBackground = source;
    }
}
