using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float tempVolumeMusic = PlayerPrefs.GetFloat("MusicVolume");
            audioMixer.SetFloat("Music", Mathf.Log10(tempVolumeMusic) * 20);
            musicSlider.value = tempVolumeMusic;
        }
        else
        {
            audioMixer.SetFloat("Music", Mathf.Log10(0.5f) * 20);
            musicSlider.value = 0.5f;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float tempVolumeSfx = PlayerPrefs.GetFloat("SFXVolume");
            audioMixer.SetFloat("SFX", Mathf.Log10(tempVolumeSfx) * 20);
            sfxSlider.value = tempVolumeSfx;
        }
        else
        {
            audioMixer.SetFloat("SFX", Mathf.Log10(0.5f) * 20);
            sfxSlider.value = 0.5f;
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}