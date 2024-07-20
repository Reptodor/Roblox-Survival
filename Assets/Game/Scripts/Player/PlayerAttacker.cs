using UnityEngine;

public class PlayerAttacker
{
    private int _damage;

    public PlayerAttacker(int damage)
    {
        _damage = damage;
    }

    public int Damage() => _damage;

    public void ChangeDamage(int damage)
    {
        _damage = damage;
    }
}
