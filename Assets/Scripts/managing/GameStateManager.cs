using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;


public class GameStateManager : MonoBehaviour
{
    // Singleton instance
    public static GameStateManager Instance { get; private set; }

    [Header("Game Containers")]
    [SerializeField] public GameObject gameBoardContainer;
    [SerializeField] private GameObject shopContainer;
    [SerializeField] private GameObject failureContainer;
    [SerializeField] private GameObject winContainer;



    // Game state variables
    [Header("Game State")]
    [SerializeField] public int _currentLevel = 0;
    [SerializeField] public int _totalScore = 0;
    [SerializeField] private int _targetScore = 100;
    [SerializeField] private int _ballsRemaining = 6;
    [SerializeField] private float _levelMultiplier = 1.4f;

    // UI References
    [Header("UI Elements")]
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _targetText;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _ballsText;
    
    [Header("trigger stuff")]
    [SerializeField] private GameObject[] triggerPrefabs;  // Array to hold your trigger prefabs


    // Ball tracking for win logic
    private int _ballsDropped = 0;
    private int _activeBalls = 0;

    // Public accessors
    public int CurrentLevel => _currentLevel;
    public int TotalScore => _totalScore;
    public int TargetScore => _targetScore;
    public int BallsRemaining => _ballsRemaining;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // No more DontDestroyOnLoad()
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        RefreshTargetScore();
        UpdateAllUI();
    }

    // Score handling
    public void AddScore(int points)
    {
        _totalScore += points;
        UpdateScoreUI();
    }

    // Called when a ball is dropped
    public void UseBall()
    {
        _ballsRemaining--;
        _ballsDropped++;
        _activeBalls++;
        UpdateBallsUI();

        // Iterate through each trigger
        foreach (TriggerLogic trigger in FindObjectsOfType<TriggerLogic>())
        {
            if (trigger != null)
            {
                // Call the method to multiply points if needed
                trigger.onBallDroppedEarnPoints();
            }
        }
    }

    // Called when a ball is destroyed or hits a hole
    public void BallFinished()
    {
        _activeBalls--;
        CheckLevelCompletion();
    }

    // Level advance
    public void AdvanceLevel()
    {
        _currentLevel++;
        _totalScore = 0;
        _ballsRemaining = 6;
        _ballsDropped = 0;
        _activeBalls = 0;
        RefreshTargetScore();
        UpdateAllUI();

        // Only modify triggers actually in the scene
        foreach (TriggerLogic trigger in FindObjectsOfType<TriggerLogic>())
        {
            if (trigger.increaseMultPerRound)
            {
                trigger.multiplyPointsValue();
            }
        }
    }

    private void RefreshTargetScore()
    {
        _targetScore = _currentLevel == 0
            ? _targetScore
            : Mathf.RoundToInt(_levelMultiplier * _targetScore);
    }

    // Only checks level when no balls are left and none are active
    private void CheckLevelCompletion()
    {
        if (_ballsRemaining <= 0 && _activeBalls <= 0)
        {
            if (_totalScore >= _targetScore && _currentLevel >= 19)
            {
                Debug.Log("Level complete! WON");
                ShowWin();
            }
            else if (_totalScore >= _targetScore)
            {
                Debug.Log("Level complete!");
                ShowShop();
            }
            else
            {
                Debug.Log("Level failed!");
                ShowFailureScreen();
            }
        }
    }

    private void ShowWin()
    {
        if (gameBoardContainer != null)
            gameBoardContainer.SetActive(false);

        if (shopContainer != null)
            shopContainer.SetActive(false);

        if (winContainer != null)
            winContainer.SetActive(true);
    }

    private void ShowShop()
    {
        if (gameBoardContainer != null)
            gameBoardContainer.SetActive(false);

        if (shopContainer != null)
            shopContainer.SetActive(true);
    }

    private void ShowFailureScreen()
    {
        if (gameBoardContainer != null)
            gameBoardContainer.SetActive(false);

        if (failureContainer != null)
            failureContainer.SetActive(true);
    }



    // UI helpers
    private void UpdateAllUI()
    {
        UpdateScoreUI();
        UpdateTargetUI();
        UpdateLevelUI();
        UpdateBallsUI();
    }

    private void UpdateScoreUI()
    {
        if (_scoreText) _scoreText.text = $"Score: {_totalScore}";
    }

    private void UpdateTargetUI()
    {
        if (_targetText) _targetText.text = $"Target: {_targetScore}";
    }

    private void UpdateLevelUI()
    {
        if (_levelText) _levelText.text = $"Level: {_currentLevel + 1}";
    }

    private void UpdateBallsUI()
    {
        if (_ballsText) _ballsText.text = $"Balls: {_ballsRemaining}";
        if (BallInventoryManager.Instance != null)
            BallInventoryManager.Instance.UpdateBallDisplay(_ballsRemaining);
    }

    public void ResetGameState()
    {
        _currentLevel = 0;
        _totalScore = 0;
        _ballsRemaining = 6;
        _ballsDropped = 0;
        _activeBalls = 0;
        RefreshTargetScore();
        UpdateAllUI();
    }

    public void ReconnectUI(Text scoreText, Text targetText, Text levelText, Text ballsText)
    {
        _scoreText = scoreText;
        _targetText = targetText;
        _levelText = levelText;
        _ballsText = ballsText;
        UpdateAllUI();
    }

    public void ReturnToBoardFromShop()
    {
        if (shopContainer != null)
            shopContainer.SetActive(false);

        if (gameBoardContainer != null)
            gameBoardContainer.SetActive(true);
    }

}
