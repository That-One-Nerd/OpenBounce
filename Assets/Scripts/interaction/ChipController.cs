using UnityEngine;

public class ChipController : MonoBehaviour
{
    public int pegHits = 0;
    public AudioClip collisionSound;  // Collision sound that plays when chip hits a peg
    public float bounciness = 0.8f;
    public float soundCooldown = 0.05f;

    private AudioSource audioSource;
    private Rigidbody2D rb;
    private float lastSoundTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetBounciness(bounciness);
        lastSoundTime = -soundCooldown;
    }

    void SetBounciness(float newBounciness)
    {
        PhysicsMaterial2D material = new PhysicsMaterial2D
        {
            bounciness = newBounciness,
            friction = 0.1f
        };
        rb.sharedMaterial = material;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionSound != null && Time.time >= lastSoundTime + soundCooldown)
        {
            // Use the AudioManager to play the sound
            AudioManager.Instance.PlayOneShot(collisionSound);  // Play the sound via AudioManager

            lastSoundTime = Time.time;
        }

        if (collision.gameObject.CompareTag("Peg"))
        {
            pegHits++;
            GameStateManager.Instance.AddScore(1);
        }
    }
}
