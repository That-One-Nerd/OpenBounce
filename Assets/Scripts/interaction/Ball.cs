using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    private void OnDestroy()
    {
        GameStateManager.Instance.BallFinished();
        Debug.Log("ball finished");

    }

    public int wallBounces = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            wallBounces++;
        }
    }
}
