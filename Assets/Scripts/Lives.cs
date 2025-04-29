using System;
using System.Collections;
using UnityEngine;

public class Lives : MonoBehaviour {
    int _lives = 3;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Decrement a life
        _lives--;
        
        LivesDisplayManager.Instance.RemoveLife();

        if (_lives < 1)
        {
            StartCoroutine(DelayBeforeNotification());
        }
    }

    IEnumerator DelayBeforeNotification()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.PlayerLost();
    }
}
