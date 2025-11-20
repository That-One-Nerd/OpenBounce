using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingsManager : MonoBehaviour
{
    public Slider masterVolumeSlider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        masterVolumeSlider.value = savedVolume;
        SetMasterVolume(savedVolume);
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
}
