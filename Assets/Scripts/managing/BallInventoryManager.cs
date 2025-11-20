using UnityEngine;

public class BallInventoryManager : MonoBehaviour
{
    public static BallInventoryManager Instance { get; private set; }

    [Header("Ball References")]
    [SerializeField] private GameObject[] ballIcons;
    [SerializeField] private float rotationSpeed = 360f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
        if (ballIcons == null || ballIcons.Length == 0)
        {
            var childBalls = new System.Collections.Generic.List<GameObject>();
            foreach (Transform child in transform)
            {
                childBalls.Add(child.gameObject);
            }
            ballIcons = childBalls.ToArray();
        }
    }

    void Update()
    {
        RotateActiveBalls();
    }

    public void UpdateBallDisplay(int activeCount)
    {
        if (ballIcons == null || ballIcons.Length == 0) return;

        activeCount = Mathf.Clamp(activeCount, 0, ballIcons.Length);

        for (int i = 0; i < ballIcons.Length; i++)
        {
            if (ballIcons[i] != null)
            {
                ballIcons[i].SetActive(i < activeCount);
            }
        }
    }

    private void RotateActiveBalls()
    {
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        foreach (var ball in ballIcons)
        {
            if (ball != null && ball.activeSelf)
            {
                ball.transform.Rotate(0, rotationThisFrame, 0, Space.World);
            }
        }
    }
}