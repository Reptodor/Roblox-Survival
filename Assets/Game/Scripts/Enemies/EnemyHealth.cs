using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Enemy _enemy;
    private EnemiesFactory _enemiesFactory;
    private HealthDisplay _healthDisplay;
    private AudioSource _hitSound;

    private int _baseHealth;
    private int _currentHealth;

    public void Initialize(Enemy enemy, EnemiesFactory enemiesFactory)
    {
        _enemy = enemy;
        _enemiesFactory = enemiesFactory;
        _healthDisplay = GetComponent<HealthDisplay>();
        _hitSound = GetComponent<AudioSource>();
    }

    public void SetHealth(int health)
    {
        _currentHealth = health;
        _baseHealth = health;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _currentHealth -= damage;
        _hitSound.Play();
        float healthPercantage = (float)_currentHealth / _baseHealth;
        _healthDisplay.Display(healthPercantage);

        if (_currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private bool IsHitSoundPlay() => _hitSound.isPlaying;

    private IEnumerator Die()
    {
        yield return new WaitWhile(IsHitSoundPlay);
        _enemiesFactory.DestroyEnemy(_enemy);
    }
}
