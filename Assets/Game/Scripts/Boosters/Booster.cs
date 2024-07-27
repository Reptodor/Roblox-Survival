using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _groundDistance;
    
    private Player _player;
    private BoosterSpawner _boosterSpawner;
    private int _index;
    private float _boostMultiplier;
    private float _boostTime;

    private bool _grounded;

    public void Initialize(Player player, BoosterSpawner boosterSpawner, int index,float bustMultiplier, float boostTime)
    {
        _player = player;
        _boosterSpawner = boosterSpawner;
        _index = index;
        _boostMultiplier = bustMultiplier;
        _boostTime = boostTime;
        CheckForGround();
    }

    private void CheckForGround()
    {
        _grounded = Physics.Raycast(transform.position, Vector3.down, _groundDistance, _ground);

        if (!_grounded )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _player.gameObject)
        {
            _player.Boost(_index, _boostMultiplier, _boostTime);
            _boosterSpawner.SpawnedCount--;
            Destroy(gameObject);
        }
    }
}
