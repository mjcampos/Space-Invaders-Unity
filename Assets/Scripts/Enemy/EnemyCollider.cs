using System;
using UnityEngine;
using UnityEngine.Serialization;

enum Size {
    Small = 1,
    Medium = 2,
    Large = 3
}

public class EnemyCollider : MonoBehaviour {
    [FormerlySerializedAs("explosion")] [SerializeField] GameObject explosionAnim;
    [SerializeField] Size size;
    [SerializeField] GameObject explosionSound;
    
    void OnTriggerEnter2D(Collider2D other) {
        /*
         * When an enemy hits another object these events will happen
         * 1. Generate a explosion sound
         *      a. Destroy temp audio after sound finishes
         * 2. Generate an explosion animation
         * 3. Destroy this enemy object
         */

        if (other.CompareTag("Bomb"))
        {
            return;
        }
        
        // Step 1
        GameObject tempAudio = Instantiate(explosionSound, transform.position, Quaternion.identity);
        AudioSource audioSource = tempAudio.GetComponent<AudioSource>();
        
        audioSource.Play();
        
        // Step 1 - a
        Destroy(tempAudio, audioSource.clip.length);;
        
        // Step 2
        GameObject explosionInstance = Instantiate(explosionAnim, transform.position, Quaternion.identity);

        explosionInstance.transform.localScale = Vector3.one * (int)size;
        explosionInstance.transform.SetParent(null);
        
        // Step 3
        Destroy(gameObject);
    }
}
