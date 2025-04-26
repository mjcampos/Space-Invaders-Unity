using System;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] int lives = 5;
    
    SpriteRenderer _spriteRenderer;
    Color _color;
    float _fullOpacityAmount;
    int _totalLives;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _fullOpacityAmount = _spriteRenderer.color.a;
        _totalLives = lives;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        lives--;
        _color = _spriteRenderer.color;
        _color.a = _fullOpacityAmount * ((float)lives/_totalLives);
        _spriteRenderer.color = _color;

        if (lives < 1) {
            Destroy(gameObject);
        }
    }
}
