using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer Mixer;
    [SerializeField] Slider MusicSlider, SFXSlider;

    const string Mixer_Music = "MusicVolume";
    const string Mixer_SFX = "SFXVolume";

    private void Awake()
    {
        MusicSlider.onValueChanged.AddListener(MusicVolume);
        SFXSlider.onValueChanged.AddListener(SFXVolume);
    }

    private void Start()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("Mixer_Music", 1f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
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
