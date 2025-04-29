using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {
    [SerializeField] InputActionAsset inputActionAsset;
    
    InputActionMap _uiInputActionMap;
    InputAction _backInputAction;

    void Start()
    {
        _uiInputActionMap = inputActionAsset.FindActionMap("UI");
        
        _backInputAction = _uiInputActionMap.FindAction("Back");
    }

    void Update()
    {
        BackListener();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    void BackListener()
    {
        if (_backInputAction.triggered)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
