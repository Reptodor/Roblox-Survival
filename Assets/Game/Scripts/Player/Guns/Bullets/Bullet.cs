using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _lifeTime;
    [SerializeField] private int _damage;

    private Player _player;
    private Rigidbody _rigidbody;

    private float _speed;

    public event Action<Bullet> Disabled;

    public void Initialize(float bulletSpeed)
    {
        _speed = bulletSpeed;
        _rigidbody = GetComponent<Rigidbody>();
        _player = FindAnyObjectByType<Player>();
    }

    private void OnEnable()
    {
        StartCoroutine(Live());
    }

    public void Shoot(Vector3 direction)
    {
        gameObject.SetActive(true);
        _rigidbody.velocity = direction * _speed;
    }

    private IEnumerator Live()
    {
        yield return new WaitForSeconds(_lifeTime);

        gameObject.SetActive(false);
        Disabled?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null) 
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(_player.CurrentDamage());

            gameObject.SetActive(false);
            Disabled?.Invoke(this);
        }
    }
}
