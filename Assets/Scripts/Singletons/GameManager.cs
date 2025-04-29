using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

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
        GameLoaded();
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
        Debug.Log("Player Won");
    }

    public void CountdownEnded()
    {
        Time.timeScale = 1f;
    }
}
