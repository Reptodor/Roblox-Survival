using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] private Gun[] _guns;
    [SerializeField] private BulletsFactory _bulletsFactory;
    [SerializeField] private int _bulletInPoolCount;
    [SerializeField] private float _timeBetweenShoot;

    private List<Bullet> _bulletsPool;

    private bool _canShoot = true;
    
    protected AudioSource _shootSound;

    protected int ShootCount = 0;
    
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
        _canShoot = true;
        ShootCount = 0;
    }

    public int BulletInPoolCount() => _bulletInPoolCount;

    public virtual void Update()
    {
        if (Input.GetKey(KeyCode.Space) && _canShoot)
        {
            Shoot();
            StartCoroutine(Cooldown());
            ShootCount++;
        }
    }

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

    private IEnumerator Cooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_timeBetweenShoot);
        _canShoot = true;
    }
}
