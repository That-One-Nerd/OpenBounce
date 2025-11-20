using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    public GameObject settingsCanvas;
    public GameObject mainCanvas; // Reference to your main menu or game UI canvas

    private void Start()
    {
        if (settingsCanvas != null)
            settingsCanvas.SetActive(false);
    }

    public void ToggleSettings()
    {
        bool isSettingsActive = !settingsCanvas.activeSelf;

        settingsCanvas.SetActive(isSettingsActive);

        if (mainCanvas != null)
            mainCanvas.SetActive(!isSettingsActive);

        Time.timeScale = isSettingsActive ? 0 : 1;
    }
}
