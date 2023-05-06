using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer Mixer;
    [SerializeField] Slider MusicSlider, SFXSlider;

    const string Mixer_Music = "MusicVolume";
    const string Mixer_SFX = "SFXVolume";
   // const string Mixer_UI = "UIVolume";

    private void Awake()
    {
        MusicSlider.onValueChanged.AddListener(MusicVolume);
        SFXSlider.onValueChanged.AddListener(SFXVolume);
    }

    private void Start()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("Mixer_Music", 0.5f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    }

    void MusicVolume(float Value)
    {
        Mixer.SetFloat(Mixer_Music, Mathf.Log10(Value) * 20);
        PlayerPrefs.SetFloat("Mixer_Music", Value);
    }

    void SFXVolume(float Value)
    {
        Mixer.SetFloat(Mixer_SFX, Mathf.Log10(Value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", Value);
    }
}
