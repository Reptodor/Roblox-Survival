using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(Collider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageDistance = 2f;
    [SerializeField] private float _damageCooldown;

    private NavMeshAgent _enemy;
    private Player _player;

    private bool _canAttack;

    public void Initialize(Player player)
    {
        _enemy = GetComponent<NavMeshAgent>();
        _player = player;
        _canAttack = true;
    }

    private void Update()
    {
        ChasePlayer();
        DamagePlayer();
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
