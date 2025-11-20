using UnityEngine;

public class PegController : MonoBehaviour
{
    private Collider2D pegCollider;

    void Start()
    {
        // Get the collider component
        pegCollider = GetComponent<Collider2D>();
    }
}