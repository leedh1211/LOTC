using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;



    private void Start()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0);
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0);

        masterSlider.value = masterVolume;
        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;

        audioMixer.SetFloat("MasterVolume", masterVolume);
        audioMixer.SetFloat("BGMVolume", bgmVolume);
        audioMixer.SetFloat("SFXVolume", sfxVolume);


        masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

    }

    public void OnMasterVolumeChanged(float value)
    {
        audioMixer.SetFloat("MasterVolume", value);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void OnBGMVolumeChanged(float value)
    {
        audioMixer.SetFloat("BGMVolume", value);
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        audioMixer.SetFloat("SFXVolume", value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}
