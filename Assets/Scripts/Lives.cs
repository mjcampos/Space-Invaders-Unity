using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lives : MonoBehaviour {
    int _lives = 3;
    Gamepad _gamepad;

    void Start()
    {
        _gamepad = Gamepad.current;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If player has gamepad, give them a rumble
        GiveGamePadARumble();
        
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
            _gamepad.SetMotorSpeeds(0.4f, 0.8f);
            
            // Stop vibration after short delay
            StartCoroutine(StopRumbleAfterDelay(0.4f));
        }
    }

    void OnApplicationQuit()
    {
        if (_gamepad != null)
        {
            _gamepad.SetMotorSpeeds(0, 0);
        }
    }
}
