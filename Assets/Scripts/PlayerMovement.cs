using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float speed = 5f;
    
    Rigidbody2D _rigidbody2D;
    Vector2 _movementInput, _defaultStartingPosition;
    float _minX, _maxX;
    Camera _camera;
    
    void Start() {
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
        
        float halfPlayerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        float screenHalfWidth = _camera.orthographicSize * _camera.aspect;
        float buffer = 0.1f;
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _minX = -screenHalfWidth + halfPlayerWidth + buffer;
        _maxX = screenHalfWidth - halfPlayerWidth - buffer;
        _defaultStartingPosition = transform.position;
    }

    void OnMove(InputValue value) {
        _movementInput = value.Get<Vector2>();
    }

    void FixedUpdate() {
        _rigidbody2D.linearVelocityX = _movementInput.x * speed;
        
        float clampedPositionX = Mathf.Clamp(_rigidbody2D.position.x, _minX, _maxX);
        
        _rigidbody2D.position = new Vector2(clampedPositionX, transform.position.y);
    }
}
