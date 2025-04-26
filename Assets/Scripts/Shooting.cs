using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Shooting : MonoBehaviour {
    [SerializeField] GameObject laserPrefab;
    
    Animator _animator;
    
    bool _isShooting = false;
    Vector3 _laserPrefabStartingPosition;

    void Start() {
        _laserPrefabStartingPosition = laserPrefab.transform.position;
        
        _animator = GetComponent<Animator>();
    }

    void OnAttack() {
        if (!_isShooting) {
            // Mark the player as currently shooting
            _isShooting = true;
            
            // Generate laser
            GameObject laser = Instantiate(laserPrefab, transform.position + _laserPrefabStartingPosition, Quaternion.identity, transform);
            Laser laserScript = laser.GetComponent<Laser>();
            
            laser.transform.parent = null;
            laserScript.NeedToShootDownwards(false);
            
            _animator.Play("Player_shooting");
        }
    }

    void NoLongerShooting() {
        _isShooting = false;
    }
}
