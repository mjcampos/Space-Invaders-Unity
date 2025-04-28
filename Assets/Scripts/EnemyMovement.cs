using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")] 
    [SerializeField] float initialMoveDelay = 1.0f;
    [SerializeField] float moveDistance = 0.2f;
    [SerializeField] float dropDistance = 0.5f;
    [SerializeField] float moveInterval = 1.0f;
    [SerializeField] float speedIncreaseFactor = 0.9f;
    [SerializeField] float accelerationRate = 0.01f;  // How much faster after each move

    [Header("Sound Settings")] 
    [SerializeField] AudioClip[] moveSounds;
    
    AudioSource _audioSource;
    int _currentMoveSoundIndex = 0;
    
    bool _movingRight = true;
    float _moveTimer;
    Camera _camera;
    
    EnemySpawner _enemySpawner;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Before we even consider moving the group we need to ensure they spawn before anything else
        _enemySpawner = GetComponent<EnemySpawner>();
        
        _enemySpawner.StartSpawning();
        
        // Initialize everything
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
        _audioSource = GetComponent<AudioSource>();
        
        _moveTimer = initialMoveDelay;
    }

    // Update is called once per frame
    void Update()
    {
        _moveTimer -= Time.deltaTime;

        if (_moveTimer <= 0f)
        {
            MoveEnemies();
            _moveTimer = moveInterval;
        }
    }

    void MoveEnemies()
    {
        Vector3 moveDirection = _movingRight ? Vector3.right : Vector3.left;
        
        transform.position += moveDirection * moveDistance;

        PlayMoveSound();
        
        // Smoothly accelerate movement
        moveInterval = Mathf.Max(0.1f, moveInterval - accelerationRate);

        if (ReachedScreenEdge())
        {
            _movingRight = !_movingRight;  // Flip direction
            transform.position += Vector3.down * dropDistance;  // Drop down
        }
    }

    void PlayMoveSound()
    {
        if (moveSounds.Length > 0)
        {
            _audioSource.PlayOneShot(moveSounds[_currentMoveSoundIndex]);
            
            _currentMoveSoundIndex = (_currentMoveSoundIndex + 1) % moveSounds.Length;
        }
    }
    
    bool ReachedScreenEdge()
    {
        foreach (Transform enemy in transform)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                continue;
            }

            Vector3 enemyViewportPos = _camera.WorldToViewportPoint(enemy.position);

            if (_movingRight && enemyViewportPos.x >= 0.95f)  // Near right edge
            {
                return true;
            } else if (!_movingRight && enemyViewportPos.x <= 0.05f)  // Near left edge
            {
                return true;
            }
        }
        
        return false;
    }
}
