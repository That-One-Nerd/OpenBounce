using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        ApplyVolumes();
    }

    void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        ApplyVolumes();
    }

    void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        ApplyVolumes();
    }

    void ApplyVolumes()
    {
        AudioListener.volume = 1f; // Ensure global volume isn't muted

        var musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        var sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Assuming you tag your music source as "Music"
        GameObject musicSource = GameObject.FindWithTag("Music");
        if (musicSource != null)
            musicSource.GetComponent<AudioSource>().volume = musicVolume;

        // You can store and apply SFX volume per AudioSource if needed.
    }
}
