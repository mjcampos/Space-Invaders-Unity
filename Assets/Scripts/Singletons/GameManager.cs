using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    
    [SerializeField] InputActionAsset inputActionAsset;
    
    InputActionMap _uiInputActionMap;
    InputAction _restartInputAction;
    InputAction _backInputAction;
    
    bool _hasOptionToRestart = false;

    void Awake()
    {
        // Singleton enforcement
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    void Start()
    {
        _uiInputActionMap = inputActionAsset.FindActionMap("UI");
        
        _restartInputAction = _uiInputActionMap.FindAction("Restart");
        _backInputAction = _uiInputActionMap.FindAction("Back");
        
        GameLoaded();
    }

    void Update()
    {
        RestartListener();
        BackListener();
    }

    void GameLoaded()
    {
        /*
         * When the player first loads the game perform the following sequence:
         * 1. Reset the score
         * 2. Freeze the game
         * 3. Countdown to start
         * 4. Unfreeze the game
         */
        
        // Step 1
        ScoreManager.Instance.ResetScore();
        
        // Step 2
        Time.timeScale = 0f;
        
        // Step 3
        CountdownManager.Instance.StartCountdown();
        
        /*
         * Step 4
         * After countdown ends, execute CountdownEnded()
         */
    }

    public void PlayerWon()
    {
        /*
         * When player has won perform the following sequence:
         * 1. Freeze the game
         * 2. Alert the player that they won
         * 3. Give them the option to play again
         * 4. Save score to high score manager
         */
        
        // Step 1
        Time.timeScale = 0f;
        
        // Step 2
        WonManager.Instance.ShowWinText();
        
        // Step 3
        _hasOptionToRestart = true;
        
        // Step 4
        int score = ScoreManager.Instance.GetScore();
        
        HighScoreManager.Instance.SaveScore(score);
    }

    public void PlayerLost()
    {
        /*
         * When player has lost perform the following sequence:
         * 1. Freeze the game
         * 2. Alert the player that they lost
         * 3. Give them the option to play again
         * 4. Save score to high score manager
         */
        
        // Step 1
        Time.timeScale = 0f;
        
        // Step 2
        LostManager.Instance.ShowLostText();
        
        // Step 3
        _hasOptionToRestart = true;
        
        // Step 4
        int score = ScoreManager.Instance.GetScore();
        
        HighScoreManager.Instance.SaveScore(score);
    }

    public void CountdownEnded()
    {
        Time.timeScale = 1f;
    }

    void RestartListener()
    {
        if (_restartInputAction.triggered && _hasOptionToRestart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void BackListener()
    {
        if (_backInputAction.triggered)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
