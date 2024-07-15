using UnityEngine;

public class PlayerMovement
{
    private Rigidbody _rigidbody;
    private float _speed;
    private float _zInput;
    private float _xInput;

    public PlayerMovement(Rigidbody rigidbody, float speed)
    {
        _rigidbody = rigidbody;
        _speed = speed;
    }

    private void GetAxis()
    {
        _zInput = Input.GetAxisRaw("Horizontal");
        _xInput = Input.GetAxisRaw("Vertical");
    }

    public void Move()
    {
        GetAxis();

        _rigidbody.velocity = new Vector3(_speed * _zInput, _rigidbody.velocity.y, _xInput * _speed);
    }
}
