using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private Rigidbody2D _rigidbody;

    private int _damage = 20;

    private void Start()
    {
        _rigidbody.velocity = -transform.right * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}
