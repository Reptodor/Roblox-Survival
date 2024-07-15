using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Gun _gun;
    private Rigidbody _rigidbody;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _gun = GetComponentInChildren<Gun>();

        _playerMovement = new PlayerMovement(_rigidbody, _speed);
    }

    private void FixedUpdate()
    {
        _playerMovement.Move();
    }
}
