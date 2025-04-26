using System;
using UnityEngine;

public class EnemyCollider : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(gameObject);
        }
    }
}
