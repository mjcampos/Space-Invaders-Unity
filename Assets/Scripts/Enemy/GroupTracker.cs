using System;
using System.Collections;
using UnityEngine;

public class GroupTracker : MonoBehaviour {
    [SerializeField] bool isTesting;

    int _counter;
    
    void Start() {
        _counter = isTesting ? 2 : transform.childCount;
    }

    public void DecrementCounter() {
        _counter--;
        
        if (_counter <= 0)
        {
            StartCoroutine(AlertGameManagerOfWin());
        }
    }

    IEnumerator AlertGameManagerOfWin()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.PlayerWon();
    }
}
