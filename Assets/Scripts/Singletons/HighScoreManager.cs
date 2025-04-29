using System;
using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour {
    public static HighScoreManager Instance { get; private set; }
    
    [SerializeField] TextMeshProUGUI highScoreText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    void Start()
    {
        UpdateHighScoreDisplay();
    }

    void UpdateHighScoreDisplay()
    {
        highScoreText.text = "High Score: " + LoadScore();
    }

    int LoadScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
    
    public void SaveScore(int score)
    {
        if (score > LoadScore())
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        
        UpdateHighScoreDisplay();
    }
}
