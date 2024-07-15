using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(Collider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _damageDistance = 2f;

    private NavMeshAgent _enemy;
    private Player _player;

    public event Action<Enemy> Disabled;

    public void Initialize(Player player)
    {
        _enemy = GetComponent<NavMeshAgent>();
        _player = player;
    }

    private void OnDisable()
    {
        Disabled?.Invoke(this);
    }

    private void Update()
    {
        ChasePlayer();
        DamagePlayer();
    }

    public void TakeDamage(int damage)
    {

    }

    private void ChasePlayer()
    {
        _enemy.SetDestination(_player.transform.position);
    }

    private void DamagePlayer()
    {
        if (Vector3.Distance(_enemy.transform.position, _player.transform.position) < _damageDistance)
        {
            _player.gameObject.SetActive(false);
        }
    }
}
