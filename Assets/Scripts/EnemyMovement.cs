using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [Header("Movement Settings")] 
    [SerializeField] float initialMoveDelay = 1.0f;
    [SerializeField] float moveDistance = 0.2f;
    [SerializeField] float dropDistance = 0.5f;
    [SerializeField] float moveInterval = 1.0f;
    [SerializeField] float accelerationRate = 0.01f;  // How much faster after each move
    
    [Header("Sound Settings")] 
    [SerializeField] AudioClip[] moveSounds;
    
    AudioSource _audioSource;
    int _currentMoveSoundIndex = 0;
    
    bool _movingRight = true;
    float _moveTimer;
    float _rightEdge = 3.0f;
    float _leftEdge = -3.0f;

    enum  State {
        Move,
        Drop
    }

    State _nextMove;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _audioSource = GetComponent<AudioSource>();
        
        _moveTimer = initialMoveDelay;
        _nextMove = State.Move;
    }

    // Update is called once per frame
    void Update() {
        _moveTimer -= Time.deltaTime;

        if (_moveTimer <= 0f) {
            MoveEnemies();
            _moveTimer = moveInterval;
        }
    }

    void MoveEnemies() {
        /*
         * When this method gets called the enemies will do either:
         * 1. Move
         * 2. Drop
         */

        switch (_nextMove) {
            case State.Move:
                Vector3 moveDirection = _movingRight ? Vector3.right : Vector3.left;
                
                transform.Translate(moveDirection * moveDistance);
                
                float xPos = transform.position.x;
                float clampedX = Mathf.Clamp(xPos, _leftEdge, _rightEdge);
                transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
                
                _nextMove = (transform.position.x >= _rightEdge || transform.position.x <= _leftEdge) ? State.Drop : State.Move;
                
                break;
            
            case State.Drop:
                transform.Translate(Vector3.down * dropDistance);
                _movingRight = !_movingRight;  // Flip direction
                _nextMove = State.Move;
                break;
        }
        
        
        PlayMoveSound();
        
        // Smoothly accelerate movement
        moveInterval = Mathf.Max(0.1f, moveInterval - accelerationRate);
    }
    
    void PlayMoveSound() {
        if (moveSounds.Length > 0) {
            _audioSource.PlayOneShot(moveSounds[_currentMoveSoundIndex]);
            
            _currentMoveSoundIndex = (_currentMoveSoundIndex + 1) % moveSounds.Length;
        }
    }
}
