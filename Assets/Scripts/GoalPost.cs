using System;
using System.Collections;
using UnityEngine;

public class GoalPost : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(WaitBeforeAlertingGameManager());
        }
    }

    IEnumerator WaitBeforeAlertingGameManager()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.PlayerLost();
    }
}
