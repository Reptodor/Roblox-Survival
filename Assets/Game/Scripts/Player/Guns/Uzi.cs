using System.Collections;
using UnityEngine;

public class Uzi : Gun
{
    [SerializeField] private float _timeBetweenShoot;
    private int _shootCount = 0;
    private bool _canShoot = true;

    private void OnDisable()
    {
        _canShoot = true;
        _shootCount = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && _canShoot)
        {
            Shoot();
            StartCoroutine(Cooldown());
            _shootCount++;
        }

        if(_shootCount > BulletInPoolCount()) 
        {
            UziOutOfBullets?.Invoke();
        }
    }

    private IEnumerator Cooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_timeBetweenShoot);
        _canShoot = true;
    }
}
