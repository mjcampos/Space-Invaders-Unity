using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownManager : MonoBehaviour {
    public static CountdownManager Instance { get; private set; }
    
    [SerializeField] TextMeshProUGUI countdownText;

    int _countdown = 3;
    
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
        countdownText.text = "";
    }

    public void StartCountdown()
    {
        _countdown = 3;
        
        StartCoroutine(Countdown());
    }
    
    IEnumerator Countdown() {
        yield return null;
        
        while (_countdown > 0) {
            countdownText.text = _countdown.ToString();
            yield return new WaitForSecondsRealtime(1f);
            _countdown--;
        }
        
        countdownText.text = _countdown.ToString();
    
        yield return new WaitForSecondsRealtime(0.5f);
    
        countdownText.text = "";
    
        // Notify game manager that countdown has ended
        GameManager.Instance.CountdownEnded();
    }
}
