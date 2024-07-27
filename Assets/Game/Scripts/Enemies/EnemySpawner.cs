using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private EnemiesFactory _enemiesFactory;

    public override void Spawn()
    {
        if(SpawnedCount < MaximumCount)
        {
            int spawnCount = GetSpawnCount();
            _enemiesFactory.Create(spawnCount);
            SpawnedCount += spawnCount;
        }
    }
}
