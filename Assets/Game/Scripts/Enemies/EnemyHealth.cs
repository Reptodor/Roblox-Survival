using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _health;

    private EnemiesFactory _enemiesFactory;
    private HealthDisplay _healthDisplay;

    private int _currentHealth;

    private void Awake()
    {
        _enemiesFactory = FindAnyObjectByType<EnemiesFactory>();
        _healthDisplay = GetComponent<HealthDisplay>();
        _currentHealth = _health;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _currentHealth -= damage;
        float healthPercantage = (float)_currentHealth / _health;
        _healthDisplay.Display(healthPercantage);

        if (_currentHealth <= 0)
            _enemiesFactory.DestroyEnemy(_enemy);
    }
}
