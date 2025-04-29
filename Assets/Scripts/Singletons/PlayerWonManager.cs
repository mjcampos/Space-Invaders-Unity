using System;
using TMPro;
using UnityEngine;

public class PlayerWonManager : MonoBehaviour {
    public static PlayerWonManager Instance { get; private set; }
    
    [SerializeField] GameObject winText;

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
        winText.SetActive(false);
    }
    
    public void ShowWinText()
    {
        winText.SetActive(true);
    }
}
