using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sound;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreen;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    private void Start()
    {
        musicSlider.value = music.volume;
        soundSlider.value = sound.volume;   
    }
    public void MusicSetting()
    {
        music.volume = musicSlider.value;
    }

    public void SoundSetting()
    {
        sound.volume = soundSlider.value;
    }

    public void ResolutionSetting()
    {
        switch (resolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080,fullScreen.isOn);
                break;
            case 1:
                Screen.SetResolution(1280, 720, fullScreen.isOn);
                break;
            case 2:
                Screen.SetResolution(640, 480, fullScreen.isOn);
                break;
        }
        Debug.Log(resolutionDropdown.value);
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
    }

    public void FullScreenSetting()
    {
        Screen.SetResolution(Screen.width, Screen.height, fullScreen.isOn);
    }

    public void QualitySetting()
    {
        QualitySettings.SetQualityLevel(qualityDropdown.value,true);
    }
}
