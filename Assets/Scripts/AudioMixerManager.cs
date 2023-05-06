using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer Mixer;
    [SerializeField] Slider MusicSlider, UISlider;

    const string Mixer_Music = "MusicVolume";
    //const string Mixer_SFX = "SFXVolume";
    const string Mixer_UI = "UIVolume";

    private void Awake()
    {
        MusicSlider.onValueChanged.AddListener(MusicVolume);
        UISlider.onValueChanged.AddListener(UIVolume);
    }

    private void Start()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("Mixer_Music", 1f);
        UISlider.value = PlayerPrefs.GetFloat("UIVolume", 1f);
    }

    void MusicVolume(float Value)
    {
        Mixer.SetFloat(Mixer_Music, Mathf.Log10(Value) * 20);
        PlayerPrefs.SetFloat("Mixer_Music", Value);
    }

    void UIVolume(float Value)
    {
        Mixer.SetFloat(Mixer_UI, Mathf.Log10(Value) * 20);
        PlayerPrefs.SetFloat("UIVolume", Value);
    }
}
