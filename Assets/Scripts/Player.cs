using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string HurtAnimation = "Hurt";

    [SerializeField] private int _health = 100;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        _animator.Play(HurtAnimation);
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
        _animator.StopPlayback();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
