using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    public float scoreMultiplier = 1; // Set this in the Inspector for each hole
    public AudioClip holeEntrySound;

    private AudioSource audioSource;

    void Start()
    {
        if (!TryGetComponent(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chip"))
        {
            ChipController chip = other.GetComponent<ChipController>();
            if (chip != null)
            {
                int finalScore = Mathf.RoundToInt(chip.pegHits * scoreMultiplier);
                GameStateManager.Instance.AddScore(finalScore);
            }
            Destroy(other.gameObject);

            // Play hole entry sound using the AudioManager
            if (holeEntrySound != null)
            {
                AudioManager.Instance.PlayOneShot(holeEntrySound);  // Play the sound via AudioManager
            }
        }
    }
}
