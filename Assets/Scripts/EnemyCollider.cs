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
    [SerializeField] GameObject explosionSound;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Instantiate a temporary audio source at the enemy's position
        GameObject tempAudio = Instantiate(explosionSound, transform.position, Quaternion.identity);
        AudioSource audioSource = tempAudio.GetComponent<AudioSource>();

        audioSource.Play();
        
        // Destroy temp audio object after sound finishes
        Destroy(tempAudio, audioSource.clip.length);;
        
        if (other.gameObject.CompareTag("Laser"))
        {
            GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);

            explosionInstance.transform.localScale = Vector3.one * (int)size;
            explosionInstance.transform.SetParent(null);
            Destroy(gameObject);
        }
    }
}
