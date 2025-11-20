using UnityEngine;
using System.Collections;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance { get; private set; }

    private GameObject currentGhost;
    private GameObject currentPrefab;
    private bool canPlace = false;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator EnablePlacementNextFrame()
    {
        yield return null;
        canPlace = true;
    }

    public void BeginPlacement(GameObject prefab)
    {
        if (currentGhost != null) Destroy(currentGhost);

        currentPrefab = prefab;
        currentGhost = Instantiate(prefab);
        Debug.Log($"Ghost created: {currentGhost.name}");

        // Make the ghost semi-transparent
        SpriteRenderer sr = currentGhost.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color ghostColor = sr.color;
            ghostColor.a = 0.5f;
            sr.color = ghostColor;
        }

        // Disable interaction with physics for the ghost
        Collider2D col = currentGhost.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        canPlace = false;
        StartCoroutine(EnablePlacementNextFrame());
    }

    private void Update()
    {
        if (currentGhost == null) return;

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentGhost.transform.position = mouseWorldPos;

        if (canPlace && Input.GetMouseButtonUp(0))
        {
            PlaceItem(mouseWorldPos);
        }
    }

    private void PlaceItem(Vector2 mouseWorldPosition)
    {
        GameObject snapSpot = FindClosestPlaceholder(mouseWorldPosition);

        if (snapSpot != null)
        {
            Vector3 worldPosition = snapSpot.transform.position;

            // Use the gameBoardContainer from GameStateManager as parent
            GameObject newItem = Instantiate(currentPrefab, worldPosition, Quaternion.identity, GameStateManager.Instance.gameBoardContainer.transform);

            Debug.Log($"Item placed at snap spot: {snapSpot.name}");
            GameStateManager.Instance.AdvanceLevel(); // Move to next level, reset state
        }
        else
        {
            Debug.Log("No valid snap spot found. Placement canceled.");
            return;
        }

        Destroy(currentGhost);
        currentGhost = null;
        currentPrefab = null;
    }


    private GameObject FindClosestPlaceholder(Vector2 mouseWorldPos)
    {
        GameObject[] placeholders = GameObject.FindGameObjectsWithTag("PlacementSpot");
        float maxDistance = 1.5f; // world units
        GameObject closest = null;
        float closestDist = float.MaxValue;

        foreach (GameObject spot in placeholders)
        {
            float dist = Vector2.Distance(mouseWorldPos, spot.transform.position);
            if (dist < maxDistance && dist < closestDist)
            {
                closest = spot;
                closestDist = dist;
            }
        }

        return closest;
    }
}
