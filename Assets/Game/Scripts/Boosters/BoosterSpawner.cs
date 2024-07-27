using UnityEngine;

public class BoosterSpawner : Spawner
{
    [SerializeField] private BoostersFactory boostersFactory;

    public override void Spawn()
    {
        if(SpawnedCount < MaximumCount)
        {
            int spawnCount = GetSpawnCount();
            boostersFactory.Create(spawnCount);
            SpawnedCount += spawnCount;
        }
    }
}
