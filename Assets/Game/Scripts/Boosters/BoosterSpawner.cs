using UnityEngine;

public class BoosterSpawner : Spawner
{
    [SerializeField] private BoostersFactory boostersFactory;

    public override void Spawn()
    {
        boostersFactory.Create(GetSpawnCount());
    }
}
