using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _lifeTime;
    [SerializeField] private int _damage;

    private Rigidbody _rigidbody;

    private float _speed;

    public event Action<Bullet> Disabled;

    public void Initialize(float bulletSpeed)
    {
        _speed = bulletSpeed;
        _rigidbody = GetComponent<Rigidbody>();
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
            other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);

            gameObject.SetActive(false);
            Disabled?.Invoke(this);
        }
    }
}
