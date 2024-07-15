using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesFactory : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _enemyCount;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private float _timeBetweenSpawn;

    private SpawnPointGenerator _spawnPointGenerator;
    private Player _player;

    private List<Enemy> _enemiesPool;

    private float _counter;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();

        _spawnPointGenerator = new SpawnPointGenerator(_spawnDistance);

        _enemiesPool = new List<Enemy>(Create(_enemyCount));
    }

    private void Update()
    {
        _counter += Time.deltaTime;

        if (_counter >= _timeBetweenSpawn)
        {
            Enemy enemy = _enemiesPool.First();
            enemy.gameObject.SetActive(true);
            _enemiesPool.Remove(enemy);
            _counter = 0;
        }

        if(_enemiesPool.Count == 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public List<Enemy> Create(int enemyCount)
    {
        List<Enemy> enemies = new List<Enemy>();

        for(int i = 0; i < enemyCount; i++)
            enemies.Add(Create());
        
        return enemies;
    }

    private Enemy Create()
    {
        Enemy enemy = Instantiate(_enemy, _spawnPointGenerator.Generate(_player.transform.position), Quaternion.identity);
        enemy.Initialize(_player);

        enemy.gameObject.SetActive(false);

        return enemy;
    }
}
