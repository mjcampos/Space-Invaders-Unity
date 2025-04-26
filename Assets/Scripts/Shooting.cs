using System;
using UnityEngine;

public class Shooting : MonoBehaviour {
    Animator _animator;
    
    bool _isShooting = false;

    void Start() {
        _animator = GetComponent<Animator>();
    }

    void OnAttack() {
        if (!_isShooting) {
            _isShooting = true;
            _animator.Play("Player_shooting");
        }
    }

    void NoLongerShooting() {
        _isShooting = false;
    }
}
