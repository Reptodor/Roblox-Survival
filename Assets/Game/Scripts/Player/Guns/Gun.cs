using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] private Gun[] _guns;
    [SerializeField] private BulletsFactory _bulletsFactory;
    [SerializeField] private int _bulletInPoolCount;

    private List<Bullet> _bulletsPool;

    protected AudioSource _shootSound;

    public Action UziOutOfBullets;

    private void Awake()
    {
        _bulletsPool = new List<Bullet>(_bulletsFactory.Create(_bulletInPoolCount));
        _shootSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _bulletsPool.ForEach(bullet => bullet.Disabled += OnBulletDisabled);
    }

    private void OnDisable()
    {
        _bulletsPool.ForEach(bullet => bullet.Disabled -= OnBulletDisabled);
    }

    public int BulletInPoolCount() => _bulletInPoolCount;

    public void Shoot()
    {
        Bullet bullet = _bulletsPool.First();
        bullet.transform.position = gameObject.transform.position;
        _bulletsPool.Remove(bullet);
        bullet.Shoot(transform.forward);
        _shootSound.Play();
    }


    private void OnBulletDisabled(Bullet bullet)
    {
        _bulletsPool.Add(bullet);
    }
}
