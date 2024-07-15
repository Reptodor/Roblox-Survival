using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 _offset;
    [Range(1, 10)]
    [SerializeField] private float _smoothFactor;

    public void FixedUpdate() => Follow();

    private void Follow()
    {
        transform.position = Vector3.Lerp(transform.position, _player.transform.position + _offset, _smoothFactor * Time.fixedDeltaTime);
    }
}
