using UnityEngine;

public class PlayerMovement
{
    private Rigidbody _rigidbody;
    private float _speed;
    private float _forwardInput;
    private float _strafeInput;

    public PlayerMovement(Rigidbody rigidbody, float speed)
    {
        _rigidbody = rigidbody;
        _speed = speed;
    }

    public void Move()
    {
        _rigidbody.velocity = GetAxis() * _speed;
    }

    public void ChangeSpeed(float speed)
    {
        _speed = speed;
    }

    public Quaternion Rotate()
    {
        Quaternion rotation = Quaternion.LookRotation(GetAxis());

        rotation.x = 0;
        rotation.z = 0;

        return rotation;
    }

    private Vector3 GetAxis()
    {
        _forwardInput = Input.GetAxisRaw("Horizontal");
        _strafeInput = Input.GetAxisRaw("Vertical");

        Vector3 moveInput = new Vector3(_forwardInput, 0, _strafeInput);
        return moveInput;
    }
}
