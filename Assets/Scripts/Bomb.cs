using System;
using System.Linq;
using UnityEngine;

public class Bomb : MonoBehaviour {
    [SerializeField] GameObject explosionAnim;
    [SerializeField] GameObject explosionSound;
    
    Camera _camera;

    void Start()
    {
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
    }
    
    void Update()
    {
        // Check if the laser is outside the camera's view
        if (IsOffScreen())
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*
         * When a bomb hits another object these events will happen
         * 1. Check if target hitting is invalid, if so don't proceed
         * 2. Generate a explosion sound
         *      a. Destroy temp audio after sound finishes
         * 3. Generate an explosion animation
         * 4. Destroy this bomb object
         */

        // Step 1
        if (other.CompareTag("Enemy"))
        {
            return;
        }
        
        // Step 2
        GameObject tempAudio = Instantiate(explosionSound, transform.position, Quaternion.identity);
        AudioSource audioSource = tempAudio.GetComponent<AudioSource>();
        
        audioSource.Play();
        
        // Step 2 - a
        Destroy(tempAudio, audioSource.clip.length);;
        
        // Step 3
        GameObject explosionInstance = Instantiate(explosionAnim, transform.position, Quaternion.identity);
        
        explosionInstance.transform.SetParent(null);
        
        // Step 4
        Destroy(gameObject);
    }

    bool IsOffScreen()
    {
        // Convert bomb's position to viewport space
        Vector3 viewportPos = _camera.WorldToViewportPoint(transform.position);
        
        // Check if it's outside the camera's visible area
        return viewportPos.y < 0f;
    }
}
