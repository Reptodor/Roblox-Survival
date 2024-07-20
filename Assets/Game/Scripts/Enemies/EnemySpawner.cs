using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private EnemiesFactory _enemiesFactory;

    public override void Spawn()
    {
        _enemiesFactory.Create(GetSpawnCount());
    }
}
