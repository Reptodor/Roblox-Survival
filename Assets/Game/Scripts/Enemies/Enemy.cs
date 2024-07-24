using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _damageDistance = 2f;
    [SerializeField] private float _damageCooldown;

    private NavMeshAgent _enemy;
    private Player _player;
    private EnemyHealth _enemyHealth;

    private bool _canAttack;

    private int _damage;

    public void Initialize(Player player, EnemiesFactory enemiesFactory, int health, int damage)
    {
        _enemy = GetComponent<NavMeshAgent>();
        _player = player;
        _canAttack = true;
        _enemyHealth = GetComponent<EnemyHealth>();

        _damage = damage;

        _enemyHealth.Initialize(this, enemiesFactory);
        _enemyHealth.SetHealth(health);
        transform.LookAt(_player.transform.position);
    }

    private void FixedUpdate()
    {
        DamagePlayer();
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        _enemy.SetDestination(_player.transform.position);
    }

    private void DamagePlayer()
    {
        if (Vector3.Distance(_enemy.transform.position, _player.transform.position) < _damageDistance && _canAttack)
        {
            _player.Damaged?.Invoke(_damage);
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_damageCooldown);
        _canAttack = true;
    }
}
