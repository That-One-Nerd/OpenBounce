using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{

    public void OnClick()
    {
        quitting();

    }

    public static void quitting() {
        Debug.Log("applicayion quit");
        Application.Quit();

    }
}