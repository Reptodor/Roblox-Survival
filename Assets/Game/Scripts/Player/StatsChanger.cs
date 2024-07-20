using System.Collections;
using UnityEngine;

public class StatsChanger
{ 
    private PlayerMovement _playerMovement;
    private PlayerAttacker _playerAttacker;

    public StatsChanger(PlayerMovement playerMovement, PlayerAttacker playerAttacker)
    {
        _playerMovement = playerMovement;
        _playerAttacker = playerAttacker;
    }

    public IEnumerator IncreaseSpeed(float speed, float boostMultiplier, float boostTime)
    {
        _playerMovement.ChangeSpeed(speed * boostMultiplier);
        yield return new WaitForSeconds(boostTime);
        _playerMovement.ChangeSpeed(speed);
    }

    public IEnumerator IncreaseDamage(int damage, float boostMultiplier, float boostTime)
    {
        var baseDamage = damage;
        _playerAttacker.ChangeDamage((int)(baseDamage * boostMultiplier) + 1);
        yield return new WaitForSeconds(boostTime);
        _playerAttacker.ChangeDamage(damage);
    }
}
