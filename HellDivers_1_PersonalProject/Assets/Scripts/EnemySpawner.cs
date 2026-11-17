using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    
    [SerializeField] private Vector3 _spawnPos;
    [SerializeField] private Vector2 _randomPoint;

    [SerializeField] private int _shortEnemyCount;
    [SerializeField] private int _longEnemyCount;
    
    private void Start()
    {
        SetRandomPosition(5);
    }
    
    private void Update()
    {
    }

    private void SpawnShortEnemy()
    {
        
    }

    private void SpawnLongEnemy()
    {
        
    }

    private void SetRandomPosition(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _randomPoint = Random.insideUnitCircle * 5;
            
            Vector3 spawnPos = new Vector3(_spawnPos.x+_randomPoint.x, transform.position.y, _spawnPos.z+_randomPoint.y);
        
            Enemy enemy = PoolManager.Instance.LongEnemy.TakePool();
            enemy.ResetSpawnPosition(spawnPos);
            
            Debug.Log($"{i+1}번째 Enemy 생성위치 : {spawnPos}");
        }
    }
}
