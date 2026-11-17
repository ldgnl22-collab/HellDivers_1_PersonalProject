using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }
    
    [SerializeField] private Bullet _bulletPrefab;
    
    public ObjectPool<Bullet> BulletPool;

    private void Awake() => SetSingleton();

    private void Start() => CreatePools();

    private void CreatePools()
    {
        BulletPool = new ObjectPool<Bullet>(_bulletPrefab, 100, transform);
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
