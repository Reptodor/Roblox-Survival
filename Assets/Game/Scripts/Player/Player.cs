using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _baseHealth;
    [SerializeField] private int _healAmount;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private PlayerMovement _playerMovement;
    private Health _health;
    private HealthDisplay _healthDisplay;
    private StatsChanger _statsChanger;
    private PlayerAttacker _playerAttacker;

    private Gun _gun;

    private Rigidbody _rigidbody;

    public Action<int> Damaged;

    private void OnEnable()
    {
        Damaged += _health.RecieveDamage;
        _health.HealthChanged += _healthDisplay.Display;
        _health.Died += Death;
    }

    private void OnDisable()
    {
        Damaged -= _health.RecieveDamage;
        _health.HealthChanged -= _healthDisplay.Display;
        _health.Died -= Death;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _gun = GetComponentInChildren<Gun>();
        _healthDisplay = GetComponent<HealthDisplay>();

        _playerMovement = new PlayerMovement(_rigidbody, _speed);
        _health = new Health(_baseHealth);
        _playerAttacker = new PlayerAttacker(_damage);
        _statsChanger = new StatsChanger(_playerMovement, _playerAttacker);
    }

    private void FixedUpdate()
    {
        _playerMovement.Move();
        transform.rotation = Quaternion.Lerp(transform.rotation, _playerMovement.Rotate(), _rotationSpeed);
        Debug.Log(_playerAttacker.Damage());
    }

    public int CurrentDamage() => _playerAttacker.Damage();

    public void Boost(int boosterIndex, float boostMultiplier, float boostTime)
    {
        switch (boosterIndex)
        {
            case 0:
                StartCoroutine(_statsChanger.IncreaseSpeed(_speed, boostMultiplier, boostTime));
                break;
            case 1:
                StartCoroutine(_statsChanger.IncreaseDamage(_damage, boostMultiplier, boostTime));
                break;
            case 2:
                _health.Heal(_healAmount);
                break;
        }
    }

    private void Death()
    {
        this.gameObject.SetActive(false); 
    }
}
