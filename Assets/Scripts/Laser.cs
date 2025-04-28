using System;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    
    Rigidbody2D _rigidbody2D;
    Camera _camera;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = Vector2.up * speed;
        
        // Check if the laser is outside the camera's view
        if (IsOffScreen())
        {
            Destroy(gameObject);
        }
    }

    bool IsOffScreen()
    {
        // Convert laser's position to viewport space
        Vector3 viewportPos = _camera.WorldToViewportPoint(transform.position);
        
        // Check if the laser is outside the camera's visible area
        return viewportPos.y > 1f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
