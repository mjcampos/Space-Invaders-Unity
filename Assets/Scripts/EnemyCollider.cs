using System;
using UnityEngine;

enum Size {
    Small = 1,
    Medium = 2,
    Large = 3
}

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] Size size;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);

            explosionInstance.transform.localScale = Vector3.one * (int)size;
            explosionInstance.transform.SetParent(null);
            Destroy(gameObject);
        }
    }
}
