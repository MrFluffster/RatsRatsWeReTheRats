using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider music_slider;
    public Slider fx_slider;

    public AudioMixer audioMixer;

    void Start()
    {
        music_slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        fx_slider.value = PlayerPrefs.GetFloat("FxVolume", 0.75f);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetVolume2(float volume2)
    {
        audioMixer.SetFloat("Fx", Mathf.Log10(volume2) * 20);
        PlayerPrefs.SetFloat("FxVolume", volume2);
    }
}
