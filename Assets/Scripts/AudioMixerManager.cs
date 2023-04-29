using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer Mixer;
    [SerializeField] Slider MusicSlider;

    const string Mixer_Music = "MusicVolume";

    private void Awake()
    {
        MusicSlider.onValueChanged.AddListener(MusicVolume);
    }

    private void Start()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("Mixer_Music", 1f);
    }

    void MusicVolume(float Value)
    {
        Mixer.SetFloat(Mixer_Music, Mathf.Log10(Value) * 20);
        PlayerPrefs.SetFloat("Mixer_Music", Value);
    }
}
