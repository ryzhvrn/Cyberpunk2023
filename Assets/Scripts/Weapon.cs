using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private const string ShootAnimation = "Shoot";

    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;

    private Animator _animator;

    private void Start()
    {
        _animator= GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.Play(ShootAnimation);
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }
}
