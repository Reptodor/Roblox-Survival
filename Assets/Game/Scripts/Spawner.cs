using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] private int _maximumSpawnCount;
    [SerializeField] private int _minimumSpawnCount;
    [SerializeField] private float _timeBetweenSpawn;

    private float _counter;

    private void Update()
    {
        _counter += Time.deltaTime;

        if (_counter >= _timeBetweenSpawn)
        {
            Spawn();
            _counter = 0;
        }
    }

    public abstract void Spawn();
    

    protected int GetSpawnCount()
    {
        int spawnCount = Random.Range(_minimumSpawnCount, _maximumSpawnCount);

        return spawnCount;
    }
}
