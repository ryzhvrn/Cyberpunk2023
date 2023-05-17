using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    private const string ShootAnimation = "Shoot";
    private const string IsShooted = "IsShooted";

    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _health = 100;
    [SerializeField] private float _distance = 5f;

    private Animator _animator;
    private float _fireRate = 1f;
    private float _nextFire = 0.0f;
    private Rigidbody2D _rb;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        DetectPlayer();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Shoot()
    {
        if (Time.time > _nextFire)
        {
            Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            _nextFire = Time.time + _fireRate;
        }
    }

    private void DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(_firePoint.position, Vector2.left, _distance);

        if (hit.collider != null && hit.collider.gameObject.TryGetComponent(out Player player))
        {
            _animator.SetBool(IsShooted, true);
            Shoot();
        }
        else
        {
            _animator.SetBool(IsShooted, false);
        }
    }
}
