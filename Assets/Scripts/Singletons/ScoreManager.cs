using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI scoreText;
    
    int _score = 0;

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    void Start()
    {
        UpdateScoreDisplay();
    }

    public void AddScore(int points)
    {
        _score += points;
        
        UpdateScoreDisplay();
    }

    public int GetScore()
    {
        return _score;   
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + _score.ToString();
    }

    public void ResetScore()
    {
        _score = 0;
        UpdateScoreDisplay();
    }
}
