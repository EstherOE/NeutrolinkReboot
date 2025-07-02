using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SettingManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    //  public Dropdown qualityDown;

    // Start is called before the first frame update

    private void Start()
    {
        if (PlayerPrefs.HasKey("music"))
        {
            LoadVolume();

        }

        else
        {
            SetMusicVolume();
            SetFXVolume();
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        if (volume <= 0.127)
            volume = 0.127f;
        mixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("music", volume);
    }
    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("music");
        sfxSlider.value = PlayerPrefs.GetFloat("sfx");

        SetMusicVolume();
        SetFXVolume();
    }
    public void SetFXVolume()
    {
        float volume = sfxSlider.value;
        if (volume <= 0.127)
            volume = 0.127f;
        mixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfx", volume);
    }
    public void SetQuality(int idx)
    {
        QualitySettings.SetQualityLevel(idx);
        PlayerPrefs.SetInt("Graphics", idx);
    }
}
