using System;
using UnityEngine;

public class GroupTracker : MonoBehaviour {
    [SerializeField] bool isTesting;

    int _counter;
    
    void Start() {
        _counter = isTesting ? 2 : transform.childCount;
    }

    public void DecrementCounter() {
        _counter--;
        
        if (_counter <= 0) {
            Debug.Log("Group Destroyed");
        }
    }
}
