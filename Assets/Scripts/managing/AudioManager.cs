using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;  // Singleton instance for easy access
    private AudioSource audioSource;      // Audio source for playing sounds

    void Awake()
    {
        // Ensure only one instance of AudioManager persists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // This makes sure the object doesn't get destroyed between scene loads
        }
        else
        {
            Destroy(gameObject);  // If there's already an instance, destroy this one
        }

        // Set up AudioSource if it's not already attached
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
