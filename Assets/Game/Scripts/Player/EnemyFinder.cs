using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    private EnemiesFactory _enemiesFactory;
    private Enemy _enemy;


    private void Awake()
    {
        _enemiesFactory = FindAnyObjectByType<EnemiesFactory>();
    }

    private void Update()
    {
        GetClosest(transform.position);
        if (_enemy != null)
            transform.LookAt(_enemy.transform.position);
    }

    public void GetClosest(Vector3 playerPosition)
    {
        float distance = Mathf.Infinity;
        foreach (Enemy enemy in _enemiesFactory.Enemies())
        {
            float currentDistance = Vector3.Distance(enemy.transform.position, playerPosition);

            if (enemy.gameObject.activeSelf)
            {
                if (currentDistance < distance)
                {
                    _enemy = enemy;
                    distance = currentDistance;
                }
            }
        }
    }
}
