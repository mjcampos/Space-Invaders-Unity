using System;
using UnityEngine;

public class Shooting : MonoBehaviour {
    [SerializeField] GameObject laserPrefab;
    
    Animator _animator;
    
    bool _isShooting = false;
    Vector3 _laserPrefabStartingPosition;

    void Start() {
        _animator = GetComponent<Animator>();
    }

    void OnAttack() {
        if (!_isShooting) {
            // Mark the player as currently shooting
            _isShooting = true;
            
            // Get the position we want the laser to spawn in
            _laserPrefabStartingPosition = laserPrefab.transform.position + transform.position;
            
            // Generate laser
            GameObject laser = Instantiate(laserPrefab, _laserPrefabStartingPosition, Quaternion.identity, transform);
            
            laser.transform.parent = null;
            
            _animator.Play("Player_shooting");
        }
    }

    void NoLongerShooting() {
        _isShooting = false;
    }
}
