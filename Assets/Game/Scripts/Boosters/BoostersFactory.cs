using System.Collections.Generic;
using UnityEngine;

public class BoostersFactory : MonoBehaviour
{
    [SerializeField] private Booster[] _boosters;
    [SerializeField] private float _boostMultiplier;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private float _boostTime;

    private SpawnPointGenerator _spawnPointGenerator;
    private BoosterSpawner _boosterSpawner;
    private Player _player;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
        _boosterSpawner = FindAnyObjectByType<BoosterSpawner>();    
        _spawnPointGenerator = new SpawnPointGenerator(_spawnDistance);
    }

    public List<Booster> Create(int boostersCount)
    {
        List<Booster> boosters = new List<Booster>();

        for (int i = 0; i < boostersCount; i++)
            boosters.Add(Create());

        return boosters;
    }

    private int ChooseBooster()
    {
        var boosterIndex = Random.Range(0, _boosters.Length);
        return boosterIndex;
    }

    private Booster Create()
    {
        int boosterIndex = ChooseBooster();
        Quaternion rotation = _boosters[boosterIndex].transform.rotation;
        Booster booster = Instantiate(_boosters[boosterIndex], _spawnPointGenerator.Generate(_player.transform.position), rotation);
        booster.Initialize(_player, _boosterSpawner, boosterIndex, _boostMultiplier, _boostTime);

        return booster;
    }
}
