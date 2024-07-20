using System.Collections.Generic;
using UnityEngine;

public class EnemiesFactory : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _spawnDistance;

    private SpawnPointGenerator _spawnPointGenerator;
    private Player _player;

    private List<Enemy> _enemies;

    public List<Enemy> Enemies() => _enemies;
    
    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();

        _spawnPointGenerator = new SpawnPointGenerator(_spawnDistance);

        _enemies = new List<Enemy>();
    }

    public void DestroyEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public List<Enemy> Create(int enemyCount)
    {
        for(int i = 0; i < enemyCount; i++)
            _enemies.Add(Create());
        
        return _enemies;
    }

    private Enemy Create()
    {
        Enemy enemy = Instantiate(_enemy, _spawnPointGenerator.Generate(_player.transform.position), Quaternion.identity);
        enemy.Initialize(_player);

        return enemy;
    }

}
