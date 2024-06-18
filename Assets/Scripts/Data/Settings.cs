using UnityEngine;
using UnityEngine.UI;
using static GeneralData;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _sound;
    [SerializeField] private Toggle _mute;
    [SerializeField] private Sounds _soundManager;
    public void FollowToURL(string url)
    {
        Application.OpenURL(url);
    }

    public void Init()
    {
        _music.value = settings.musicVolume;
        _sound.value = settings.soundVolume;
        _mute.isOn = settings.mute;
    }
    public void ChangeSoundVolume()
    {        
        _mute.isOn = false;
        settings.soundVolume = _sound.value;
        _soundManager.SetSoundsVolume();
    }
    public void ChangeMusicVolume()
    {        
        _mute.isOn = false;
        settings.musicVolume = _music.value; 
        _soundManager.SetMusicVolume();
    }
    public void Mute()
    {        
        settings.mute = _mute.isOn;
        _soundManager.Mute();
    }
}
