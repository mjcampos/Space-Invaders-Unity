using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] GameObject bombPrefab;
    [SerializeField] int minShootDelay = 2;
    [SerializeField] int maxShootDelay = 10;
    [SerializeField] LayerMask enemyLayer;
    
    [Header("Overlap Circle Detection Settings")]
    [SerializeField] Vector2 capsuleSize = new Vector2(0.5f, 1f);  // Width and height of the capsule
    [SerializeField] float verticalOffset = 0.5f;  // How far below the enemy to check
    
    float _shootTimer;
    Vector3 _bombPrefabStartingPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        ResetShootTimer();
    }

    // Update is called once per frame
    void Update() {
        _shootTimer -= Time.deltaTime;

        if (_shootTimer <= 0f) {
            if (CanShoot()) {
                Shoot();
            }

            ResetShootTimer();
        }
    }

    bool CanShoot() {
        Vector3 checkPosition = transform.position + Vector3.down * verticalOffset;
        Collider2D hit = Physics2D.OverlapCapsule(
            checkPosition,
            capsuleSize,
            CapsuleDirection2D.Vertical,  // Direction of the capsule
            0f,
            enemyLayer
        );
        
        /*
         * If the hit collider detects something then it's another enemy
         * In that case we return false because we don't want to hit it
         * Otherwise it's not a fellow enemy and enemy is allowed to hit
         */
        return hit == null;
    }

    void Shoot() {
        // Get the position we want the bomb to spawn in
        _bombPrefabStartingPosition = bombPrefab.transform.position + transform.position;
        
        // Generate bomb
        GameObject bomb = Instantiate(bombPrefab, _bombPrefabStartingPosition, Quaternion.identity, transform);
        
        bomb.transform.parent = null;
    }

    void ResetShootTimer() {
        _shootTimer = Random.Range(minShootDelay, maxShootDelay);
    }
    
    void OnDrawGizmosSelected() {
        Vector3 checkPosition = transform.position + Vector3.down * verticalOffset;
        Gizmos.color = CanShoot() ? Color.green : Color.red;
        Gizmos.DrawWireCube(checkPosition, capsuleSize);
    }
}
