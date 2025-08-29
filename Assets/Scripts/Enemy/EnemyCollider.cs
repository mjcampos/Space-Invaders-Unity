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
    [SerializeField] bool isBoss = false;
    
    Points _points;
    UFOPoints _ufoPoints;
    GroupTracker _groupTracker;

    void Start() {
        _points = GetComponent<Points>();
        _ufoPoints = GetComponent<UFOPoints>();
        _groupTracker = GetComponentInParent<GroupTracker>();
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bomb")) return;
        
        /*
         * When an enemy hits another object these events will happen
         * 1. Generate a explosion sound
         *      a. Destroy temp audio after sound finishes
         * 2. Generate an explosion animation
         * 3. Alert the points script to update the score manager
         *      a. If enemy is the UFO (boss) then alert UFOPoints script instead
         * 4. Alert the parent tracker to decrement child counter
         * 5. Destroy this enemy object
         */
        
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
        if (isBoss)
        {
            if (_ufoPoints != null)
            {
                _ufoPoints.SendPointsToScoreManager();
            }
        }
        else
        {
            if (_points != null) {
                _points.SendPointsToScoreManager();
            }
        }
        
        // Step 4
        if (_groupTracker)
        {
            _groupTracker.DecrementCounter();
        }
        
        // Step 5
        Destroy(gameObject);
    }
}
