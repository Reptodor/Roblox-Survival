using System;

public class Health
{
    private int _health;
    private int _currentHealth;

    public Action<float> HealthChanged;
    public Action Died;

    public Health(int health)
    {
        _health = health;
        _currentHealth = _health;
    }

    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        HealthChanged?.Invoke(GetCurrentHealthPercantage());
    }

    private float GetCurrentHealthPercantage() => (float)_currentHealth / _health;

    public void RecieveDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _currentHealth -= damage;
        HealthChanged?.Invoke(GetCurrentHealthPercantage());

        if (_currentHealth <= 0)
        {
            Died?.Invoke();
        }
    }
}
