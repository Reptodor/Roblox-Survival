using UnityEngine;

public class LevelComplicator : MonoBehaviour
{
    [SerializeField] private EnemiesFactory _enemiesFactory;
    [SerializeField] private float _complicateTime;

    private float _timer;

    private void Update()
    {
        Complicate();
    }

    private void Complicate()
    {
        _timer += Time.deltaTime;
        
        if( _timer > _complicateTime )
        {
            _enemiesFactory.Complicate();
            _timer = 0;
        }
    }
}
