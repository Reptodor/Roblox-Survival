using System.Collections.Generic;
using UnityEngine;

public class BulletsFactory : MonoBehaviour
{
    [SerializeField] private Bullet _bullets;
    [SerializeField] private float _bulletsSpeed;

    public List<Bullet> Create(int bulletsCount)
    {
        List<Bullet> bullets = new List<Bullet>();

        for (int i = 0; i < bulletsCount; i++)
            bullets.Add(Create());

        return bullets;
    }

    private Bullet Create()
    {
        Bullet bullet = Instantiate(_bullets);
        bullet.Initialize(_bulletsSpeed);

        bullet.gameObject.SetActive(false);

        return bullet;
    }
}
