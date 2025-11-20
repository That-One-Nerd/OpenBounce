using UnityEngine;

public class ChipDropper : MonoBehaviour
{
    [Header("Visual Feedback")]
    [SerializeField] private Transform dropIndicator;
    private SpriteRenderer dropIndicatorRenderer;

    [Header("Settings")]
    [SerializeField] private GameObject chipPrefab;
    [SerializeField] private float dropAreaWidth = 6.2f;

    [Header("References")]
    [SerializeField] private Camera gameCamera;

    private bool _isInPlayArea = false;

    void Start()
    {
        if (dropIndicator != null)
        {
            dropIndicatorRenderer = dropIndicator.GetComponent<SpriteRenderer>();
            if (dropIndicatorRenderer != null)
                dropIndicatorRenderer.enabled = false;
        }
    }

    void Update()
    {
        Vector3 mouseWorldPos = gameCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        float halfWidth = dropAreaWidth / 2f;
        _isInPlayArea = Mathf.Abs(mouseWorldPos.x - transform.position.x) <= halfWidth;

        UpdateDropIndicator(mouseWorldPos.x);

        if (Input.GetMouseButtonDown(0) && CanDropChip())
        {
            DropChip(mouseWorldPos.x);
        }
    }

    private bool CanDropChip()
    {
        return _isInPlayArea && GameStateManager.Instance.BallsRemaining > 0;
    }

    private void DropChip(float mouseX)
    {
        Vector3 dropPosition = new Vector3(mouseX, transform.position.y, 0);
        Instantiate(chipPrefab, dropPosition, Quaternion.identity);
        GameStateManager.Instance.UseBall();
    }

    private void UpdateDropIndicator(float mouseX)
    {
        if (dropIndicator == null || dropIndicatorRenderer == null) return;

        if (CanDropChip())
        {
            dropIndicatorRenderer.enabled = true;
            dropIndicator.position = new Vector3(mouseX, transform.position.y, 0);
        }
        else
        {
            dropIndicatorRenderer.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(dropAreaWidth, 1f, 0));
    }
}
