using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lives : MonoBehaviour
{
    [Header("Explosion Manager")]
    [SerializeField] GameObject explosionAnim;
    [SerializeField] GameObject explosionSound;
    
    [Header("Lives Manager")]
    [SerializeField, Range(1, 3)] int lives = 3;
    
    Gamepad _gamepad;

    void Start()
    {
        _gamepad = Gamepad.current;
        
        LivesDisplayManager.Instance.SetLivesDisplay(lives);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If player has gamepad, give them a rumble
        GiveGamePadARumble();
        
        // Decrement a life
        lives--;
        
        LivesDisplayManager.Instance.RemoveLife(lives);

        if (lives < 1)
        {
            PlayerDestructionSequence();
        }
    }

    void PlayerDestructionSequence()
    {
        /*
         * When player gets destroyed trigger following events:
         * 1. Generate an explosion sound
         *      a. Destroy temp audio after sound finishes
         * 2. Generate an explosion animation
         * 3. Destroy the player
         */
        
        // Step 1
        GameObject tempAudio = Instantiate(explosionSound, transform.position, Quaternion.identity);
        AudioSource audioSource = tempAudio.GetComponent<AudioSource>();
        
        audioSource.Play();
        
        // Step 1 - a
        Destroy(tempAudio, audioSource.clip.length);
        
        // Step 2
        GameObject explosionInstance = Instantiate(explosionAnim, transform.position, Quaternion.identity);

        explosionInstance.transform.localScale = Vector3.one * 4;
        explosionInstance.transform.SetParent(null);
        
        // Step 3
        Destroy(gameObject);
    }

    IEnumerator StopRumbleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _gamepad.SetMotorSpeeds(0, 0);
    }

    void GiveGamePadARumble()
    {
        if (_gamepad != null)
        {
            // Set vibration: LeftMotor (low freq), rightMotor (high freq)
            _gamepad.SetMotorSpeeds(0.5f, 0.5f);
            
            // Stop vibration after short delay
            StartCoroutine(StopRumbleAfterDelay(0.3f));
        }
    }

    void OnApplicationQuit()
    {
        CleanUp();
    }

    void OnDestroy()
    {
        CleanUp();
    }

    void CleanUp()
    {
        if (_gamepad != null)
        {
            _gamepad.SetMotorSpeeds(0, 0);
        }
    }
}
