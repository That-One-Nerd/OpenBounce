using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchButton : MonoBehaviour
{
    public string gameSceneName = "GameScene"; // Name of your gameplay scene

    public void OnClick()
    {
        Debug.Log("StartGameButton clicked! Attempting to load scene: " + gameSceneName);

        if (string.IsNullOrEmpty(gameSceneName))
        {
            Debug.LogError("Scene name is empty! Assign a scene in the Inspector.");
            return;
        }

        if (Application.CanStreamedLevelBeLoaded(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogError("Scene '" + gameSceneName + "' does not exist or isn't in Build Settings!");
        }
    }
}