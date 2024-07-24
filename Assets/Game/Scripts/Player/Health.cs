using System;
using System.Collections;
using UnityEngine;

public class Health
{
    private AudioSource _hitSound;

    private int _health;
    private int _currentHealth;

    public Action<float> HealthChanged;
    public Action Died;

    public Health(int health, AudioSource hitSound)
    {
        _health = health;
        _hitSound = hitSound;
        _currentHealth = _health;
    }

    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;

        if (_currentHealth > _health)
            _currentHealth = _health;

        HealthChanged?.Invoke(GetCurrentHealthPercantage());
    }

    private float GetCurrentHealthPercantage() => (float)_currentHealth / _health;

    public void RecieveDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _currentHealth -= damage;
        _hitSound.Play();
        HealthChanged?.Invoke(GetCurrentHealthPercantage());

        if (_currentHealth <= 0)
        {
            Died?.Invoke();
        }
    }
}
