using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadGameScene : MonoBehaviour
{
    public void Reload()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // Fully resets the scene
    }
}
