using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    
    [SerializeField] private Vector3 _spawnPos;
    [SerializeField] private Vector2 _randomPoint;

    [SerializeField] private int _shortEnemyCount;
    [SerializeField] private int _longEnemyCount;
    
    private void Update()
    {
        SpawnEnemy(EnemyType.LongRange);
    }

    private void SpawnEnemy(EnemyType enemyType)
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Enemy enemy;
            
            _randomPoint = Random.insideUnitCircle * 5;
            Vector3 spawnPoint = _spawnPoint.position;

            _spawnPos = new Vector3(spawnPoint.x + _randomPoint.x,
                spawnPoint.y,
                spawnPoint.z + _randomPoint.y);

            if (enemyType == EnemyType.ShortRange)
            {
                enemy = PoolManager.Instance.ShortEnemy.TakePool();
                enemy.ResetSpawnPosition(_spawnPos);
            }
            else if (enemyType == EnemyType.LongRange)
            {
                enemy = PoolManager.Instance.LongEnemy.TakePool();
                enemy.ResetSpawnPosition(_spawnPos);
            }

            Debug.Log($"Enemy 생성위치 : {_spawnPos}");
        }
    }
}
