using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverSimple : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image defaultGIF;
    public Image hoverGIF;
    public AudioClip hoverSound;  // The sound to play when hovering

    void Start()
    {
        // Ensure only the default GIF is visible initially
        defaultGIF.enabled = true;
        hoverGIF.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        defaultGIF.enabled = false;
        hoverGIF.enabled = true;

        // Play the hover sound via the AudioManager
        AudioManager.Instance.PlayOneShot(hoverSound);  // Play the sound from the persistent AudioManager
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        defaultGIF.enabled = true;
        hoverGIF.enabled = false;
    }
}
