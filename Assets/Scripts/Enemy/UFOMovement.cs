using System.Collections;
using UnityEngine;

public class UFOMovement : MonoBehaviour {
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float minDelay = 2f;
    [SerializeField] float maxDelay = 10f;

    [Header("Audio")] 
    [SerializeField] float fadeOutDuration = 1f;
    
    Vector3 _startingPosition;
    Camera _camera;
    AudioSource _audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
        _audioSource = GetComponent<AudioSource>();
        _startingPosition = transform.position;
        
        StartCoroutine(UFOLoop());
    }

    IEnumerator UFOLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);

            yield return new WaitForSeconds(waitTime);
            
            // Play UFO sound
            if (_audioSource != null)
            {
                _audioSource.volume = 1f;
                _audioSource.Play();
            }
            
            // Move until off right side of screen
            while (IsVisibleOnScreen())
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                yield return null;
            }
            
            // Stop sound
            if (_audioSource != null)
            {
                yield return StartCoroutine(FadeOutAudio());
                _audioSource.Stop();
            }
            
            // Teleport back to starting position
            transform.position = _startingPosition;
        }
    }

    bool IsVisibleOnScreen()
    {
        Vector3 viewportPos = _camera.WorldToViewportPoint(transform.position);
        
        return viewportPos.x < 1.1f;
    }

    // Coroutine to fade audio volume to 0 over the fade-out duration
    IEnumerator FadeOutAudio()
    {
        float startVolume = _audioSource.volume;

        for (float t = 0f; t < fadeOutDuration; t += Time.deltaTime)
        {
            _audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeOutDuration);
            yield return null;
        }
    }
}
