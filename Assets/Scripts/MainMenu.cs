using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField] TextMeshProUGUI highScoreText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        LoadHighScore();
    }

    void LoadHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        highScoreText.text = "High Score: " + highScore;
    }
}
