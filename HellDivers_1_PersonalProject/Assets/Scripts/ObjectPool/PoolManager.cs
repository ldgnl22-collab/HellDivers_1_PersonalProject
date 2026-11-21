using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }
    
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private ShortRangeEnemy _shortEnemyPrefab;
    [SerializeField] private LongRangeEnemy _longEnemyPrefab;
    [SerializeField] private Beacon _beaconPrefab;
    
    public ObjectPool<Bullet> BulletPool;
    public ObjectPool<ShortRangeEnemy> ShortEnemy;
    public ObjectPool<LongRangeEnemy> LongEnemy;
    public ObjectPool<Beacon> BeaconPool;

    private void Awake()
    {
        SetSingleton();
        CreatePools();
    }
    
    private void CreatePools()
    {
        BulletPool = new ObjectPool<Bullet>(_bulletPrefab, 100, transform);
        ShortEnemy = new ObjectPool<ShortRangeEnemy>(_shortEnemyPrefab, 50, transform);
        LongEnemy = new ObjectPool<LongRangeEnemy>(_longEnemyPrefab,  50, transform);
        BeaconPool = new ObjectPool<Beacon>(_beaconPrefab, 10, transform);
    }
    
    private void SetSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
