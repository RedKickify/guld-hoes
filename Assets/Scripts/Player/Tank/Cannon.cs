using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private PlayerTypeController _playerTypeController;
    private Animator _animator;
    void Start()
    {
        _playerTypeController = GetComponentInParent<PlayerTypeController>();
        _animator = GetComponentInParent<Animator>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _animator.SetTrigger("ShootCannonTrigger");
    }
}
