using System;
using UnityEngine;
using YG;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    [SerializeField] private Gun[] _guns;
    [SerializeField] private DeathMenu _deathMenu;
    [SerializeField] private int _baseHealth;
    [SerializeField] private int _healAmount;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private PlayerMovement _playerMovement;
    private PlayerAnimations _playerAnimations;
    private Health _health;
    private HealthDisplay _healthDisplay;
    private StatsChanger _statsChanger;
    private PlayerAttacker _playerAttacker;
    private AudioSource _hitSound;
    private Animator _animator;

    private Rigidbody _rigidbody;

    public Action<int> Damaged;

    private void OnEnable()
    {
        Damaged += _health.RecieveDamage;
        _health.HealthChanged += _healthDisplay.Display;
        _health.Died += Death;

        Gun gun = _guns[1];
        gun.UziOutOfBullets += TakePistol;
    }

    private void OnDisable()
    {
        Damaged -= _health.RecieveDamage;
        _health.HealthChanged -= _healthDisplay.Display;
        _health.Died -= Death;

        Gun gun = _guns[1];
        gun.UziOutOfBullets -= TakePistol;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _healthDisplay = GetComponent<HealthDisplay>();
        _hitSound = GetComponent<AudioSource>();
        _animator = GetComponentInChildren<Animator>();

        _playerMovement = new PlayerMovement(_rigidbody, _speed);
        _health = new Health(_baseHealth, _hitSound);
        _playerAttacker = new PlayerAttacker(_damage);
        _statsChanger = new StatsChanger(_playerMovement, _playerAttacker);
        _playerAnimations = new PlayerAnimations(_animator);
    }

    private void Update()
    {
        _playerMovement.Move();
        transform.rotation = Quaternion.Lerp(transform.rotation, _playerMovement.Rotate(), _rotationSpeed);
        _playerAnimations.MoveAnimation(_playerMovement.IsMoving());
    }

    public int CurrentDamage() => _playerAttacker.Damage();

    public void TakeUzi()
    {
        _guns[0].gameObject.SetActive(false);
        _guns[1].gameObject.SetActive(true);
    }

    public void TakePistol()
    {
        _guns[1].gameObject.SetActive(false);
        _guns[0].gameObject.SetActive(true);
    }

    public void Boost(int boosterIndex, float boostMultiplier, float boostTime)
    {
        switch (boosterIndex)
        {
            case 0:
                TakeUzi();
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
        _deathMenu.gameObject.SetActive(true);
    }
}
